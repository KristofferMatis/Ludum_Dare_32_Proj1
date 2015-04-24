﻿using UnityEngine;
using System.Collections;

public class EnemyWaveManager : Singleton<EnemyWaveManager> 
{
	//Spawner
	HordeSpawner[] m_Spawners;

	public float MaxNumberOfEnemies 
	{
		get;
		set;
	}

	public float NumberOfEnemies 
	{
		get;
		protected set;
	}

	public float NumberOfWaves 
	{
		get;
		protected set;
	}

	int m_Day;

	float m_PreviousNumberOfWaves;

	public void AddEnemy()
	{
		NumberOfEnemies++;
		MaxNumberOfEnemies++;
	}

	public void RemoveEnemy()
	{
		NumberOfEnemies--;
	}

	void Start()
	{		
		m_Spawners = GameObject.FindObjectsOfType<HordeSpawner> ();
	}

	public void SpawnEnemies()
	{
		m_Day++;

		CalculateNumberOfEnemiesThisDay ();

		//Enemy spawners
		if (m_Spawners.Length > 0)
		{
			//Tell all spawners the day
			for (int i = 0; i < m_Spawners.Length; i++)
			{
				m_Spawners[i].SetDay(true);
			}
		}
	}

	void CalculateNumberOfEnemiesThisDay()
	{
		if(m_Day == 1)
		{
			NumberOfWaves = 1;
		}
		else if(m_Day == 2)
		{
			NumberOfWaves = 2;
		}
		else
		{
			NumberOfWaves += m_PreviousNumberOfWaves;
		}
		
		m_PreviousNumberOfWaves = NumberOfWaves;
	}
}
