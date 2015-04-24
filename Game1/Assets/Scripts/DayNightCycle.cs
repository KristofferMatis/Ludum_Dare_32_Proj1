using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
	public GameObject m_DayLight;
	public GameObject m_NightLight;

	//Day or night
	public bool m_IsDay = true;

	//Length of day and night
	public float m_DayLength = 240f;
	public float m_NightLength = 30f;
	float m_CycleTimer = 0f;

	public float m_SunRiseAngle = 45.0f;
	float m_DayStartAngle;
	float m_DayEndAngle;

	float m_DayTimeRotationSpeed;
	float m_NightTimeRotationSpeed;

	public float m_TransitionRotationSpeed;

	WeaponsSpawningManager m_WeaponSpawner;

	bool m_Transitioning;

	float m_CurrentAngle;


	void Start ()
	{		
		//Find objects
		m_WeaponSpawner = GameObject.FindObjectOfType<WeaponsSpawningManager> ();

		m_DayStartAngle = m_SunRiseAngle;
		m_DayEndAngle = 180.0f - m_SunRiseAngle;
		m_CurrentAngle = m_DayStartAngle;

		if (m_IsDay)
		{
			m_DayLight.transform.eulerAngles = new Vector3(m_CurrentAngle, 0.0f, 0.0f);
			m_DayLight.SetActive (true);
			m_NightLight.SetActive (false);

			m_CycleTimer = m_DayLength;

			if(m_WeaponSpawner)
			{
				m_WeaponSpawner.SpawnPlane();
			}
		}
		else
		{
			m_NightLight.transform.eulerAngles = new Vector3(m_CurrentAngle, 0.0f, 0.0f);
			m_NightLight.SetActive (true);
			m_DayLight.SetActive (false);
			
			m_CycleTimer = m_NightLength;

			if(m_WeaponSpawner)
			{
				m_WeaponSpawner.StopSmoke();
			}
		}

		m_DayTimeRotationSpeed = (180.0f - 2.0f * m_SunRiseAngle) / m_DayLength;
		m_NightTimeRotationSpeed = (180.0f - 2.0f * m_SunRiseAngle) / m_NightLength;
	}

	// Update is called once per frame
	void Update ()
	{
		if(!m_Transitioning)
		{
			if(m_IsDay)
			{
				float lerpAmount = EnemyWaveManager.Instance.MaxNumberOfEnemies > 0 ? 1.0f - (EnemyWaveManager.Instance.NumberOfEnemies / EnemyWaveManager.Instance.MaxNumberOfEnemies) : 0.0f;
				float angleForRemainingEnemies = Mathf.Lerp (m_DayStartAngle, m_DayEndAngle, lerpAmount);
				m_CurrentAngle = Mathf.Lerp (m_CurrentAngle, angleForRemainingEnemies, m_DayTimeRotationSpeed * Time.deltaTime);
			}
			else
			{
				float lerpAmount = 1.0f - m_CycleTimer / m_NightLength;
				float angleForRemainingEnemies = Mathf.Lerp (m_DayStartAngle, m_DayEndAngle, lerpAmount);
				m_CurrentAngle = Mathf.Lerp (m_CurrentAngle, angleForRemainingEnemies, m_NightTimeRotationSpeed * Time.deltaTime);
			}
		}
		else
		{
			m_CurrentAngle += m_TransitionRotationSpeed * Time.deltaTime;

			if(m_CurrentAngle > m_DayStartAngle)
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
		
		//Rotate direction light
		m_DayLight.transform.eulerAngles = new Vector3 (m_CurrentAngle, 0f, 0f);
		m_NightLight.transform.forward = - m_DayLight.transform.forward;

		if(m_IsDay && m_CurrentAngle > m_DayEndAngle - 1.0f)
		{
			FinishDay ();
		}
	}

	[ContextMenu("Finish day")]
	public void FinishDay()
	{
		m_IsDay = !m_IsDay;

		m_CycleTimer = m_IsDay ? m_DayLength : m_NightLength;

		m_Transitioning = true;

		if(m_IsDay)
		{
			m_DayLight.SetActive(true);
		}
		else
		{
			m_NightLight.SetActive(true);
		}

		int numberOfTurns = (int)m_CurrentAngle / 360;
		m_DayStartAngle = (numberOfTurns + 1) * 360.0f + m_SunRiseAngle;
		m_DayEndAngle = (numberOfTurns + 1) * 360.0f + 180.0f - m_SunRiseAngle;

		if(!m_IsDay)
		{
			m_DayStartAngle -= 180.0f;
			m_DayEndAngle -= 180.0f;
		}

		if(m_WeaponSpawner)
		{
			if(m_IsDay)
			{
				m_WeaponSpawner.SpawnPlane();
			}
			else
			{
				m_WeaponSpawner.StopSmoke();
			}
		}
	}
}
