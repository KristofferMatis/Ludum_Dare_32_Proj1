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
        m_Item = item;
        m_Item.OnMount(this);

        CraftingMenu.Instance.OnMountItem(m_Item, this);
    }

    //used to dismount the current item
    public virtual Item OnDisMount()
    {
        if (m_Item != null)
        {
            Item temp = m_Item;
            m_Item = null;
            temp.OnDisMount();
            CraftingMenu.Instance.OnDisMountItem(temp, this);
            return temp;
        }
        return null;
    }
}
