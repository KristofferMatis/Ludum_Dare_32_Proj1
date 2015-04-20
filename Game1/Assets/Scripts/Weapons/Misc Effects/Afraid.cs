using UnityEngine;
using System.Collections;

public class Afraid : MiscEffects
{
	void Start()
	{
		m_Type = MiscEffectType.e_Afraid;
	}

	protected override void DoEffectVirtual (Health otherHealth)
	{
		if(otherHealth.GetComponent<EnemyController>())
		{
			otherHealth.GetComponent<EnemyController>().m_Fearful = true;
		}
	}
}
