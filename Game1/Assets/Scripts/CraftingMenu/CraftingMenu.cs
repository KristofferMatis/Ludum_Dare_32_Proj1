using UnityEngine;
using System.Collections;

public class CraftingMenu : Singleton<CraftingMenu>
{
    public GameObject i_ItemPrefab;

    const int TOTAL_ITEM_SLOTS = 7;
    ItemSlot[] m_InventorySlots;
    AttachmentSlot[] m_AttachmentSlots;
    BaseWeaponSlot m_BaseWeaponSlot;
    

    //=========================================================================

    ClickHandler m_ClickHandler = new ClickHandler();
    public ClickHandler clickHandler
    {
        get { return m_ClickHandler; }
    }

    HoverInfo m_HoverInfo = new HoverInfo();
    public HoverInfo hoverInfo
    {
        get { return m_HoverInfo; }
    }

    //TODO: get a reference to it
    Inventory m_Inventory;

    //=========================================================================

    bool m_IsActive = false;
    public bool IsActive
    {
        get { return m_IsActive; }
        set { m_IsActive = value; }
    }

	// Use this for initialization
	void Start () 
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

		m_ClickHandler.Start ();
		m_HoverInfo.Start ();
		
		//since attachment slots and base weapon slots inherit from item slot we need to sort them
        m_InventorySlots = gameObject.GetComponentsInChildren<ItemSlot>();
        ItemSlot[] tempSlots = new ItemSlot[TOTAL_ITEM_SLOTS];
        int counter = 0;
        for (int i = 0; i < m_InventorySlots.Length; i++)
        {
            if(m_InventorySlots.GetType() != typeof(AttachmentSlot) && m_InventorySlots.GetType() != typeof(BaseWeaponSlot))
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


		for (int i = 0; i < Inventory.Instance.getIventory().Length; i++)
        {
            m_InventorySlots[i].OnMount( GameObject.Instantiate(i_ItemPrefab).AddComponent<Item>());
            m_InventorySlots[i].getItem().start(Inventory.Instance.getIventory()[i]);

			//TODO: replace ArtClone With the Menu art version of the asset
			GameObject artClone = (GameObject)(GameObject.Instantiate(Inventory.Instance.getIventory()[i].gameObject, 
			                                                          m_InventorySlots[i].getItem().transform.position,
			                                                          m_InventorySlots[i].getItem().transform.rotation));

            m_InventorySlots[i].getItem().transform.localScale = artClone.transform.localScale;
            artClone.transform.parent = m_InventorySlots[i].getItem().transform;

			MeshCollider meshCollider = m_InventorySlots[i].getItem().gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = meshCollider.GetComponentInChildren<MeshFilter>().mesh;

            m_InventorySlots[i].getItem().gameObject.layer = LayerMask.NameToLayer(m_ClickHandler.RaycastLayers[0]);
        }

        //TODO: get the current baseWeapon
        //TODO: get the current attachments from the baseWeapon

        UpdateAttachmentSlots();
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_ClickHandler.Update();
        m_HoverInfo.Update();   

        //TODO: REMOVE THIS IT IS FOR TESTING ONLY
		if (Input.GetKeyDown (KeyCode.Alpha0)) 
		{
			EnterMenu();
		}
		else if(Input.GetKeyDown (KeyCode.Alpha1))
		{
			ExitMenu();
		}
	}

    void EnterMenu()
    {
		m_IsActive = true;

        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;

        //TODO: make menu Visable

        //TODO: get slot data from weapon
        //TODO: get item list from inventory

        //TODO: create the menus slot list
        //TODO: create the menus item list
    }

    void ExitMenu()
    {
		m_IsActive = false;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        //TODO: make menu InVisable

        //TODO: set weapon slot data
        //TODO: set  inventory item list
    }

    public void OnMountItem(Item item, ItemSlot itemSlot)
    {
        if(itemSlot.GetType() == typeof(BaseWeaponSlot))
        {
            UpdateAttachmentSlots();
        }
    }

    public void OnDisMountItem(Item item, ItemSlot itemSlot)
    {
        if (itemSlot.GetType() == typeof(BaseWeaponSlot))
        {
            UpdateAttachmentSlots();
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
}
