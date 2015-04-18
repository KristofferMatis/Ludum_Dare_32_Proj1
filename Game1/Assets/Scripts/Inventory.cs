using UnityEngine;
using System.Collections;

public class Inventory : Singleton<Inventory> 
{
    public GameObject i_AttachmentPrefab;
    public GameObject i_BaseWeaponPrefab;
    BaseAttachment[] m_Inventory = new BaseAttachment[7];

	// Use this for initialization
	void Awake () 
    {
	    for(int i = 0; i < m_Inventory.Length; i ++)
        {
            m_Inventory[i] = GameObject.Instantiate((Random.Range(0, 2) > 0) ? i_AttachmentPrefab : i_BaseWeaponPrefab).GetComponentInChildren<BaseAttachment>();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public BaseAttachment[] getIventory()
    {
        //TODO: Implement This
        return m_Inventory;
    }
}
