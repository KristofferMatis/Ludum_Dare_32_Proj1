using UnityEngine;
using System.Collections;

public class Inventory : Singleton<Inventory> 
{
    WeaponDrop[] m_Inventory = new WeaponDrop[7];

    public WeaponDrop[] getIventory()
    {
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
