using UnityEngine;
using System.Collections;

public class Drunk : MiscEffects 
{
    void Start()
    {
        m_Type = MiscEffectType.e_Drunk;
    }

	protected override void DoEffectVirtual (Health otherHealth)
	{
		otherHealth.GetComponent<Attack> ().SetDrunkEffect (Constants.DRUNK_EFFECT_DURATION);
	}
}
