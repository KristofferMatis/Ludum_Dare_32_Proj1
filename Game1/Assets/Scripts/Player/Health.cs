﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	bool m_CanBeHitThisFrame = true;

	public int m_MaxHealth;
	float m_CurrentHealth;

	EnemyController m_Controller;
	PlayerMovement m_Movement;

	float m_OngoingEffectTimer;
	float m_OngoingEffectDamageRate;

	public float m_UnderwaterDamageRate;

	ParticleSystem m_FlameParticles;

	public ParticleSystem m_UnderwaterParticles;

	GameObject m_Water;

	float m_WaterToMouthMaxDistance = 3.82f;
	float m_BaseStartLifetime;

	// Use this for initialization
	void Start () 
	{
		m_Controller = GetComponent<EnemyController> ();

		if(m_Controller == null)
		{
			m_Movement = GetComponent<PlayerMovement>();
		}

		m_CurrentHealth = m_MaxHealth;

		m_BaseStartLifetime = m_UnderwaterParticles.startLifetime;

		m_Water = GameObject.FindGameObjectWithTag (Constants.WATER_TAG);
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_CanBeHitThisFrame = true;

		if(m_OngoingEffectTimer > 0.0f)
		{
			m_CurrentHealth -= m_OngoingEffectDamageRate * Time.deltaTime;

			m_OngoingEffectTimer -= Time.deltaTime;

			if(m_OngoingEffectTimer <= 0.0f)
			{
				m_FlameParticles.loop = false;
			}
		}

		return;
		if(m_UnderwaterParticles.transform.position.y < m_Water.transform.position.y)
		{
			m_CurrentHealth -= m_UnderwaterDamageRate * Time.deltaTime;

			m_UnderwaterParticles.startLifetime = m_BaseStartLifetime * 
				Mathf.Clamp(Mathf.Abs(m_UnderwaterParticles.transform.position.y - m_Water.transform.position.y) / m_WaterToMouthMaxDistance, 0.0f, 1.0f);

			if(!m_UnderwaterParticles.isPlaying)
			{
				m_UnderwaterParticles.Play();
			}
		}
		else
		{
			m_UnderwaterParticles.Stop();
		}
		
		if(m_CurrentHealth <= 0.0f)
		{
			Die ();
		}
	}

	public void Damage(int damage, Vector3 knockback)
	{
		if(m_CanBeHitThisFrame)
		{
			m_CanBeHitThisFrame = false;

			m_CurrentHealth -= damage;

			Knockback(knockback);
		}
	}

	void Die()
	{
		if(m_Controller)
		{
			m_Controller.SetState(EnemyController.EnemyState.Dead);
		}
		else
		{
			// TODO: have player die

			//for now i'll just send us straight to the splash screen
			Application.LoadLevel ("SplashScene");
		}
	}

	void Knockback(Vector3 knockback)
	{
		if(m_Controller)
		{
			m_Controller.Knockback(knockback);
		}
		else
		{
			m_Movement.Knockback(knockback);
		}
	}
		
	public void SetOnFire(float time, float damageRate, GameObject flameParticles)
	{
		m_OngoingEffectTimer = time;
		m_OngoingEffectDamageRate = damageRate;

		if(m_FlameParticles == null)
		{
			GameObject flameObject = (GameObject)Instantiate (flameParticles, transform.position, transform.rotation);
			flameObject.transform.parent = transform;
			flameObject.transform.localScale = Vector3.one;

			m_FlameParticles = flameObject.GetComponent<ParticleSystem> ();
		}
		else
		{
			m_FlameParticles.loop = true;
			m_FlameParticles.Play ();
		}
	}
}
