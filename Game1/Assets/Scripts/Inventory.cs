using UnityEngine;
using System.Collections;

public class Inventory : Singleton<Inventory> 
{
    public Vector3 i_OriginalItemHidingSpot = new Vector3(-5000.0f, -5000.0f, -5000.0f);

    WeaponDrop[] m_Inventory = new WeaponDrop[7];

	// Use this for initialization
	void Awake () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public WeaponDrop[] getIventory()
    {
        //TODO: Implement This
        return m_Inventory;
    }

    public bool setAttachment(WeaponDrop drop)
    {
        for(int i = 0; i < m_Inventory.Length; i++)
        {
            if(m_Inventory[i] == null)
            {
                m_Inventory[i] = drop;
                return true;
            }
        }
        return false;
    }

    public void setAttachment(WeaponDrop drop, int index)
    {
        if(index >= 0 && index < m_Inventory.Length)
            m_Inventory[index] = drop;
    }
}
