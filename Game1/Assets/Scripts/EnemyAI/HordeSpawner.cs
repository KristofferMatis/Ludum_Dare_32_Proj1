﻿using UnityEngine;
using System.Collections.Generic;

public class HordeSpawner : MonoBehaviour
{
	//The hordes of enemies
	List<HordeController> m_Hordes;
	bool IsDay = false;

	//The player
	Transform m_PlayerTransform;

	//How far to spawn hordes and enemies
	const float SPAWN_DISTANCE_FROM_HORDE = 35f;
	const float MIN_FIRST_SPAWN_DISTANCE = 100f;
	const float MAX_FIRST_SPAWN_DISTANCE = 135f;

	//Spawning
	const float INITIAL_SPAWN_TIME = 30f;
	const float MIN_SPAWN_TIME = 15f;
	float m_MaxTimeBetweenSpawns = INITIAL_SPAWN_TIME;
	float m_TimeSinceLastSpawn = INITIAL_SPAWN_TIME;
	public int BasicSpawnCount = 4;
	public int SpecialSpawnCount = 0;
	public int DifficultyAdjustment = 2;

	const int MAX_ENEMIES = 50;

	//Enemy prefabs
	public List<GameObject> SpawnableEnemies;

	public bool ScatterOnly = false;

	//Horde Game Object
	GameObject m_HordeGameObject;

	int m_NumberOfWaveSpawned;

	int m_EnemiesInAWave = 0;


	//Iinitialization
	void Start ()
	{
		m_Hordes = new List<HordeController>();
		HordeController[] hordes = GameObject.FindObjectsOfType<HordeController> ();
		m_Hordes.AddRange (hordes);
		m_PlayerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		m_HordeGameObject = (GameObject)Resources.Load<GameObject> ("Prefabs/Enemies/BaseHorde"); 
	}
	
	//Update is called once per frame
	void Update ()
	{
		if (IsDay && SpawnableEnemies.Count > 0)
		{
			m_TimeSinceLastSpawn -= Time.deltaTime;
			if (m_TimeSinceLastSpawn < 0f)
			{
				//Acclerate spawns as day goes on
				if (m_MaxTimeBetweenSpawns > MIN_SPAWN_TIME)
				{
					m_MaxTimeBetweenSpawns -= DifficultyAdjustment;
				}
				m_TimeSinceLastSpawn = m_MaxTimeBetweenSpawns;

				int count = 0;
				for (int i = 0; i < m_Hordes.Count; i++)
				{
					count += m_Hordes[i].GetHordeCount();
				}
				if (EnemyWaveManager.Instance.NumberOfSpawnedEnemies >= EnemyWaveManager.Instance.MaxNumberOfEnemies)
				{
					return;
				}

				SpawnScatteredEnemies (BasicSpawnCount, SpawnableEnemies[0], EnemyController.EnemyState.Wander);

				if(!ScatterOnly)
				{
					//Spawn basic horde
					SpawnHorde (BasicSpawnCount, SpawnableEnemies[0], EnemyController.EnemyState.Wander);

					//Spawn specials
					for (int i = 0; i < SpecialSpawnCount; i++)
					{
						m_Hordes[m_Hordes.Count -1].Spawn(SpecialSpawnCount, SpawnableEnemies[Random.Range(0, SpawnableEnemies.Count)], EnemyController.EnemyState.Wander);
					}
				}

				EnemyWaveManager.Instance.NumberOfSpawnedEnemies += m_EnemiesInAWave;
			}
		}
	}

