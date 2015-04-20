using UnityEngine;
using System.Collections;

public class CraftingTable : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerStay(Collider otherCollider)
	{
		if(otherCollider.CompareTag(Constants.PLAYER_TAG) && InputManager.Instance.PlayerInteract())
		{
			CraftingMenu.Instance.m_CraftingCamera.SetIsOn(true);
		}
	}
}
