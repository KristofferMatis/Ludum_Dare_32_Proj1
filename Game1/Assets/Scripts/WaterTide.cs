using UnityEngine;
using System.Collections;

public class WaterTide : MonoBehaviour
{
	const float HEIGHT_CHANGE = 1.5f;
	Vector3 m_InitialPosition;
	float m_Timer = 1f;

	void Start ()
	{
		m_InitialPosition = transform.position;
	}

	void Update ()
	{
		m_Timer += Time.deltaTime * 12f / (DayNightCycle.DAY_LENGTH + DayNightCycle.NIGHT_LENGTH);
		transform.position = m_InitialPosition - Vector3.up * HEIGHT_CHANGE * Mathf.Sin (m_Timer);
	}

	void OnTriggerEnter (Collider collider)
	{
		//Check if they are too below us and kill them
	}
}
