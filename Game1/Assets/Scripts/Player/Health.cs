using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	bool m_CanBeHitThisFrame = true;

	public int m_MaxHealth;
	float m_CurrentHealth;

	EnemyController m_Controller;

	float m_OngoingEffectTimer;
	float m_OngoingEffectDamageRate;

	ParticleSystem m_FlameParticles;

	// Use this for initialization
	void Start () 
	{
		m_Controller = GetComponent<EnemyController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_CanBeHitThisFrame = true;

		if(m_OngoingEffectTimer > 0.0f)
		{
			m_CurrentHealth -= m_OngoingEffectDamageRate * Time.deltaTime;

			if(m_CurrentHealth <= 0)
			{
				m_Controller.SetState(EnemyController.EnemyState.Dead);
			}

			if(m_OngoingEffectTimer <= 0.0f)
			{
				m_FlameParticles.loop = false;
			}
		}
	}

	public void Damage(int damage, Vector3 knockback)
	{
		if(m_CanBeHitThisFrame)
		{
			m_CanBeHitThisFrame = false;

			m_CurrentHealth -= damage;

			if(m_CurrentHealth <= 0.0f)
			{
				m_Controller.SetState(EnemyController.EnemyState.Dead);
			}
		}
	}

	public void SetOnFire(float time, float damageRate, GameObject flameParticles)
	{
		m_OngoingEffectTimer = time;
		m_OngoingEffectDamageRate = damageRate;

		GameObject flameObject = (GameObject)Instantiate (flameParticles, transform.position, transform.rotation);
		flameObject.transform.parent = transform;
		flameObject.transform.localScale = Vector3.one;

		m_FlameParticles = flameObject.GetComponent<ParticleSystem> ();
	}
}
