using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
	public bool m_IsDay = true;

	const float DAY_LENGTH = 10f;
	const float NIGHT_LENGTH = 10f;
	float m_CycleTimer = DAY_LENGTH;



	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(new Vector3 (Time.deltaTime, 0f, 0f));
		m_CycleTimer -= Time.deltaTime;
		if (m_CycleTimer < 0f)
		{
			m_IsDay = !m_IsDay;
			if (m_IsDay)
			{
				m_CycleTimer = DAY_LENGTH;
			}
			else
			{
				m_CycleTimer = NIGHT_LENGTH;
			}
			HordeSpawner.m_Instance.SetDay(m_IsDay);
		}
	}
}
