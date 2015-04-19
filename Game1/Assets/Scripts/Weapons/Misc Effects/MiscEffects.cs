using UnityEngine;
using System.Collections;

public enum MiscEffectType
{
	e_Flaming,
	e_Scaling,
    Count,

    //NOT implemented
	e_Drunk,
	e_Afraid
}

public class MiscEffects : MonoBehaviour 
{
	public void DoEffect(Health otherHealth)
	{
		DoEffectVirtual (otherHealth);
	}

	protected virtual void DoEffectVirtual(Health otherHealth)
	{
		// Override for specific effects ;-)
	}
}
