using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	bool m_CanBeHitThisFrame = true;

	public int m_MaxHealth;
	int m_CurrentHealth;

	EnemyController m_Controller;

	// Use this for initialization
	void Start () 
	{
		m_Controller = GetComponent<EnemyController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_CanBeHitThisFrame = true;
	}

	public void Damage(int damage, Vector3 knockback)
	{
		if(m_CanBeHitThisFrame)
		{
			m_CanBeHitThisFrame = false;

			m_CurrentHealth -= damage;

			if(m_CurrentHealth <= 0)
			{
				m_Controller.SetState(EnemyController.EnemyState.Dead);
			}

			m_Controller.Knockback(knockback);
		}
	}
}
