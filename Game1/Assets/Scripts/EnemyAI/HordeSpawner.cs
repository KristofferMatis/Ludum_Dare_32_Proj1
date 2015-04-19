﻿using UnityEngine;
using System.Collections.Generic;

public class HordeSpawner : MonoBehaviour
{
	//The hordes of enemies
	List<HordeController> m_Hordes;
	bool IsDay = true;
	int SpecialSpawnCount = 0;

	//This is a singleton
	public static HordeSpawner m_Instance;

	//The player
	Transform m_PlayerTransform;

	//How far to spawn hordes and enemies
	const float SPAWN_DISTANCE = 10f;
	const float MIN_FIRST_SPAWN_DISTANCE = 40f;
	const float MAX_FIRST_SPAWN_DISTANCE = 60f;

	//Spawning
	const float INITIAL_SPAWN_TIME = 5f;
	float m_MaxTimeBetweenSpawns = INITIAL_SPAWN_TIME;
	float m_TimeSinceLastSpawn = INITIAL_SPAWN_TIME;
	public int BasicSpawnCount = 5;

	//Enemy prefabs
	public List<GameObject> SpawnableEnemies;

	//Horde Game Object
	public GameObject m_HordeGameObject;


	//Iinitialization
	void Start ()
	{
		m_Instance = this;
		m_Hordes = new List<HordeController>();
		HordeController[] hordes = GameObject.FindObjectsOfType<HordeController> ();
		m_Hordes.AddRange (hordes);
		m_PlayerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	//Update is called once per frame
	void Update ()
	{
		if (IsDay && SpawnableEnemies.Count > 0)
		{
			m_TimeSinceLastSpawn -= Time.deltaTime;
			if (m_TimeSinceLastSpawn < 0f)
			{
				//Acclerate
				m_MaxTimeBetweenSpawns--;
				m_TimeSinceLastSpawn = m_MaxTimeBetweenSpawns;

				//Spawn basic horde
				SpawnHorde (BasicSpawnCount, SpawnableEnemies[0], EnemyController.EnemyState.Wander);

				//Spawn specials
				for (int i = 0; i < SpecialSpawnCount; i++)
				{
					m_Hordes[m_Hordes.Count -1].Spawn(SpecialSpawnCount, SpawnableEnemies[Random.Range(0, SpawnableEnemies.Count)], EnemyController.EnemyState.Wander);
				}
			}
		}
	}

	/// <summary>
	/// Sets the day to start or end.
	/// </summary>
	public void SetDay (bool isDay)
	{
		IsDay = isDay;
		BasicSpawnCount++;
		SpecialSpawnCount++;

		//Spawn hordless wandering enemies
		if (SpawnableEnemies.Count > 0)
		{
			SpawnScatteredEnemies (BasicSpawnCount, SpawnableEnemies[0], EnemyController.EnemyState.Wander);
		}

		//Reset time between spawns when the day ends
		if (!IsDay)
		{
			m_MaxTimeBetweenSpawns = INITIAL_SPAWN_TIME;
		}
	}

	/// <summary>
	/// Spawns a horde.
	/// </summary>>
	public void SpawnHorde (int enemyCount, GameObject enemy, EnemyController.EnemyState state)
	{
		HordeController newHorde = GameObject.Instantiate(m_HordeGameObject).GetComponent<HordeController>();
		newHorde.OnCreateHorde ();
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
		if (horde != null && horde.m_Enemies.Count > 0)
		{
			return horde.GetHordePosition () + new Vector3(Random.Range(-SPAWN_DISTANCE, SPAWN_DISTANCE), 0f, Random.Range(-SPAWN_DISTANCE, SPAWN_DISTANCE));
		}
		else
		{
			return m_PlayerTransform.position + Random.Range(MIN_FIRST_SPAWN_DISTANCE, MAX_FIRST_SPAWN_DISTANCE) *
				new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
		}
	}
}
