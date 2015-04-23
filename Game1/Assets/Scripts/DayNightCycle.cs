using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
	public GameObject m_DayLight;
	public GameObject m_NightLight;

	//Day or night
	public bool m_IsDay = true;

	//Length of day and night
	public const float DAY_LENGTH = 240f;
	public const float NIGHT_LENGTH = 30f;
	public float m_TimeMultiplier = 1f;
	float m_CycleTimer = 0f;

	public float m_DayStartAngle = 45.0f;

	float m_DayTimeRotationSpeed;
	float m_NightTimeRotationSpeed;

	public float m_TransitionRotationSpeed;

	WeaponsSpawningManager m_WeaponSpawner;
	
	//Spawner
	HordeSpawner[] m_Spawners;

	bool m_Transitioning;

	float m_CurrentAngle;
	float m_TargetAngle;


	void Start ()
	{
		if (m_IsDay)
		{
			m_DayLight.transform.eulerAngles = new Vector3(m_DayStartAngle, 0.0f, 0.0f);
			m_DayLight.SetActive (true);
			m_NightLight.SetActive (false);

			m_CycleTimer = DAY_LENGTH;
		}
		else
		{
			m_NightLight.transform.eulerAngles = new Vector3(m_DayStartAngle, 0.0f, 0.0f);
			m_NightLight.SetActive (true);
			m_DayLight.SetActive (false);
			
			m_CycleTimer = NIGHT_LENGTH;
		}

		//Find objects
		m_Spawners = GameObject.FindObjectsOfType<HordeSpawner> ();
		m_WeaponSpawner = GameObject.FindObjectOfType<WeaponsSpawningManager> ();

		m_DayTimeRotationSpeed = Mathf.PI / DAY_LENGTH;
		m_NightTimeRotationSpeed = Mathf.PI / NIGHT_LENGTH;
	}

	// Update is called once per frame
	void Update ()
	{
		if(!m_Transitioning)
		{
			if(m_IsDay)
			{
				//Rotate direction light
				m_DayLight.transform.Rotate(new Vector3 (m_DayTimeRotationSpeed * Time.deltaTime, 0f, 0f));
				m_NightLight.transform.eulerAngles = new Vector3(m_DayLight.transform.eulerAngles.x + 180.0f, 0.0f, 0.0f);
			}
			else
			{
				//Rotate direction light
				m_NightLight.transform.Rotate(new Vector3 (m_NightTimeRotationSpeed * Time.deltaTime, 0f, 0f));
				m_DayLight.transform.eulerAngles = new Vector3(m_NightLight.transform.eulerAngles.x + 180.0f, 0.0f, 0.0f);
			}
		}
		else
		{
			//Rotate direction light
			m_DayLight.transform.Rotate(new Vector3 (m_TransitionRotationSpeed * Time.deltaTime, 0f, 0f));
			m_NightLight.transform.eulerAngles = new Vector3(m_DayLight.transform.eulerAngles.x + 180.0f, 0.0f, 0.0f);

			m_CurrentAngle += m_TransitionRotationSpeed * Time.deltaTime;

			if(m_CurrentAngle > m_TargetAngle)
			{
				if(m_IsDay)
				{
					m_NightLight.SetActive(false);
				}
				else
				{
					m_DayLight.SetActive(false);
				}

				m_Transitioning = false;
			}
		}

		//Cycle day and night
		m_CycleTimer -= Time.deltaTime * m_TimeMultiplier;
		if (m_CycleTimer < 0f)
		{
			//Set day
			FinishDay ();

			//Weapon spawns
			if (m_WeaponSpawner != null)
			{
				if (m_IsDay)
				{
					m_WeaponSpawner.SpawnPlane(this);
				}
				else
				{
					m_WeaponSpawner.StopSmoke();
				}
			}
		}
	}

	public void SpawnEnemies()
	{
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

	[ContextMenu("Finish day")]
	public void FinishDay()
	{
		m_IsDay = !m_IsDay;

		m_CycleTimer = m_IsDay ? DAY_LENGTH : NIGHT_LENGTH;

		m_Transitioning = true;

		if(m_IsDay)
		{
			m_DayLight.SetActive(true);
			m_CurrentAngle = m_DayLight.transform.eulerAngles.x;
		}
		else
		{
			m_NightLight.SetActive(true);
			m_CurrentAngle = m_NightLight.transform.eulerAngles.x;
		}

		int numberOfTurns = (int)m_CurrentAngle / 360;
		m_TargetAngle = (numberOfTurns + 1) * 360.0f + m_DayStartAngle;
	}
}
