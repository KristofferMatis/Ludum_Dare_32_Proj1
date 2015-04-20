using UnityEngine;
using System.Collections;

public class Scaling : MiscEffects 
{
	public float m_MinScale = 0.5f;
	public float m_MaxScale = 3.0f;

	void Start()
	{
		m_MinScale = Constants.SCALING_EFFECT_MIN_SCALE;
		m_MaxScale = Constants.SCALING_EFFECT_MAX_SCALE;

        m_Type = MiscEffectType.e_Scaling;
	}

	protected override void DoEffectVirtual (Health otherHealth)
	{
		Vector3 newScale = Random.insideUnitSphere * Random.Range (m_MinScale, m_MaxScale);
		newScale.x = Mathf.Abs (newScale.x);
		newScale.y = Mathf.Abs (newScale.y);
		newScale.z = Mathf.Abs (newScale.z);
		otherHealth.transform.localScale = newScale;	
	}
}
