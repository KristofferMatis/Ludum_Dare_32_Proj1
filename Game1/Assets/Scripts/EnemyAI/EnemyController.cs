﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	//Distances
	const float CHASE_DISTANCE = 40.0f;
	const float LEASH_DISTANCE = 30.0f;
	const float WANDER_DISTANCE = 10.0f;
	const float KNOWN_DISTANCE = 12.0f;

	//Chasing
	const float SIGHT_ANGLE = 0f;

	//Speed of the enemy
	const float WANDER_SPEED = 2.0f;
	const float SEARCH_SPEED = 6.0f;
	const float CHASE_SPEED = 10.0f;
	const float WANDER_ACCELERATION = 0.5f;
	const float SEARCH_ACCELERATION = 4.0f;
	const float CHASE_ACCELERATION = 8.0f;

	//Knockback
	Vector3 m_KnockBackSpeed;
	float m_Gravity = -10.0f;
	float m_KnockbackTimer;

	//Searching
	Vector3 m_SearchPos;
	const float SEARCH_TIMER = 10f;
	float m_SearchTimer = -1f;

	//The nav mesh agent
	NavMeshAgent m_Agent;

	//The players transform
	Transform m_PlayerTransform;

	//The position this enemy sticks around for wandering
	Vector3 m_LeashPosition;

	//The horde of this enemy (null means it will wander on it's own)
	HordeController m_Horde;


	//Current enemy state
	public enum EnemyState
	{
		Wander = 0,
		Search,
		Chase,
		Dead,
		Knockback
	};
	public EnemyState m_State = EnemyState.Wander;

	//Tells the agent where to go
	public void MoveTowards(Vector3 pos)
	{
		m_Agent.SetDestination (pos);
	}

	//Initialization
	public void OnSpawn (EnemyState state)
	{
		m_Agent = (NavMeshAgent)GetComponent<NavMeshAgent> ();
		m_PlayerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		SetState (state);
	}

	//If the enemy was placed in the scene directly
	void Start ()
	{
		m_LeashPosition = transform.position;
		OnSpawn (m_State);
	}

	//Each frame
	void Update ()
	{
		UpdateMovement ();
	}

	//Updates where this enemy will move towards
	void UpdateMovement ()
	{
		m_State = CheckState ();

		switch (m_State)
		{
		
		//Searching
		case EnemyState.Search:
		{
			//Set timer
			m_SearchTimer -= Time.deltaTime;
			if (m_SearchTimer < 0f || Vector3.Distance (transform.position, m_Agent.destination) <= m_Agent.stoppingDistance)
			{
				SetState(EnemyState.Wander);
			}
			break;
		}

		//Chasing
		case EnemyState.Chase:
		{
			MoveTowards(m_PlayerTransform.position);
			break;
		}	

		//Wandering
		case EnemyState.Wander:
		{	
			//Check if we need a new position to wander to
			if (Vector3.Distance (transform.position, m_Agent.destination) <= m_Agent.stoppingDistance ||
			    Vector3.Distance(transform.position, m_LeashPosition) > LEASH_DISTANCE)
			{
				GetNewWanderPosition ();
			}
			break;
		}	

		//Flying
		case EnemyState.Knockback:
		{
			transform.position += m_KnockBackSpeed * Time.deltaTime;

			m_KnockBackSpeed.y += m_Gravity * Time.deltaTime;

			m_KnockbackTimer -= Time.deltaTime;

			if(m_KnockbackTimer <= 0.0f)
			{
				SetState(EnemyState.Wander);
			}

			break;
		}

		//Error or dead
		default:
		{
			break;
		}
		}
	}

	/// <summary>
	/// Checks if this enemy should enter a state.
	/// </summary>
	EnemyState CheckState ()
	{
		//Precalculated values
		Vector3 playerPos = m_PlayerTransform.position;
		Vector3 dir = (transform.position - playerPos).normalized;
		float distance = Vector3.Distance(transform.position, playerPos);

		//If the enemy should enter the chase state
		if (distance < KNOWN_DISTANCE ||
		    (distance < CHASE_DISTANCE &&
		 Vector3.Dot (transform.forward, dir) > SIGHT_ANGLE &&
		    Physics.Raycast(transform.position, dir, CHASE_DISTANCE)))
		{
			//Set known player position
			if (m_Horde != null)
			{
				m_Horde.BroadcstPlayerPosition(playerPos);
			}
			else
			{
				SetSearchPosition (playerPos);
			}

			//Chase
			if (m_State != EnemyState.Chase)
			{
				SetState (EnemyState.Chase);
			}
		}
		//If the enemy should exit chase and enter search
		else if (m_State == EnemyState.Chase)
		{
			SetState (EnemyState.Search);
		}
		else if (m_State == EnemyState.Search)
		{
			if (Vector3.Distance(transform.position, m_SearchPos) <= m_Agent.stoppingDistance)
			{
				SetState (EnemyState.Wander);
			}
		}
		return m_State;
	}

	/// <summary>
	/// Sets the leash position.
	/// </summary>
	public void SetLeashPosition (Vector3 pos)
	{
		m_LeashPosition = pos;
	}

	/// <summary>
	/// Sets the enemies state.
	/// </summary>
	public void SetState (EnemyState state)
	{
		//Set the state
		m_State = state;

		//Wander ariund aimlessly
		if (state == EnemyState.Wander)
		{
			//Set speeds
			m_Agent.speed = WANDER_SPEED;
			m_Agent.acceleration = WANDER_ACCELERATION;

			//Set timer
			m_SearchTimer = -1f;

			//Set own leash position
			if (m_Horde == null)
			{
				SetLeashPosition (transform.position);
			}
			GetNewWanderPosition ();
		}

		//Searching for the player
		else if (state == EnemyState.Search)
		{
			//Set speeds
			m_Agent.speed = SEARCH_SPEED;
			m_Agent.acceleration = SEARCH_ACCELERATION;

			//Set timer
			m_SearchTimer = SEARCH_TIMER;

			//Set own search position
			if (m_Horde == null)
			{
				SetSearchPosition (m_PlayerTransform.position);
			}
			//Search movement for hordes
			else
			{
				MoveTowards(m_SearchPos + transform.position - m_LeashPosition);
			}
		}

		//Chasing the player
		else
		{
			//Set speeds
			m_Agent.speed = CHASE_SPEED;
			m_Agent.acceleration = CHASE_ACCELERATION;

			//Set timer
			m_SearchTimer = -1f;

			//Tell horde player found
			if (m_Horde != null)
			{
				m_Horde.OnPlayerFound(m_PlayerTransform.position);
			}
		}
	}

	//Gets a new wander to position
	void GetNewWanderPosition ()
	{
		//Move to there
		MoveTowards	(m_LeashPosition + WANDER_DISTANCE *
		             (new Vector3(Random.Range(0f, 1f), 0f, Random.Range(0f, 1f)) +
		 (m_LeashPosition - transform.position).normalized).normalized);
	}

	/// <summary>
	/// Sets the horde.
	/// </summary>
	public void SetHorde (HordeController horde)
	{
		m_Horde = horde;
	}

	/// <summary>
	/// Sets the search position
	/// </summary>
	public void SetSearchPosition (Vector3 pos)
	{
		if (m_SearchPos != pos)
		{
			m_SearchPos = pos;
			m_SearchTimer = SEARCH_TIMER;
			if (m_State == EnemyState.Search)
			{
				MoveTowards(m_SearchPos);
			}
		}
	}

	/// <summary>
	/// Knockback this enemy
	/// </summary>
	public void Knockback(Vector3 knockbackSpeed)
	{
		m_KnockBackSpeed = knockbackSpeed;

		SetState (EnemyState.Knockback);

		m_KnockbackTimer = Constants.KNOCKBACK_TIME;
	}
}
