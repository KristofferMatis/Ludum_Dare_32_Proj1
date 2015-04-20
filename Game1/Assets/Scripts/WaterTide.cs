using UnityEngine;
using System.Collections;

public class WaterTide : MonoBehaviour
{
	const float HEIGHT_CHANGE = 16.75f;
	Vector3 m_InitialPosition;
	float m_Timer = 0.0f;

	void Start ()
	{
		m_InitialPosition = transform.position;
	}

	void Update ()
	{
		m_Timer += Time.deltaTime;
		transform.position = m_InitialPosition - Vector3.up * HEIGHT_CHANGE * (0.5f - 0.5f * Mathf.Cos (m_Timer / (DayNightCycle.DAY_LENGTH + DayNightCycle.NIGHT_LENGTH) * 2.0f * Mathf.PI));
	}

	void OnTriggerEnter (Collider collider)
	{
		//Check if they are too below us and kill them <- Done in Health ;-)
	}
}
