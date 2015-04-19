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
		if (item != null && item.BaseWeapon != null && base.CanAcceptItem(item))
            return true;
        else
            return false;
    }
}
