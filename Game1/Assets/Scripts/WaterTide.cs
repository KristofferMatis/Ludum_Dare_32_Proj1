using UnityEngine;
using System.Collections;

public class WaterTide : MonoBehaviour
{
	public float m_TopPosition;
	public float m_BottomPosition;

	public float m_TideSpeed;
	public float m_WaveSpeed;

	public float m_WaveHeight;

	float m_Timer;

	float m_CurrentWaveCenter;

	DayNightCycle m_DayNightCycle;

	void Start()
	{
		m_DayNightCycle = FindObjectOfType<DayNightCycle> ();

		m_CurrentWaveCenter = transform.position.y;
	}

	void Update ()
	{
		float targetCenter = m_TopPosition;

		if(m_DayNightCycle.m_IsDay)
		{
			if(EnemyWaveManager.Instance.MaxNumberOfEnemies > 0)
			{
				targetCenter = Mathf.Lerp(m_BottomPosition, m_TopPosition, (1.0f - EnemyWaveManager.Instance.NumberOfEnemies / EnemyWaveManager.Instance.MaxNumberOfEnemies));
			}
			else
			{
				targetCenter = m_BottomPosition;
			}
		}

		m_CurrentWaveCenter = Mathf.Lerp (m_CurrentWaveCenter, targetCenter, m_TideSpeed * Time.deltaTime);

		m_Timer += Time.deltaTime * m_WaveSpeed;

		Vector3 newPosition = transform.position;
		newPosition.y = m_CurrentWaveCenter + m_WaveHeight * Mathf.Cos (m_Timer);
		transform.position = newPosition;
	}
}
