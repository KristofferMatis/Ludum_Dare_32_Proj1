using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	//Distances
	const float CHASE_DISTANCE = 35.0f;
	const float LEASH_DISTANCE = 30.0f;

	//Speed of the enemy
	const float WANDER_SPEED = 2.0f;
	const float SEARCH_SPEED = 3.5f;
	const float CHASE_SPEED = 5.0f;

	//The nav mesh agent
	NavMeshAgent m_Agent;

	//The players transform
	Transform m_PlayerTransform;

	//The position this enemy sticks around for wandering
	Vector3 m_LeashPosition;

	//The horde of this enemy (null means it will wander on it's own)
	public HordeController m_Horde;

	//Current enemy state
	public enum EnemyState
	{
		Wander = 0,
		Search,
		Chase,
		Dead
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

	//If the enemy was placed inot the scene
	void Start ()
	{
		m_LeashPosition = transform.position;
		if (m_Horde != null)
		{
			m_Horde.AddEnemy (this);
		}
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
		Vector3 playerPos = m_PlayerTransform.position;
		m_State = CheckChaseDistance (playerPos);
		switch (m_State)
		{
			
		case EnemyState.Search:
		{
			if (m_Horde != null)
			{
				MoveTowards(playerPos + transform.position - m_LeashPosition);
			}
			else
			{
				MoveTowards(playerPos);
			}
			break;
		}
		case EnemyState.Chase:
		{
			MoveTowards(playerPos);
			break;
		}	
		case EnemyState.Wander:
		{	
			if (Vector3.Distance(transform.position, m_LeashPosition) > LEASH_DISTANCE)
			{
				GetNewWanderPosition ();
			}
			break;
		}	
		default:
		{
			break;
		}
		}
	}

	/// <summary>
	/// Checks if this enemy should enter chasing state.
	/// </summary>
	/// <returns>The chase distance.</returns>
	public EnemyState CheckChaseDistance (Vector3 pos)
	{
		if (m_State != EnemyState.Chase && Vector3.Distance(transform.position, pos) < CHASE_DISTANCE)
		{
			SetState (EnemyState.Chase);
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
		if (state == EnemyState.Wander)
		{
			m_Agent.speed = WANDER_SPEED;
			if (m_Horde == null)
			{
				SetLeashPosition (transform.position);
			}
			GetNewWanderPosition ();
		}
		else if (state == EnemyState.Search)
		{
			m_Agent.speed = SEARCH_SPEED;
		}
		else
		{
			m_Agent.speed = CHASE_SPEED;
		}
		m_State = state;
	}

	void GetNewWanderPosition ()
	{
		MoveTowards	(m_LeashPosition + (LEASH_DISTANCE + 1f) *
		            (new Vector3(Random.Range(0f, 1f), 0.0f, Random.Range(0f, 1f)) +
		 			(m_LeashPosition - transform.position).normalized).normalized);
	}
}
