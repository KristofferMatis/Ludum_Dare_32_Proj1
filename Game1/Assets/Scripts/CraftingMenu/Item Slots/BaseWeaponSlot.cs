using UnityEngine;
using System.Collections;

public class BaseWeaponSlot : ItemSlot
{

	// Use this for initialization
	void Start ()
    {
        INSTRUCTIONS = new string[] {   "This is The Weapon Base", 
                                        "Slot, Only Weapons Can", 
                                        "Be Placed Here.", 
                                        "Your Weapon Will Determine",
                                        "Your Attack Type and", 
                                        "How Many Attachment Slots", 
                                        "You Have" };
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
