using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour 
{
    public WeaponStats WeaponStats
    {
        get { return (m_Attachment != null) ? m_Attachment.m_Stats : 
                                            (m_BaseWeapon != null) ? m_BaseWeapon.m_Stats : null; }
    }

    BaseAttachment m_Attachment;
    public BaseAttachment Attachment
    {
        get { return m_Attachment; }
    }

    BaseBaseWeapon m_BaseWeapon;
    public BaseBaseWeapon BaseWeapon
    {
        get { return m_BaseWeapon; }
    }

    ItemSlot m_MountedTo = null;
    public ItemSlot MountedTo
    {
        get { return m_MountedTo; }
    }
    const float ITEM_LERP_SPEED = 10.0f;

    //============================================
    bool m_IsBeingDragged = false;
    public bool IsBeingDragged
    {
        get { return m_IsBeingDragged; }
        set { m_IsBeingDragged = value; }
    }

    Vector3 m_DragedToPos = Vector3.zero;
    public Vector3 DraggedToPos
    {
        get { return m_DragedToPos; }
        set { m_DragedToPos = value; }
    }

	// Use this for initialization
	void Start () 
    {
        m_Attachment = (BaseAttachment)gameObject.GetComponent(typeof(BaseAttachment));
        m_BaseWeapon = (BaseBaseWeapon)gameObject.GetComponent(typeof(BaseBaseWeapon));
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!m_IsBeingDragged)
        {
            if (m_MountedTo != null && m_MountedTo.i_MountPoint != null)
            {
                transform.position = Vector3.Lerp(transform.position, m_MountedTo.i_MountPoint.position, ITEM_LERP_SPEED * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, m_DragedToPos, ITEM_LERP_SPEED * Time.deltaTime);
        }
	}

    public void OnMount(ItemSlot itemSlot)
    {
        m_MountedTo = itemSlot;
    }

    public void OnDisMount()
    {
        if(m_MountedTo != null)
        {
            ItemSlot temp = m_MountedTo;
            m_MountedTo = null;
            temp.OnDisMount();            
        }        
    }
}
