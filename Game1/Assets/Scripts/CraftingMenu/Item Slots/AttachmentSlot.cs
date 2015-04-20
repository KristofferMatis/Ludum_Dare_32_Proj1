﻿using UnityEngine;
using System.Collections;

public class AttachmentSlot : ItemSlot
{
    BaseAttachment m_Attachment;

    bool m_IsEnabled = true;
    public bool IsEnabled
    {
        get { return m_IsEnabled; }
        set 
        { 
            m_IsEnabled = value;
            UpdateMaterials();
        }
    }

    protected string[][] NEW_INSTRUCTIONS = new string[2][];
    new public string[] Instructions
    {
        get
        {
            if(m_IsEnabled)
            {
                return NEW_INSTRUCTIONS[1];
            }
            return NEW_INSTRUCTIONS[0];
        }
    }

    Renderer m_Renderer;
    Material[] m_Materials = new Material[2];
    const string DISABLED_MATERIAL_PATH = "Materials/CraftingMenu/Menu/CraftingMenu_AttachmentSlot_Disabled";
    const string ENABLED_MATERIAL_PATH =  "Materials/CraftingMenu/Menu/CraftingMenu_AttachmentSlot_Enabled";
    

	// Use this for initialization
	void Start ()
    {
        m_Renderer = gameObject.GetComponent<Renderer>();

        m_Materials[0] = Resources.Load<Material>(DISABLED_MATERIAL_PATH);
        m_Materials[1] = Resources.Load<Material>(ENABLED_MATERIAL_PATH);

        UpdateMaterials();

        NEW_INSTRUCTIONS[0] = new string[] {    "This is an Attachment", 
                                                "Slot, Any Item Can", 
                                                "Be Placed Here", 
                                                "This Slot Is Currently", 
                                                "Disabled. To Enable It", 
                                                "Equip a Weapon in the", 
                                                "Weapon Base Slot or a", 
                                                "Weapon With More", 
                                                "MountPoints" };

        NEW_INSTRUCTIONS[1] = new string[] {    "This is an Attachment", 
                                                "Slot, Any Item Can", 
                                                "Be Placed Here.", 
                                                "Items Added Here Will", 
                                                "Be Added To Your Weapon" };
	}
	
    public override bool CanAcceptItem(Item item)
    {
		if (item != null && item.Attachment != null && base.CanAcceptItem(item) && m_IsEnabled)
            return true;
        else
            return false;
    }

    public override void OnMount(Item item)
    {
        base.OnMount(item);

        IsEnabled = true;
    }

    public override Item OnDisMount()
    {
        return base.OnDisMount();        
    }

    void UpdateMaterials()
    {
        if (m_IsEnabled)
            m_Renderer.material = m_Materials[1];
        else
            m_Renderer.material = m_Materials[0];
    }
}
