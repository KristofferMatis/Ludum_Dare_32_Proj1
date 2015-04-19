using UnityEngine;
using System.Collections;

public class ItemRemove : ItemSlot 
{
    public bool i_IsBoatDrop = false;
    public override Item getItem()
    {
        return null;
    }

    public override bool CanAcceptItem(Item item)//TODO: once hover info is working make text say what is needed
    {
        //TODO: put logic on whether or not the item is of the requested type here
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
            //TODO: Handle Boat Building Here
        }

        Destroy(item.gameObject);
    }

    protected override void Start()
    {
        return;
    }
}