	/// <summary>
	/// Sets the day to start or end.
	/// </summary>
	public void SetDay (bool isDay)
	{
		IsDay = isDay;

		if(IsDay)
		{
			if (SpawnableEnemies.Count > 0)
			{			
				m_EnemiesInAWave = 0;

				BasicSpawnCount += DifficultyAdjustment;
				SpecialSpawnCount += DifficultyAdjustment;

				GameObject enemyPrefab = null;

				//Spawn basic horde
				if (!ScatterOnly)
				{
					enemyPrefab = SpawnableEnemies[0];
					SpawnHorde (BasicSpawnCount, enemyPrefab, EnemyController.EnemyState.Wander);

					if(enemyPrefab.GetComponent<EnemyController>().m_AffectsEnemyCount)
					{
						m_EnemiesInAWave += BasicSpawnCount;
					}

					//Spawn specials
					for (int i = 0; i < SpecialSpawnCount; i++)
					{
						enemyPrefab = SpawnableEnemies[Random.Range(0, SpawnableEnemies.Count)];
						m_Hordes[m_Hordes.Count -1].Spawn(SpecialSpawnCount, enemyPrefab, EnemyController.EnemyState.Wander);

						if(enemyPrefab.GetComponent<EnemyController>().m_AffectsEnemyCount)
						{
							m_EnemiesInAWave += SpecialSpawnCount;
						}
					}
				}

				enemyPrefab = SpawnableEnemies[0];
				SpawnScatteredEnemies (BasicSpawnCount, enemyPrefab, EnemyController.EnemyState.Wander);

				if(enemyPrefab.GetComponent<EnemyController>().m_AffectsEnemyCount)
				{
					m_EnemiesInAWave += BasicSpawnCount;
				}
			}

			EnemyWaveManager.Instance.MaxNumberOfEnemies += EnemyWaveManager.Instance.NumberOfWaves * m_EnemiesInAWave;

			EnemyWaveManager.Instance.NumberOfSpawnedEnemies += m_EnemiesInAWave;

			//Reset time between spawns when the day ends
			if (IsDay)
			{
				m_MaxTimeBetweenSpawns = INITIAL_SPAWN_TIME;
			}
		}
	}

	/// <summary>
	/// Spawns a horde.
	/// </summary>>
	public void SpawnHorde (int enemyCount, GameObject enemy, EnemyController.EnemyState state)
	{
		HordeController newHorde = GameObject.Instantiate(m_HordeGameObject).GetComponent<HordeController>();
		newHorde.OnCreateHorde (this);
		newHorde.Spawn (enemyCount, enemy, state);
		m_Hordes.Add (newHorde);
	}

	/// <summary>
	/// Spawns a mix of enemies that do not act as a horde
	/// </summary>>
	public void SpawnScatteredEnemies (int enemyCount, GameObject enemy, EnemyController.EnemyState state)
	{
		for (int i = 0; i < enemyCount; i++)
		{
			GameObject tempEnemy = GameObject.Instantiate(enemy);
			tempEnemy.transform.position = GetSpawnPosition();
			EnemyController controller = tempEnemy.GetComponent<EnemyController>();
			if (controller != null)
			{
				controller.OnSpawn (controller.m_State);
				controller.SetHorde (null);
				controller.SetSearchPosition(m_PlayerTransform.position);
			}
			else
			{
				Destroy(tempEnemy);
				return;
			}
		}
	}
	
	//Gets where to spawn the ennemy
	public Vector3 GetSpawnPosition (HordeController horde = null)
	{
		Vector3 pos = Vector3.zero;
		if (horde != null && horde.m_Enemies.Count > 0)
		{
			pos = horde.GetHordePosition () + new Vector3(Random.Range(-SPAWN_DISTANCE_FROM_HORDE, SPAWN_DISTANCE_FROM_HORDE), 0f, Random.Range(-SPAWN_DISTANCE_FROM_HORDE, SPAWN_DISTANCE_FROM_HORDE));
		}
		else
		{
			pos = m_PlayerTransform.position + Random.Range(MIN_FIRST_SPAWN_DISTANCE, MAX_FIRST_SPAWN_DISTANCE) *
				new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
		}
		NavMeshHit hit = new NavMeshHit();
		if (NavMesh.FindClosestEdge (pos, out hit, NavMesh.AllAreas))
		{
			pos = hit.position;
		}

		//Try again
		if (Vector3.Distance(pos, m_PlayerTransform.position) < MIN_FIRST_SPAWN_DISTANCE)
		{
			pos = GetSpawnPosition (horde);
		}
		return pos;
	}
}
