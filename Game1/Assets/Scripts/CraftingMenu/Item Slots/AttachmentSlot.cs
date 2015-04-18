using UnityEngine;
using System.Collections;

public class AttachmentSlot : ItemSlot
{
    BaseAttachment m_Attachment;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override bool CanAcceptItem(Item item)
    {
        if (item.Attachment != null)
            return true;
        else
            return false;
    }
}
