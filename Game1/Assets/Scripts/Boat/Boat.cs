using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boat : InteractiveObject 
{
	int m_CurrentState;

	public List<GameObject> m_Chunks;
	List<Transform> m_MountPoints = new List<Transform>();

	bool m_IsDone = false;

	void Start()
	{
		foreach(GameObject chunk in m_Chunks)
		{
			m_MountPoints.Add (chunk.transform.FindChild("Dummy003"));
		}
	}

	public void BuildUp(GameObject gameObjectToMountOnBoat)
	{
		if(!m_IsDone)
		{
			gameObjectToMountOnBoat.transform.parent = m_MountPoints [m_CurrentState];
			gameObjectToMountOnBoat.transform.localPosition = Vector3.zero;
			gameObjectToMountOnBoat.transform.localRotation = Quaternion.identity;

			m_CurrentState++;

			if (m_CurrentState >= m_Chunks.Count)
			{
				m_IsDone = true;
			}
			else
			{
				m_Chunks[m_CurrentState].SetActive (true);
			}
		}
	}

	protected override void PlayerInteract (PlayerMovement player)
	{
		if(m_IsDone)
		{
			WinSequenceManager.Instance.StartWinSequence();
		}
	}
}
