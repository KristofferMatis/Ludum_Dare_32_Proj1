using UnityEngine;
using System.Collections;

public class HoverInfo
{
    const string TEXT_BG_PATH = "Textures/HoverTextBG";
    Texture m_TextBG;
    Rect m_PositionBG;
    Rect m_PositionText;
    Item m_ItemThisFrame = null;
    float m_OffsetX = 0.1f;

    string[] m_Stats = new string[6];
    string[] m_StatsBaseString = new string[] { "Damage : ", "Knockback : ", "AttackType : ", "StartUpTime : ", "RecoveryTime : ", "MountPoints : " };

    public ClickHandler clickHandler { get; set; }

    enum Stats
    { 
        Damage,
        Knockback,
        AttackType,
        StartUpTime,
        RecoveryTime,
        MountPoints
    };
	// Use this for initialization
    public void Start() 
    {
        m_TextBG = Resources.Load<Texture>(TEXT_BG_PATH);
        m_PositionBG = new Rect(0.0f, 0.0f, 150.0f, 105.0f);
        m_PositionText = new Rect(0.0f, 0.0f, 150.0f, 0.0f);
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
            if (items.Length <= 0)
                return;

            // get new strings
            setStringData(items[0]);            

            //TODO: update rect
            m_ItemThisFrame = items[0];
        } 
	}

    void setStringData(Item item)
    {
        setStringData(item.Attachment.m_Stats);
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
        if (!m_ItemThisFrame != null)
            return;

        GUI.DrawTexture(m_PositionBG, m_TextBG, ScaleMode.StretchToFill);
        m_PositionText.position = m_PositionBG.position;

        if(!m_ItemThisFrame.BaseWeapon != null)
        {
            m_PositionText.height = m_PositionBG.height / 6.0f;
            for (int i = 0; i < m_Stats.Length - 1; i++)
            {
                GUI.Label(m_PositionText, m_Stats[i]);
                m_PositionText.y += m_PositionText.height;
            }
        }
        else
        {
            m_PositionText.height = m_PositionBG.height / 7.0f;
            for (int i = 0; i < m_Stats.Length; i++)
            {
                GUI.Label(m_PositionText, m_Stats[i]);
                m_PositionText.y += m_PositionText.height;
            }
        }

        m_ItemThisFrame = null;
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
