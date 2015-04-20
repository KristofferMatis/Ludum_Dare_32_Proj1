using UnityEngine;
using System.Collections;

public class CraftingMenu : Singleton<CraftingMenu>
{
    public GameObject i_ItemPrefab;

    const int TOTAL_ITEM_SLOTS = 7;
    ItemSlot[] m_InventorySlots;
    AttachmentSlot[] m_AttachmentSlots;
    BaseWeaponSlot m_BaseWeaponSlot;
    public CraftingCamera m_CraftingCamera;
 
    //=========================================================================

    ClickHandler m_ClickHandler = new ClickHandler();
    public ClickHandler clickHandler
    {
        get { return m_ClickHandler; }
    }

    public HoverInfo hoverInfo
    {
        get { return clickHandler.hoverInfo; }
    }

    public Attack i_AttackPlayer;

    public Transform i_WeaponMount;

    //=========================================================================

    bool m_IsActive = false;
    public bool IsActive
    {
        get { return m_IsActive; }
        set { m_IsActive = value; }
    }

    Camera m_MainCamera;

	// Use this for initialization
	void Start () 
    {
        i_AttackPlayer.m_MenuPivotPoint = i_WeaponMount;

        m_MainCamera = Camera.main;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

		m_ClickHandler.Start ();
        m_ClickHandler.m_CraftingCamera = m_CraftingCamera;
        
        
		
		//since attachment slots and base weapon slots inherit from item slot we need to sort them
        m_InventorySlots = gameObject.GetComponentsInChildren<ItemSlot>();
        ItemSlot[] tempSlots = new ItemSlot[TOTAL_ITEM_SLOTS];
        int counter = 0;
        for (int i = 0; i < m_InventorySlots.Length; i++)
        {
            if (m_InventorySlots.GetType() != typeof(AttachmentSlot) && m_InventorySlots.GetType() != typeof(BaseWeaponSlot) && m_InventorySlots.GetType() != typeof(ItemRemove))
            {//its an item slot
                tempSlots[counter++] = m_InventorySlots[i];
                if(counter>=TOTAL_ITEM_SLOTS)
                {
                    break;
                }
            }
        }
        m_InventorySlots = tempSlots;

        m_AttachmentSlots = gameObject.GetComponentsInChildren<AttachmentSlot>();
        m_BaseWeaponSlot = gameObject.GetComponentInChildren<BaseWeaponSlot>();

		if (i_AttackPlayer.m_InitialWeapon != null)
        {
            Item tempItem = GameObject.Instantiate(i_ItemPrefab).AddComponent<Item>();
			tempItem.start(i_AttackPlayer.m_InitialWeapon);

            m_BaseWeaponSlot.OnMount(tempItem);

			GameObject artClone = (GameObject)(GameObject.Instantiate(i_AttackPlayer.m_InitialWeapon.MenuPrefab,
                                                                      m_BaseWeaponSlot.getItem().transform.position,
                                                                      m_BaseWeaponSlot.getItem().transform.rotation));

            m_BaseWeaponSlot.getItem().transform.localScale = artClone.transform.localScale;
            artClone.transform.parent = m_BaseWeaponSlot.getItem().transform;

            MeshCollider meshCollider = m_BaseWeaponSlot.getItem().gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = meshCollider.GetComponentInChildren<MeshFilter>().mesh;

            Transform[] objects = m_BaseWeaponSlot.getItem().gameObject.GetComponentsInChildren<Transform>();
            for (int c = 0; c < objects.Length; c++)
            {
                objects[c].gameObject.layer = LayerMask.NameToLayer(m_ClickHandler.RaycastLayers[0]);
            }
        }
        UpdateAttachmentSlots();
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.forward = m_MainCamera.transform.position - transform.position;
        m_ClickHandler.Update();  
	}

    public void EnterMenu()
    {
		m_IsActive = true;

        i_AttackPlayer.DrawWeapon(false);

        for (int i = 0; i < Inventory.Instance.getIventory().Length; i++)
        {
			if(Inventory.Instance.getIventory()[i] == null)
				continue;

            m_InventorySlots[i].OnMount(GameObject.Instantiate(i_ItemPrefab).AddComponent<Item>());
            m_InventorySlots[i].getItem().start(Inventory.Instance.getIventory()[i]);

            GameObject artClone = (GameObject)(GameObject.Instantiate(Inventory.Instance.getIventory()[i].MenuPrefab,
                                                                      m_InventorySlots[i].getItem().transform.position,
                                                                      m_InventorySlots[i].getItem().transform.rotation));

            m_InventorySlots[i].getItem().transform.localScale = artClone.transform.localScale;
            artClone.transform.parent = m_InventorySlots[i].getItem().transform;

            MeshCollider meshCollider = m_InventorySlots[i].getItem().gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = meshCollider.GetComponentInChildren<MeshFilter>().mesh;

            Transform[] objects = m_InventorySlots[i].getItem().gameObject.GetComponentsInChildren<Transform>();
            for (int c = 0; c < objects.Length; c++)
            {
                objects[c].gameObject.layer = LayerMask.NameToLayer(m_ClickHandler.RaycastLayers[0]);
            }
        }
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
    }

