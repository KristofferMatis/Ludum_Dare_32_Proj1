using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickHandler 
{
    Camera m_MainCamera;

    LayerMask m_RaycastMask = new LayerMask();
	string[] m_RaycastLayers = new string[] {"Interactive Menu Piece"};

	LayerMask m_DragAndDropRaycastMask = new LayerMask();
	string[] m_DragAndDropRaycastLayers = new string[] {"Drag and Drop"};

    const float RAYCAST_DISTANCE = 500.0f;

    Item m_ItemBeingDragged = null;

	// Use this for initialization
    public void Start() 
	{
        m_MainCamera = Camera.main;
		
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
                    if (itemSlots[0].CanAcceptItem(m_ItemBeingDragged))
                    {
                        m_ItemBeingDragged.OnDisMount();
                        itemSlots[0].OnMount(m_ItemBeingDragged);
                    }
                }
            }
			m_ItemBeingDragged.DraggedToPos = m_ItemBeingDragged.MountedTo.i_MountPoint.position;
			m_ItemBeingDragged = null;
        }
	}

    /// <summary>
    /// Raycasts from the mouse
    /// returns true if something was hit
    /// 
    /// uses out to return relevant menu scripts
    /// 
    /// //TODO: add buttons (exit)
    /// 
    /// </summary>
    /// <param name="hitInfo"></param>
    /// <param name="items"></param>
    /// <param name="itemSlots"></param>
    /// <returns></returns>
    bool Raycast(out RaycastHit[] hitInfo, out Item[] items, out ItemSlot[] itemSlots)
    {
        items = new Item[0];
		itemSlots = new ItemSlot[0];
        //hitInfo = Physics.RaycastAll(m_MainCamera.ScreenPointToRay(Input.mousePosition), m_MainCamera.transform.forward, RAYCAST_DISTANCE, m_RaycastMask.value);
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
}
