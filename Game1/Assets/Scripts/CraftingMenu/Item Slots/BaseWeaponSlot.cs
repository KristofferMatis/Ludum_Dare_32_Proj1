using UnityEngine;
using System.Collections;

public class BaseWeaponSlot : ItemSlot
{

	// Use this for initialization
	void Start ()
    {
        INSTRUCTIONS = new string[] {   "WEAPON BASE SLOT:", 
                                        "Accepts ONLY WEAPONS.", 
                                        "WEAPONS determine",
                                        "MOVE SET and total", 
                                        "ATTACHMENT SLOTS."};
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public override bool CanAcceptItem(Item item)
    {
		if (item != null && item.BaseWeapon != null && base.CanAcceptItem(item))
		{
			CraftingMenu.Instance.PlayItemDropped();
            return true;
		}
        else
		{
			CraftingMenu.Instance.PlayDropImpossible();
            return false;
		}
    }
}
