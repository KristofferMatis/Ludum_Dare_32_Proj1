﻿using UnityEngine;
using System.Collections.Generic;

public class HordeController : MonoBehaviour
{
	//The enemies for this horde
	List<EnemyController> m_Enemies;
	
	//Update each enemies awareness of their horde
	void Update ()
	{
		//Define where the horde currently is
		Vector3 hordePos = Vector3.zero;
		for (int i = 0; i < m_Enemies.Count; i++)
		{
			hordePos += m_Enemies[i].transform.position;
		}
		hordePos /= m_Enemies.Count;
		for (int i = 0; i < m_Enemies.Count; i++)
		{
			if (m_Enemies[i] != null)
			{
				m_Enemies[i].SetLeashPosition (hordePos);
			}
		}
	}
	
	/// <summary>
	/// Spawn a number of enemies of provided type, and starting in a predefined state
	/// </summary>
	public void Spawn(int numberOfEnemies, GameObject enemy, EnemyController.EnemyState state)
	{
		for (int i = 0; i < numberOfEnemies; i++)
		{
			GameObject tempEnemy = GameObject.Instantiate(enemy);
			EnemyController controller = tempEnemy.GetComponent<EnemyController>();
			if (controller != null)
			{
				controller.OnSpawn (state);
				controller.m_Horde = this;
				m_Enemies.Add(controller);
			}
			else
			{
				Destroy(tempEnemy);
				return;
			}
		}
	}

	/// <summary>
	/// Sets the state of all the enemies at once.
	/// </summary>
	public void SetEnemiesState (EnemyController.EnemyState state)
	{
		for (int i = 0; i < m_Enemies.Count; i++)
		{
			m_Enemies[i].SetState(state);
		}
	}

	/// <summary>
	/// Adds an enemy to the list.
	/// </summary>
	public void AddEnemy (EnemyController enemy)
	{
		if (m_Enemies == null)
		{
			m_Enemies = new List<EnemyController>();
		}
		if (enemy != null)
		{
			m_Enemies.Add(enemy);
		}
	}
}