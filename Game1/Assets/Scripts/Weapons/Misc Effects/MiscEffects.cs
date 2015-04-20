using UnityEngine;
using System.Collections;

public enum MiscEffectType
{
	e_Flaming,
	e_Scaling,
	e_Drunk,
	Count,
	
	//NOT implemented
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
