using UnityEngine;
using System.Collections;

public class ItemRemove : ItemSlot 
{
    public bool i_IsBoatDrop = false;
    public override Item getItem()
    {
        return null;
    }

    public override bool CanAcceptItem(Item item)
    {
        return true;
    }

    public override Item OnDisMount()
    {
        if (i_IsBoatDrop)
        {
            //TODO: Handle Boat Building Here
        }
        return null;
    }

    public override void OnMount(Item item)
    {
        item.OnMount(this);
        Destroy(item.gameObject);
    }

    protected override void Start()
    {
        return;
    }
}
