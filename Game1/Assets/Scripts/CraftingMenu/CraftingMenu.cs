using UnityEngine;
using System.Collections;

public class CraftingMenu : Singleton<CraftingMenu>
{
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

        m_ClickHandler.m_CraftingMenu = this;
        m_HoverInfo.m_CraftingMenu = this;

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


        for (int i = 0; i < m_InventorySlots.Length; i++)
        {
            m_InventorySlots[i].OnMount(Inventory.Instance.getIventory()[i]);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_ClickHandler.Update();
        m_HoverInfo.Update();   

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

    void OnMountItem(Item item, ItemSlot itemSlot)
    {
        //TODO: check total attachment slots allowed and update accordingly
    }

    void OnDisMountItem(Item item, ItemSlot itemSlot)
    {
        //TODO: check total attachment slots allowed and update accordingly
    }
}
