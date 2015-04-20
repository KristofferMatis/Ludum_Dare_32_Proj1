using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoatManager : MonoBehaviour 
{
	Boat m_Boat;

	public List<string> m_ObjectTypesPossible;
	Dictionary<string, bool> m_ObjectTypesNecessary = new Dictionary<string, bool>();

	public Dictionary<string, bool> ObjectsNecessary
	{
		get
		{
			return m_ObjectTypesNecessary;
		}
	}

	public List<string> ObjectsStillNecessary
	{
		get
		{
			List<string> result = new List<string>();

			foreach(string type in m_ObjectTypesNecessary.Keys)
			{
				if(m_ObjectTypesNecessary[type])
				{
					result.Add (type);
				}
			}

			return result;
		}
	}

	// Use this for initialization
	void Start ()
	{
		m_Boat = GetComponent<Boat> ();

		for(int i = 0; i < m_Boat.m_NumberOfStates; i++)
		{
			int random = Random.Range (0, m_ObjectTypesPossible.Count);
			m_ObjectTypesNecessary.Add (m_ObjectTypesPossible[random], true);
			m_ObjectTypesPossible.RemoveAt(random);
		}
	}

	public bool IsObjectTypeNecessary(string type)
	{
		return (m_ObjectTypesNecessary.ContainsKey (type) && m_ObjectTypesNecessary [type]);
	}

	public void BuildUpBoat(string type, GameObject objectToAddToBoat)
	{
		m_ObjectTypesNecessary [type] = false;

		m_Boat.BuildUp (objectToAddToBoat);
	}
}
