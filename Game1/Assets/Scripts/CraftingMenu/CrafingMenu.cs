using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrafingMenu : Singleton<CrafingMenu>
{
    ItemSlot[] m_InventorySlots;
    BaseWeaponSlot m_BaseWeaponSlot;
    List<AttachmentSlot> m_AttachmentSlots = new List<AttachmentSlot>();

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
        m_ClickHandler.m_CraftingMenu = this;
        m_HoverInfo.m_CraftingMenu = this;
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_ClickHandler.Update();
        m_HoverInfo.Update();        
	}

    void EnterMenu()
    {
        //TODO: enable Curser
        //TODO: make curser visible

        //TODO: make menu Visable

        //TODO: get slot data from weapon
        //TODO: get item list from inventory

        //TODO: create the menus slot list
        //TODO: create the menus item list
    }

    void ExitMenu()
    {
        //TODO: Disable Curser
        //TODO: make curser Invisible

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