    public void ExitMenu()
    {
		m_IsActive = false;

		if( i_AttackPlayer.m_WeaponEquipped != null)
			i_AttackPlayer.m_WeaponEquipped.SetTotalStatsAfterCrafting ();
        i_AttackPlayer.DrawWeapon(true);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        for(int i = 0; i < m_InventorySlots.Length; i++)
        {
			Inventory.Instance.setAttachment((m_InventorySlots[i].getItem() != null)? m_InventorySlots[i].getItem().Drop : null, i);
			if(m_InventorySlots[i].getItem() != null)
				Destroy(m_InventorySlots[i].OnDisMount().gameObject);
        }
    }

    public void OnMountItem(Item item, ItemSlot itemSlot)
    {
        if(itemSlot.GetType() == typeof(BaseWeaponSlot))
        {
            UpdateAttachmentSlots();            
            i_AttackPlayer.EquipWeapon(GameObject.Instantiate(item.BaseWeapon.gameObject).GetComponent<BaseBaseWeapon>());

            if(!m_IsActive)
            {
                i_AttackPlayer.DrawWeapon(true);
            }
        }
		else if(item.Attachment != null && itemSlot.GetType() == typeof(AttachmentSlot))
		{
            item.AttachedIndex = i_AttackPlayer.m_WeaponEquipped.AddAttachment((GameObject)GameObject.Instantiate(item.Attachment.gameObject));
		}
    }

    public void OnDisMountItem(Item item, ItemSlot itemSlot)
    {
        if (itemSlot.GetType() == typeof(BaseWeaponSlot))
        {
            i_AttackPlayer.EquipWeapon(null);
            UpdateAttachmentSlots();
        }
		else if(item.Attachment != null && itemSlot.GetType() == typeof(AttachmentSlot))
		{
			if(i_AttackPlayer.m_WeaponEquipped != null)
            	i_AttackPlayer.m_WeaponEquipped.RemoveAttachment(item.AttachedIndex);
		}
    }

    public void SwapItems(ItemSlot slot1, ItemSlot slot2)
    {
        ItemSlot.SwapItemsNoReAdding(slot1, slot2);
        SwapItems(slot1.getItem(), slot2.getItem());
    }

    void SwapItems(Item item1, Item item2)
    {
        i_AttackPlayer.m_WeaponEquipped.SwapAttachment(item1.AttachedIndex, item2.AttachedIndex);

        int tempIndex = item1.AttachedIndex;
        item1.AttachedIndex = item2.AttachedIndex;
        item2.AttachedIndex = tempIndex;
    }

    public void ReAttachAllAttachments()
    {
        //MIGHT NEED TO RECALCULATE THE ENABLED SLOTS HERE
        for (int i = 0; i < m_AttachmentSlots.Length; i++)
        {
            if(m_AttachmentSlots[i].getItem() != null)
            {
                if(m_AttachmentSlots[i].IsEnabled)
                {
                    m_AttachmentSlots[i].OnMount(m_AttachmentSlots[i].OnDisMount());
                }
                else
                {
                    for (int c = 0; c < m_AttachmentSlots.Length; c++)
                    {
                        if (m_AttachmentSlots[c].getItem() == null && m_AttachmentSlots[i].IsEnabled)
                        {
                            m_AttachmentSlots[c].OnMount(m_AttachmentSlots[i].OnDisMount());
                        }
                    }
                }
            }
        }
    }

    void UpdateAttachmentSlots()
    {
        int counter = 0;
        for (int i = 0; i < m_AttachmentSlots.Length; i++)
        {
            if (m_BaseWeaponSlot.getItem() != null && counter < m_BaseWeaponSlot.getItem().WeaponStats.m_MountPoints.Count)
            {
                m_AttachmentSlots[i].IsEnabled = true;
                counter++;
                continue;
            }
            m_AttachmentSlots[i].IsEnabled = false;
        }
    }

    public int getTotalInUseAtachmentSlots()
    {
        int counter = 0;
        for(int i =0; i <m_AttachmentSlots.Length; i++)
        {
            if (m_AttachmentSlots[i].getItem() != null)
                counter++;
        }
        return counter;
    }

    const string SET_M_ITEM_THIS_FRAME_TO_NULL_COUROUTINE = "setm_ItemThisFrameToNull";
    void OnGUI()
    {
        m_ClickHandler.onGUI();
        StartCoroutine(SET_M_ITEM_THIS_FRAME_TO_NULL_COUROUTINE);
    }

    IEnumerator setm_ItemThisFrameToNull()
    {
        yield return new WaitForEndOfFrame();
        m_ClickHandler.setm_ItemThisFrameToNull();
    }
}
