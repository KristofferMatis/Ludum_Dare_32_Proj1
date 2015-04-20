using UnityEngine;
using System.Collections;

public class HoverInfo
{
    const string TEXT_BG_PATH = "Textures/HoverTextBG";
    Texture m_TextBG;
    Rect m_PositionBG;
    Rect m_PositionText;
    Item m_ItemThisFrame = null;
    ItemSlot m_SlotThisFrame = null;
    float m_OffsetX = 0.05f;
	Vector2 m_TextOffset = new Vector2 (15.0f, 10.0f);


    string[] m_Stats = new string[9];
	string[] m_StatsBaseString = new string[] { "Item : ", "Item Type : ", "Misc Effects : ", "Damage : ", "Knockback : ", "StartUpTime : ", "RecoveryTime : ", "AttackType : ", "MountPoints : " };

    public ClickHandler clickHandler { get; set; }

    int update = 0;
    int gui = 0;

    enum Stats
    { 
        ItemName,
        ItemType,
        MiscEffects,
        Damage,
        Knockback,        
        StartUpTime,
        RecoveryTime,
		AttackType,
        MountPoints
    };
	// Use this for initialization
    public void Start() 
    {
        m_TextBG = Resources.Load<Texture>(TEXT_BG_PATH);
        m_PositionBG = new Rect(0.0f, 0.0f, 190.0f, 195.0f);
        m_PositionText = new Rect(0.0f, 0.0f, 190.0f, 0.0f);
        m_OffsetX *= Screen.width;
	}
	
	// Update is called once per frame
	public void Update () 
    {
        if (!CraftingMenu.Instance.IsActive)
            return;


        RaycastHit[] hitInfo;
        Item[] items;
        ItemSlot[] itemSlots;

        if(Raycast(out hitInfo, out items, out itemSlots))
        {
            if (items.Length > 0)
            {

                // get new strings
                setStringData(items[0]);

                m_ItemThisFrame = items[0];
            }
            else if(itemSlots.Length > 0)
            {
                m_SlotThisFrame = itemSlots[0];
            }
        } 
	}

    void setStringData(Item item)
    {
        setStringData(item.Attachment.m_Stats);
        m_Stats[(int)Stats.ItemName]        += item.ItemName;
        m_Stats[(int)Stats.ItemType]        += item.ItemType;
        m_Stats[(int)Stats.MiscEffects]     += item.MiscEffects;
    }

    void setStringData(WeaponStats stats)
    {
        for(int i = 0; i < m_Stats.Length; i++)
        {
            m_Stats[i] = m_StatsBaseString[i];
        }

        
        m_Stats[(int)Stats.Damage]          += stats.m_Damage.ToString();
        m_Stats[(int)Stats.Knockback]       += stats.m_Knockback.ToString();
        m_Stats[(int)Stats.AttackType]      += stats.m_AttackType.ToString();
        m_Stats[(int)Stats.StartUpTime]     += stats.m_StartUpTime.ToString();
        m_Stats[(int)Stats.RecoveryTime]    += stats.m_RecoveryTime.ToString();
        m_Stats[(int)Stats.MountPoints]     += stats.m_MountPoints.Count.ToString();
    }

    public void onGUI()
    {
        if (m_ItemThisFrame != null)
        {
            m_PositionBG.position = Input.mousePosition;

            m_PositionBG.position = new Vector2(m_PositionBG.position.x - m_OffsetX - m_PositionBG.width, Screen.height - m_PositionBG.y);

            GUI.DrawTexture(m_PositionBG, m_TextBG, ScaleMode.StretchToFill);
            m_PositionText.position = m_PositionBG.position + m_TextOffset;

            if (m_ItemThisFrame.BaseWeapon != null)
            {
                m_PositionText.height = m_PositionBG.height / (float)m_Stats.Length - 3.0f;
                //m_PositionText
                for (int i = 0; i < m_Stats.Length; i++)
                {
                    GUI.Label(m_PositionText, m_Stats[i]);
                    m_PositionText.y += m_PositionText.height;
                }
            }
            else
            {
                m_PositionText.height = m_PositionBG.height / (float)m_Stats.Length - 2.0f;
                for (int i = 0; i < m_Stats.Length - 2; i++)
                {
                    GUI.Label(m_PositionText, m_Stats[i]);
                    m_PositionText.y += m_PositionText.height;
                }
            }
        }
        else if(m_SlotThisFrame != null)
        {
            AttachmentSlot slot = m_SlotThisFrame as AttachmentSlot;
            if (slot == null)
            {
                m_PositionBG.position = Input.mousePosition;

                m_PositionBG.position = new Vector2(m_PositionBG.position.x - m_OffsetX - m_PositionBG.width, Screen.height - m_PositionBG.y);

                GUI.DrawTexture(m_PositionBG, m_TextBG, ScaleMode.StretchToFill);
                m_PositionText.position = m_PositionBG.position + m_TextOffset;

                m_PositionText.height = m_PositionBG.height / (float)m_Stats.Length - 2.0f;

                for (int i = 0; i < m_SlotThisFrame.Instructions.Length; i++)
                {
                    GUI.Label(m_PositionText, m_SlotThisFrame.Instructions[i]);
                    m_PositionText.y += m_PositionText.height;
                }
            }
            else
            {
                m_PositionBG.position = Input.mousePosition;

                m_PositionBG.position = new Vector2(m_PositionBG.position.x - m_OffsetX - m_PositionBG.width, Screen.height - m_PositionBG.y);

                GUI.DrawTexture(m_PositionBG, m_TextBG, ScaleMode.StretchToFill);
                m_PositionText.position = m_PositionBG.position + m_TextOffset;

                m_PositionText.height = m_PositionBG.height / (float)m_Stats.Length - 2.0f;

                for (int i = 0; i < slot.Instructions.Length; i++)
                {
                    GUI.Label(m_PositionText, slot.Instructions[i]);
                    m_PositionText.y += m_PositionText.height;
                }
            }
        }
    }

    public void setm_ItemThisFrameToNull()
    {
        m_ItemThisFrame = null;
        m_SlotThisFrame = null;
    }


    /// </summary>
    /// <param name="hitInfo"></param>
    /// <param name="items"></param>
    /// <param name="itemSlots"></param>
    /// <returns></returns>
    bool Raycast(out RaycastHit[] hitInfo, out Item[] items, out ItemSlot[] itemSlots)
    {
        return clickHandler.Raycast(out  hitInfo, out  items, out  itemSlots);
    }
}
