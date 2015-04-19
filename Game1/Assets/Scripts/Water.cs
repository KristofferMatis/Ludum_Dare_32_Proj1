using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour
{
	const float HEIGHT_CHANGE = 20f;
	Vector3 m_InitialPosition;
	public DayNightCycle m_Cycle;
	float m_Timer = 0f;

	void Start ()
	{
		m_InitialPosition = transform.position;
		if (m_Cycle.m_IsDay)
		{
			transform.position += Vector3.up * HEIGHT_CHANGE;
		}
		else
		{
			transform.position -= Vector3.up * HEIGHT_CHANGE;
		}
	}

	void Update ()
	{
		if (m_Cycle.m_IsDay)
		{
			transform.position = Vector3.Lerp (m_InitialPosition + Vector3.up * HEIGHT_CHANGE, m_InitialPosition - Vector3.up * HEIGHT_CHANGE, m_Cycle.m_CycleTimer / DayNightCycle.DAY_LENGTH);
		}
		else
		{
			transform.position = Vector3.Lerp (m_InitialPosition - Vector3.up * HEIGHT_CHANGE, m_InitialPosition + Vector3.up * HEIGHT_CHANGE, m_Cycle.m_CycleTimer / DayNightCycle.NIGHT_LENGTH);
		}
	}

	void OnTriggerENter (Collider collider)
	{
		//Check if they are too below us and kill them
	}
}
