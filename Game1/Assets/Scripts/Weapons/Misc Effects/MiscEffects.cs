using UnityEngine;
using System.Collections;

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
