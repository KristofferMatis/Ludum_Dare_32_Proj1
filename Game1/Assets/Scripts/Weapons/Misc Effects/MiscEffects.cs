using UnityEngine;
using System.Collections;

public class MiscEffects : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void DoEffect(Health otherHealth)
	{
		DoEffectVirtual (otherHealth);
	}

	protected virtual void DoEffectVirtual(Health otherHealth)
	{
		// Override for specific effects ;-)
	}
}
