using UnityEngine;
using System.Collections;

public class Drunk : MiscEffects 
{
	protected override void DoEffectVirtual (Health otherHealth)
	{
		otherHealth.GetComponent<Attack> ().SetDrunkEffect (Constants.DRUNK_EFFECT_DURATION);
	}
}
