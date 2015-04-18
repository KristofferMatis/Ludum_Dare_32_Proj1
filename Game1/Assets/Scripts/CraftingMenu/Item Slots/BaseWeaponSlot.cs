using UnityEngine;
using System.Collections;

public class BaseWeaponSlot : ItemSlot
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public override bool CanAcceptItem(Item item)
    {
        if (item.BaseWeapon != null)
            return true;
        else
            return false;
    }
}
