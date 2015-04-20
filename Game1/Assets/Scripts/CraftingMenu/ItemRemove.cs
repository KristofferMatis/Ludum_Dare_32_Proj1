using UnityEngine;
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

			CraftingMenu.Instance.PlayBoatUpdated();
        }
		else
		{
			CraftingMenu.Instance.PlayItemDeleted();
		}

        Destroy(item.gameObject);
    }

    protected override void Start()
    {
        if (i_IsBoatDrop)
        {
            INSTRUCTIONS = new string[] {   "-->>BUILD BOAT<<--",
                                            "Items ADDED HERE", 
                                            "BUILD THE BOAT.", 
                                            "Items will be", 
                                            "PERMANENTLY CONSUMED.", 
                                            "Items NEEDED:" };
        }
        else
        {
            INSTRUCTIONS = new string[] {   " -->>TRASH<<--", 
                                            "Accepts ANY Item.", 
                                            "Items will be",
                                            "PERMANENTLY DESTROYED."};
        }

    }
}
