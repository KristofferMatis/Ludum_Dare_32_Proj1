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
			return m_BoatManager.IsObjectTypeNecessary(item.Attachment.m_AttachmentName);
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
			m_BoatManager.BuildUpBoat(item.Attachment.m_AttachmentName, item.Drop.GamePrefab);
        }

        Destroy(item.gameObject);
    }

    protected override void Start()
    {
		m_BoatManager = GameObject.FindGameObjectWithTag (Constants.BOAT_TAG).GetComponent<BoatManager> ();
    }
}
