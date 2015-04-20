﻿using UnityEngine;
using System.Collections;

public class ItemRemove : ItemSlot 
{
	BoatManager m_BoatManager;

    public bool i_IsBoatDrop = false;
    public override Item getItem()
    {
        return null;
    }

    public override bool CanAcceptItem(Item item)//TODO: once hover info is working make text say what is needed
    {
        if(i_IsBoatDrop)
		{
			return BoatManager.Instance.IsObjectTypeNecessary(item.Attachment.m_AttachmentName);
		}

        return true;
    }

    public override Item OnDisMount()
    {      
        return null;
    }

    public override void OnMount(Item item)
    {
        item.OnMount(this);

        // item.Attachment.gameObject <<clone this for art
        if (i_IsBoatDrop)
        {
			BoatManager.Instance.BuildUpBoat(item.Attachment.m_AttachmentName, item.Drop.GamePrefab);
        }

        Destroy(item.gameObject);
    }

    protected override void Start()
    {
        if (i_IsBoatDrop)
        {
            INSTRUCTIONS = new string[] { "This is The Boat", "Slot, Drag Items Here", "To Build The Boat", "WARNING: They Will Be", "DELETED.", "Items Still Needed:" };
        }
        else
        {
            INSTRUCTIONS = new string[] { "This is The Trash", "Drag Items Here To", "Delete Them" };
        }

    }
}
