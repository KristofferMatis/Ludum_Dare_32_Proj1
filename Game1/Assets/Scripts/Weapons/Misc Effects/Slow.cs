using UnityEngine;
using System.Collections;

public class Slow : MiscEffects 
{
	float m_EffectTime;
	float m_EffectDamageRate;

	// Use this for initialization
	void Start () 
	{
        m_Type = MiscEffectType.e_Slow;
	}

	protected override void DoEffectVirtual (Health otherHealth)
	{

	}
}
