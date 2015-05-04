using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour
{
	PlayerMovement m_Player;
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Player && InputManager.Instance.PlayerInteract())
		{
			PlayerInteract(m_Player);
		}
	}

	protected virtual void PlayerInteract(PlayerMovement player)
	{

	}
	
	void OnTriggerEnter(Collider otherCollider)
	{
		if(otherCollider.CompareTag(Constants.PLAYER_TAG))
		{
			m_Player = otherCollider.GetComponent<PlayerMovement>();

			ExclamationMark.Instance.DisplayMark(true);
		}
	}
	
	void OnTriggerExit(Collider otherCollider)
	{
		if(otherCollider.CompareTag(Constants.PLAYER_TAG))
		{
			m_Player = null;

			ExclamationMark.Instance.DisplayMark (false);
		}
	}
}
