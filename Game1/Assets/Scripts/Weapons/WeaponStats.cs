using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WeaponStats 
{
	public int m_Damage;
	public float m_Knockback;
	public string m_AttackType;
	public float m_Weight = 0.5f;
	public List<Transform> m_MountPoints;
	
	public WeaponStats(WeaponStats stats)
	{
		m_Damage = stats.m_Damage;
		m_Knockback = stats.m_Knockback;
		m_AttackType = stats.m_AttackType;
        m_Weight = stats.m_Weight;
		m_MountPoints = stats.m_MountPoints;
	}
}
