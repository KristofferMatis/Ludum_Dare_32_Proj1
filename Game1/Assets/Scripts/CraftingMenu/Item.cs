using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour 
{
    public WeaponStats WeaponStats
    {
        get { return m_Attachment.m_Stats; }
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

    Transform m_CurrentMountPoint;
    const float ITEM_LERP_SPEED = 0.5f;

    bool m_IsBeingDragged = false;
    public bool IsBeingDragged
    {
        get { return m_IsBeingDragged; }
        set { m_IsBeingDragged = value; }
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
            if (m_CurrentMountPoint != null)
            {
                transform.position = Vector3.Lerp(transform.position, m_CurrentMountPoint.position, ITEM_LERP_SPEED * Time.deltaTime);
            }
        }
        else
        {
            //TODO: lerp to mouse pos
        }
	}

    public void OnMount(ItemSlot itemSlot)
    {
        m_CurrentMountPoint = itemSlot.i_MountPoint;
    }

    public void OnDisMount()
    {
        m_CurrentMountPoint = null;
    }
}
