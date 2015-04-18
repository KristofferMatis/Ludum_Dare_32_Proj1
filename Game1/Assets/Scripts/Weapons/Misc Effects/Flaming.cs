using UnityEngine;
using System.Collections;

public class Flaming : MiscEffects 
{
	GameObject m_FlameParticles;

	public float m_EffectTime;
	public float m_EffectDamageRate;

	// Use this for initialization
	void Start () 
	{
		MeshFilter meshFilter = GetComponentInChildren<MeshFilter> ();

		m_FlameParticles = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/Weapons/Flaming"), meshFilter.transform.position, meshFilter.transform.rotation);

		m_FlameParticles.transform.parent = meshFilter.transform;

		m_FlameParticles.transform.localPosition = Vector3.zero;
		m_FlameParticles.transform.localRotation = Quaternion.identity;
		m_FlameParticles.transform.localScale = Vector3.one;

	}

	protected override void DoEffectVirtual (Health otherHealth)
	{
		otherHealth.SetOnFire (m_EffectTime, m_EffectDamageRate, m_FlameParticles);
	}
}
