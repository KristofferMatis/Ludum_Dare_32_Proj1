using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
	//Day or night
	public bool m_IsDay = true;

	//Length of day and night
	public const float DAY_LENGTH = 120f;
	public const float NIGHT_LENGTH = 80f;
	public float m_CycleTimer = 0f;

	//Spawner
	HordeSpawner m_Spawner;

	void Start ()
	{
		if (m_IsDay)
		{
			transform.rotation = new Quaternion (-(NIGHT_LENGTH / DAY_LENGTH) / 2f, 0f, 0f, 1f);
		}
		else
		{
			transform.rotation = new Quaternion (-(NIGHT_LENGTH / DAY_LENGTH) * 2f, 0f, 0f, 1f);
		}

		//Since update is called immediatly
		m_IsDay = !m_IsDay;
		m_Spawner = GameObject.FindObjectOfType<HordeSpawner> ().GetComponent<HordeSpawner>();
	}

	// Update is called once per frame
	void Update ()
	{
		//Rotate direction light
		transform.Rotate(new Vector3 (Time.deltaTime * 360f / (DAY_LENGTH + NIGHT_LENGTH), 0f, 0f));

		//Cycle day and night
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
			m_Spawner.SetDay(m_IsDay);
		}
	}
}
