using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickHandler 
{
    Camera m_MainCamera;

    LayerMask m_RaycastMask = new LayerMask();
	string[] m_RaycastLayers = new string[] {"Interactive Menu Piece"};

    public string[] RaycastLayers
    {
        get { return m_RaycastLayers; }
    }

	LayerMask m_DragAndDropRaycastMask = new LayerMask();
	string[] m_DragAndDropRaycastLayers = new string[] {"Drag and Drop"};

    const float RAYCAST_DISTANCE = 500.0f;

    Item m_ItemBeingDragged = null;

    public CraftingCamera m_CraftingCamera;

    HoverInfo m_HoverInfo = new HoverInfo();
    public HoverInfo hoverInfo
    {
        get { return m_HoverInfo; }
    }

	// Use this for initialization
    public void Start() 
	{
        m_MainCamera = Camera.main;

        m_HoverInfo.Start();
        m_HoverInfo.clickHandler = this;

        m_RaycastMask.value = LayerMask.GetMask(m_RaycastLayers);
        m_DragAndDropRaycastMask = LayerMask.GetMask(m_DragAndDropRaycastLayers);
	}
	
	// Update is called once per frame
    public void Update() 
	{
        if (!CraftingMenu.Instance.IsActive)
            return;

        if(Input.GetMouseButtonDown(0))
        {//click
            OnClickDown();
        }
        else if(Input.GetMouseButton(0))
        {//mouse being held
            OnClick();
        }
        else if(Input.GetMouseButtonUp(0))
        {//mouse Released
            OnClickUp();
        }
        else
        {
            m_HoverInfo.Update(); 
        }       
	}

    void OnClickDown()
    {
        RaycastHit[] hitInfo;
        Item[] items;
        ItemSlot[] itemSlots;
        if (Raycast(out hitInfo, out  items, out  itemSlots))
        {//hit something
            if (m_ItemBeingDragged == null && items.Length > 0)
            {
                items[0].IsBeingDragged = true;
                m_ItemBeingDragged = items[0];
                m_ItemBeingDragged.DraggedToPos = m_ItemBeingDragged.transform.position;
            }
			else if(m_ItemBeingDragged == null)
            {
                for(int i = 0; i < hitInfo.Length; i++)
                {
                    if (hitInfo[i].collider.CompareTag(Constants.EXIT_BUTTON_TAG))
                    {
                        //CraftingMenu.Instance.ExitMenu();
                        m_CraftingCamera.SetIsOn(false);
                        return;
                    }
                }
            }
        }
    }

	void OnClick()
	{
        if (m_ItemBeingDragged != null)
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(m_MainCamera.ScreenPointToRay (Input.mousePosition), out hitInfo, RAYCAST_DISTANCE, m_DragAndDropRaycastMask.value))
            {//This should always happen
                m_ItemBeingDragged.DraggedToPos = hitInfo.point;
            }
        }
	}	

	void OnClickUp()
	{
        if(m_ItemBeingDragged != null)
        {
            RaycastHit[] hitInfo;
            Item[] items;
            ItemSlot[] itemSlots;
            if (Raycast(out hitInfo, out  items, out  itemSlots))
            {//hit something
                if (itemSlots.Length > 0)
                {
                    if (itemSlots[0] != m_ItemBeingDragged.MountedTo && itemSlots[0].CanAcceptItem(m_ItemBeingDragged))
                    {//the item being dragged is valid to place in the slot

						if(itemSlots[0].getItem() != null)
                        {//the spot were placing it on already has something
							if(m_ItemBeingDragged.MountedTo.CanAcceptItem(itemSlots[0].getItem()))
							{//both items can be swapped
	                            SwapItems(itemSlots[0], m_ItemBeingDragged.MountedTo);
								return;
							}
                        }
                        else
                        {
							if(!(m_ItemBeingDragged.MountedTo.GetType() == typeof(BaseWeaponSlot)) || (CraftingMenu.Instance.getTotalInUseAtachmentSlots() == 0 && itemSlots[0].GetType() != typeof(AttachmentSlot)))
							{
	                            m_ItemBeingDragged.OnDisMount();
	                            itemSlots[0].OnMount(m_ItemBeingDragged);
							}
                        }
                    }
                }
            }
            m_ItemBeingDragged.DraggedToPos = m_ItemBeingDragged.MountedTo.i_MountPoint.position;
			m_ItemBeingDragged.IsBeingDragged = false;
            m_ItemBeingDragged = null;
        }
	}

    void SwapItems(ItemSlot slot1, ItemSlot slot2)
    {
        bool isSwappingBaseWeapon = false;
        if(slot1.GetType() == typeof(BaseWeaponSlot))
        {//swapping out the base weapon
            if(!checkBaseWeaponMounts(slot2.getItem()))
            {
                return;
            }
            isSwappingBaseWeapon = true;
        }
        else if (slot2.GetType() == typeof(BaseWeaponSlot))
		{//swapping out the base weapon
            if (!checkBaseWeaponMounts(slot1.getItem()))
            {
                return;
            }
            Item TempItem = slot2.OnDisMount();
            slot2.OnMount(slot1.OnDisMount());
            slot1.OnMount(TempItem);

            slot1.getItem().DraggedToPos = slot1.getItem().MountedTo.i_MountPoint.position;
            slot2.getItem().DraggedToPos = slot2.getItem().MountedTo.i_MountPoint.position;

            m_ItemBeingDragged = null;
            CraftingMenu.Instance.ReAttachAllAttachments();
            return;
        }
		else if(slot1.GetType() == typeof(AttachmentSlot) && slot2.GetType() == typeof(AttachmentSlot))
		{//swapping two items that are already on the weapon
            CraftingMenu.Instance.SwapItems(slot1, slot2);
            slot1.getItem().DraggedToPos = slot1.getItem().MountedTo.i_MountPoint.position;
            slot2.getItem().DraggedToPos = slot2.getItem().MountedTo.i_MountPoint.position;

            m_ItemBeingDragged = null;
            return;
		}


        Item tempItem = slot1.OnDisMount();
        slot1.OnMount(slot2.OnDisMount());
        slot2.OnMount(tempItem);

        slot1.getItem().DraggedToPos = slot1.getItem().MountedTo.i_MountPoint.position;
        slot2.getItem().DraggedToPos = slot2.getItem().MountedTo.i_MountPoint.position;

        m_ItemBeingDragged = null;

		if(isSwappingBaseWeapon)
		{//since base weapon was swapped we need to make sure the correct attachment slots are open / being used
            CraftingMenu.Instance.ReAttachAllAttachments();
        }
    }

    bool checkBaseWeaponMounts(Item notInUse)
    {
        if(notInUse.WeaponStats.m_MountPoints.Count >= CraftingMenu.Instance.getTotalInUseAtachmentSlots())
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Raycasts from the mouse
    /// returns true if something was hit
    /// 
    /// uses out to return relevant menu scripts
    /// 
    /// </summary>
    /// <param name="hitInfo"></param>
    /// <param name="items"></param>
    /// <param name="itemSlots"></param>
    /// <returns></returns>
    public bool Raycast(out RaycastHit[] hitInfo, out Item[] items, out ItemSlot[] itemSlots)
    {
        items = new Item[0];
		itemSlots = new ItemSlot[0];
        hitInfo = Physics.RaycastAll (m_MainCamera.ScreenPointToRay (Input.mousePosition), RAYCAST_DISTANCE, m_RaycastMask.value);
		Debug.DrawLine (m_MainCamera.ScreenPointToRay(Input.mousePosition).origin, m_MainCamera.ScreenPointToRay(Input.mousePosition).origin + (m_MainCamera.ScreenPointToRay(Input.mousePosition).direction * RAYCAST_DISTANCE), Color.black, 1.0f);

        if(hitInfo.Length > 0)
        {//hit at least one menu thing
            List<Item> itemsFound = new List<Item>();
            List<ItemSlot> itemSlotsFound = new List<ItemSlot>();

            for(int i = 0; i < hitInfo.Length; i++)
            {
                Item item = hitInfo[i].collider.gameObject.GetComponent<Item>();
                if (item != null)
                    itemsFound.Add(item);

                ItemSlot itemSlot = hitInfo[i].collider.gameObject.GetComponent<ItemSlot>();
				if (itemSlot != null)
					itemSlotsFound.Add(itemSlot);
            }

            items = itemsFound.ToArray();
            itemSlots = itemSlotsFound.ToArray();

            return true;
        }
        return false;
    }

    public void onGUI()
    {
        m_HoverInfo.onGUI();
    }

    public void setm_ItemThisFrameToNull()
    {
        m_HoverInfo.setm_ItemThisFrameToNull();
    }
}

    
