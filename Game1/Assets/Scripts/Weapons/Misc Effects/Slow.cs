using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slow : MiscEffects 
{
	float m_EffectTime;
	float m_EffectSpeedRate;

	Dictionary<EnemyController, float> m_EnemyTimers = new Dictionary<EnemyController, float>();
	List<EnemyController> m_Enemies = new List<EnemyController>();

	PlayerMovement m_Player;
	float m_PlayerTimer;

	// Use this for initialization
	void Start () 
	{
		m_EffectTime = Constants.SLOW_EFFECT_TIME;
		m_EffectSpeedRate = Constants.SLOW_EFFECT_SPEED_RATE;

        m_Type = MiscEffectType.e_Slow;
	}

	void Update()
	{
		if(m_Player != null)
		{
			m_PlayerTimer -= Time.deltaTime;

			if(m_PlayerTimer <= 0.0f)
			{
				m_Player.MoveSpeed /= m_EffectSpeedRate;
				m_Player.RunSpeed /= m_EffectSpeedRate;

				m_Player = null;
			}
		}

		int index = 0;

		while (index < m_Enemies.Count)
		{
			m_EnemyTimers[m_Enemies[index]] -= Time.deltaTime;

			if(m_EnemyTimers[m_Enemies[index]] <= 0.0f)
			{
				m_Enemies[index].WANDER_SPEED /= m_EffectSpeedRate;
				m_Enemies[index].SEARCH_SPEED /= m_EffectSpeedRate;
				m_Enemies[index].CHASE_SPEED /= m_EffectSpeedRate;
				m_Enemies[index].ATTACK_SPEED /= m_EffectSpeedRate;

				m_EnemyTimers.Remove(m_Enemies[index]);
				m_Enemies.RemoveAt (index);
			}
			else
			{
				index++;
			}
		}
	}

	protected override void DoEffectVirtual (Health otherHealth)
	{
		if(otherHealth.GetComponent<PlayerMovement>())
		{
			if(m_Player == null)
			{
				m_Player = otherHealth.GetComponent<PlayerMovement>();
				m_Player.MoveSpeed *= m_EffectSpeedRate;
				m_Player.RunSpeed *= m_EffectSpeedRate;
			}

			m_PlayerTimer = m_EffectTime;
		}
		else if(otherHealth.GetComponent<EnemyController>())
		{
			EnemyController enemy = otherHealth.GetComponent<EnemyController>();

			if(!m_EnemyTimers.ContainsKey(enemy))
			{
				m_Enemies.Add (enemy);
				m_EnemyTimers.Add (enemy, m_EffectTime);
				enemy.WANDER_SPEED *= m_EffectSpeedRate;
				enemy.SEARCH_SPEED *= m_EffectSpeedRate;
				enemy.CHASE_SPEED *= m_EffectSpeedRate;
				enemy.ATTACK_SPEED *= m_EffectSpeedRate;
			}

			m_EnemyTimers[enemy] = m_EffectTime;
		}
	}
}
