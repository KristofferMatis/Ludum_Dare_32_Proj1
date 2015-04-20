﻿using UnityEngine;
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

    const string ATTACHMENT = "Attachment";
    const string BASE_WEAPON = "Weapon";
    public string ItemType
    {
        get { return (BaseWeapon == null) ? ATTACHMENT : BASE_WEAPON; }
    }


    string m_MiscEffects = "n/a";
    public string MiscEffects
    {
        get { return m_MiscEffects; }
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

    WeaponDrop m_WeaponDrop = null;
    public WeaponDrop Drop
    {
        get { return m_WeaponDrop; }
    }

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

    public int AttachedIndex { get; set; }

	// Use this for initialization
	public void start (WeaponDrop drop) 
    {
        m_WeaponDrop = drop;
        m_Attachment = drop.GamePrefab.GetComponentInChildren<BaseAttachment>(); 
        m_BaseWeapon = m_Attachment as BaseBaseWeapon;

        MiscEffects[] effects = drop.GamePrefab.GetComponentsInChildren<MiscEffects>();

        if(effects.Length > 0)
        {
            m_MiscEffects = "";
            for(int i = 0; i < effects.Length; i++)
            {
                m_MiscEffects += effects[i].EffectType;
            }
        }
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
