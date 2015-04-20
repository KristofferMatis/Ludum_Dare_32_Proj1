using UnityEngine;
using System.Collections;

public class Flaming : MiscEffects 
{
	GameObject m_FlameParticles;

	float m_EffectTime;
	float m_EffectDamageRate;

	// Use this for initialization
	void Start () 
	{
		m_EffectTime = Constants.FIRE_EFFECT_TIME;
		m_EffectDamageRate = Constants.FIRE_EFFECT_DAMAGE_RATE;

		MeshFilter meshFilter = GetComponentInChildren<MeshFilter> ();

		//m_FlameParticles = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/Weapons/Effects/Flaming" + GetComponent<BaseAttachment>().m_AttachmentName), meshFilter.transform.position, meshFilter.transform.rotation);
		m_FlameParticles = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/Weapons/Effects/FireComplex"), meshFilter.transform.position, meshFilter.transform.rotation);

		m_FlameParticles.transform.parent = meshFilter.transform;

		m_FlameParticles.transform.localPosition = Vector3.zero;
		m_FlameParticles.transform.localRotation = Quaternion.identity;
		m_FlameParticles.transform.localScale = Vector3.one;

		m_FlameParticles.gameObject.layer = gameObject.layer;

        m_Type = MiscEffectType.e_Flaming;
	}

	protected override void DoEffectVirtual (Health otherHealth)
	{
		otherHealth.SetOnFire (m_EffectTime, m_EffectDamageRate, m_FlameParticles);
	}
}
