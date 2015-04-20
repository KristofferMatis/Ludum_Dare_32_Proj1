using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boat : MonoBehaviour 
{
	public int m_NumberOfStates;
	int m_CurrentState;

	public List<GameObject> m_Chunks;
	List<Transform> m_MountPoints = new List<Transform>();

	void Start()
	{
		foreach(GameObject chunk in m_Chunks)
		{
			m_MountPoints.Add (chunk.transform.FindChild("Dummy003"));
		}
	}

	public void BuildUp(GameObject gameObjectToMountOnBoat)
	{
		gameObjectToMountOnBoat.transform.parent = m_MountPoints [m_CurrentState];
		gameObjectToMountOnBoat.transform.localPosition = Vector3.zero;
		gameObjectToMountOnBoat.transform.localRotation = Quaternion.identity;

		m_CurrentState++;

		if (m_CurrentState >= m_NumberOfStates)
		{
			// TODO: Win screen -> raft + player floating away ;-)
		}
		else
		{
			m_Chunks[m_CurrentState].SetActive (true);
		}
	}
}
