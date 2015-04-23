using UnityEngine;
using System.Collections;

public class EnemyWaveManager : Singleton<EnemyWaveManager> 
{
	public float MaxNumberOfEnemies 
	{
		get;
		protected set;
	}

	public float NumberOfEnemies 
	{
		get;
		protected set;
	}

	public void AddEnemy()
	{
		NumberOfEnemies++;
		MaxNumberOfEnemies++;
	}

	public void RemoveEnemy()
	{
		NumberOfEnemies--;
	}
}
