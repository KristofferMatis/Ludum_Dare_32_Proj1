using UnityEngine;
using System.Collections.Generic;

public class HordeController : MonoBehaviour
{
	//The enemies for this horde
	public List<EnemyController> m_Enemies;

	//Starting state of horde, wander lets them choose their own starting state
	public EnemyController.EnemyState m_HordeState = EnemyController.EnemyState.Wander;

	//Where the enemies think the player is
	Vector3 m_SearchPosition;
	Transform m_PlayerTransform;

	//Spawner
	HordeSpawner m_Spawner;


	//When this horde is spawned
	public void OnCreateHorde (HordeSpawner spawner)
	{
		m_PlayerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		m_SearchPosition = m_PlayerTransform.position;
		m_Spawner = spawner;
		if (m_Enemies == null)
		{
			m_Enemies = new List<EnemyController>();
		}
		else
		{
			for (int i = 0; i < m_Enemies.Count; i++)
			{
				m_Enemies[i].SetHorde (this);
				m_Enemies[i].SetLeashPosition (GetHordePosition ());
				m_Enemies[i].SetSearchPosition(m_SearchPosition);
			}
			
			//If non wander is selected
			if (m_HordeState != EnemyController.EnemyState.Wander)
			{
				SetEnemiesState(m_HordeState);
			}
		}
	}

	//Tell existing enemies we exist
	void Start ()
	{
		OnCreateHorde (GameObject.FindObjectOfType<HordeSpawner> ().GetComponent<HordeSpawner>());
	}
	
	//Update each enemies awareness of their horde
	void Update ()
	{
		//Define where the horde currently is
		Vector3 hordePos = GetHordePosition ();
		for (int i = 0; i < m_Enemies.Count; i++)
		{
			if (m_Enemies[i] != null)
			{
				m_Enemies[i].SetLeashPosition (hordePos);
			}
		}
	}

	/// <summary>
	/// //Gets the average position of this horde of enemies
	/// </summary>
	public Vector3 GetHordePosition ()
	{
		Vector3 hordePos = Vector3.zero;
		for (int i = 0; i < m_Enemies.Count; i++)
		{
			hordePos += m_Enemies[i].transform.position;
		}
		hordePos /= m_Enemies.Count;
		return hordePos;
	}

	/// <summary>
	/// //Gets the average position of this horde of enemies
	/// </summary>
	public int GetHordeCount ()
	{
		return m_Enemies.Count;
	}
	
	/// <summary>
	/// Spawn a number of enemies of provided type, and starting in a predefined state
	/// </summary>
	public void Spawn(int numberOfEnemies, GameObject enemy, EnemyController.EnemyState state)
	{
		for (int i = 0; i < numberOfEnemies; i++)
		{
			GameObject tempEnemy = GameObject.Instantiate(enemy);
			tempEnemy.transform.position = m_Spawner.GetSpawnPosition(this);
			EnemyController controller = tempEnemy.GetComponent<EnemyController>();
			if (controller != null)
			{
				controller.OnSpawn (m_HordeState);
				controller.SetHorde (this);
				controller.SetSearchPosition(m_SearchPosition);
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
		//Set horde state
		if (m_HordeState == state)
		{
			return;
		}
		m_HordeState = state;

		//Set enemies states
		for (int i = 0; i < m_Enemies.Count; i++)
		{
			if (m_Enemies[i].m_State != state)
			{
				m_Enemies[i].SetState(state);
			}
		}
	}

	/// <summary>
	/// If enemies were wandering, they are now searching
	/// </summary>
	public void OnPlayerFound (Vector3 pos)
	{
		//Set horde state
		if (m_HordeState != EnemyController.EnemyState.Wander)
		{
			return;
		}
		m_HordeState = EnemyController.EnemyState.Search;
		
		//Set enemies states
		for (int i = 0; i < m_Enemies.Count; i++)
		{
			if (m_Enemies[i].m_State == EnemyController.EnemyState.Wander)
			{
				m_Enemies[i].SetState(EnemyController.EnemyState.Search);
			}
		}

		//Tell enemies where the player is
		BroadcstPlayerPosition (pos);
	}

	/// <summary>
	/// If enemies were wandering, they are now searching
	/// </summary>
	public void BroadcstPlayerPosition (Vector3 pos)
	{
		//If we alkready have the position
		if (m_SearchPosition == pos)
		{
			return;
		}

		//Set enemies states
		for (int i = 0; i < m_Enemies.Count; i++)
		{
			m_Enemies[i].SetSearchPosition(pos);
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
			enemy.SetHorde (this);
			m_Enemies.Add(enemy);
		}
	}

	/// <summary>
	/// Removes an enemy fron the list.
	/// </summary>
	public void RemoveEnemy (EnemyController enemy)
	{
		if (m_Enemies != null && enemy != null)
		{
			enemy.SetHorde (null);
			m_Enemies.Remove(enemy);
			if (m_Enemies.Count == 0)
			{
				Destroy(gameObject);
			}
		}
	}

	/// <summary>
	/// Clears the horde and removes itself. Letting the enemies run free
	/// </summary>
	public void ClearHorde ()
	{
		//Set the enemies free
		for (int i = 0; i < m_Enemies.Count; i++)
		{
			m_Enemies[i].SetHorde (null);
		}
		m_Enemies.Clear ();
		Destroy(gameObject);
	}
}
