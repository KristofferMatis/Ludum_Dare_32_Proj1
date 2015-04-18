using UnityEngine;
using System.Collections;

public class Scaling : MiscEffects 
{
	public float m_MinScale = 0.5f;
	public float m_MaxScale = 3.0f;

	public float m_RescalingSpeed = 0.5f;
	Vector3 m_Time;

	protected override void DoEffectVirtual (Health otherHealth)
	{
		otherHealth.transform.localScale = transform.localScale;	
	}

	void Start()
	{
		m_Time.x = Random.Range (0.0f, Mathf.PI);
		m_Time.y = Random.Range (0.0f, Mathf.PI);
		m_Time.z = Random.Range (0.0f, Mathf.PI);
	}

	void Update()
	{
		Vector3 newScale = Vector3.zero;
		newScale.x = m_MinScale + (m_MaxScale - m_MinScale) / 2.0f * (1.0f + Mathf.Cos (m_Time.x));
		newScale.y = m_MinScale + (m_MaxScale - m_MinScale) / 2.0f * (1.0f + Mathf.Sin (m_Time.y));
		newScale.z = m_MinScale + (m_MaxScale - m_MinScale) / 2.0f * (1.0f + Mathf.Cos (m_Time.z));
		transform.localScale = newScale;

		m_Time.x += Time.deltaTime * m_RescalingSpeed;
		m_Time.y += Time.deltaTime * m_RescalingSpeed;
		m_Time.z += Time.deltaTime * m_RescalingSpeed;
	}
}
