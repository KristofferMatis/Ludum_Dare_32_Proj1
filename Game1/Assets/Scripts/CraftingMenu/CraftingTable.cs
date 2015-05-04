using UnityEngine;
using System.Collections;

public class CraftingTable : InteractiveObject 
{
	protected override void PlayerInteract (PlayerMovement player)
	{
		CraftingMenu.Instance.m_CraftingCamera.SetIsOn(true);
	}
}
