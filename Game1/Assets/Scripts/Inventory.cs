using UnityEngine;
using System.Collections;

public class Inventory : Singleton<Inventory> 
{
    public GameObject i_ItemPrefab;
    Item[] m_Items = new Item[7];

	// Use this for initialization
	void Awake () 
    {
	    for(int i = 0; i < m_Items.Length; i ++)
        {
            m_Items[i] = GameObject.Instantiate(i_ItemPrefab).GetComponentInChildren<Item>();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public Item[] getIventory()
    {
        //TODO: Implement This
        return m_Items;
    }
}
