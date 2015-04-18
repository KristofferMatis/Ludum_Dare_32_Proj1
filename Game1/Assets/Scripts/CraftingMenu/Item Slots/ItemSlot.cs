using UnityEngine;
using System.Collections;

public class ItemSlot : MonoBehaviour 
{
    public Transform i_MountPoint;
    Item m_Item;

	// Use this for initialization
    protected virtual void Start() 
    {
	
	}
	
	// Update is called once per frame
    protected virtual void Update() 
    {
	
	}

    //will be used by children classes that can only take attachments or base weapons
    public virtual bool CanAcceptItem(Item item)
    {
        return true;
    }

    //used to get the current item
    public virtual Item getItem()
    {
        return m_Item;
    }

    //used to mount an item
    public virtual void OnMount(Item item)
    {
        item.OnMount(this);
    }

    //used to dismount the current item
    public virtual Item OnDisMount()
    {
        m_Item.OnDisMount();
        return m_Item;
    }
}
