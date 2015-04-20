using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
	//Day or night
	public bool m_IsDay = true;

	//Length of day and night
	public const float DAY_LENGTH = 120f;
	public const float NIGHT_LENGTH = 80f;
	public float m_TimeMultiplier = 1f;
	float m_CycleTimer = 0f;

	WeaponsSpawningManager m_WeaponSpawner;
	
	//Spawner
	HordeSpawner[] m_Spawners;


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

		//Find objects
		m_Spawners = GameObject.FindObjectsOfType<HordeSpawner> ();
		m_WeaponSpawner = GameObject.FindObjectOfType<WeaponsSpawningManager> ();
	}

	// Update is called once per frame
	void Update ()
	{
		//Rotate direction light
		transform.Rotate(new Vector3 (Time.deltaTime * m_TimeMultiplier * 360f / (DAY_LENGTH + NIGHT_LENGTH), 0f, 0f));

		//Cycle day and night
		m_CycleTimer -= Time.deltaTime * m_TimeMultiplier;
		if (m_CycleTimer < 0f)
		{
			//Set day
			m_IsDay = !m_IsDay;

			if (m_IsDay)
			{
				m_CycleTimer = DAY_LENGTH;
			}
			else
			{
				m_CycleTimer = NIGHT_LENGTH;
			}

			//Weapon spawns
			if (m_WeaponSpawner != null)
			{
				if (m_IsDay)
				{
					m_WeaponSpawner.SpawnNewItems();
				}
				else
				{
					m_CycleTimer = NIGHT_LENGTH;
				}
			}
			//Enemy spawners
			if (m_Spawners.Length > 0)
			{
				//Tell all spawners the day
				for (int i = 0; i < m_Spawners.Length; i++)
				{
					m_Spawners[i].SetDay(m_IsDay);
				}
			}
		}
	}
}
