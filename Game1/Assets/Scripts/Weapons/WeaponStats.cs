using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WeaponStats 
{
	public int m_Damage;
	public float m_Knockback;
	public int m_PlayerSpeed;
	public float m_StartUpTime;
	public float m_RecoveryTime;
	public List<Transform> m_MountPoints;
	
	public WeaponStats(WeaponStats stats)
	{
		m_Damage = stats.m_Damage;
		m_Knockback = stats.m_Knockback;
		m_PlayerSpeed = stats.m_PlayerSpeed;
		m_StartUpTime = stats.m_StartUpTime;
		m_RecoveryTime = stats.m_RecoveryTime;
		m_MountPoints = stats.m_MountPoints;
	}
}
