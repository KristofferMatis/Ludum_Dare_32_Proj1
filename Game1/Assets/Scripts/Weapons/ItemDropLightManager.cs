using UnityEngine;
using System.Collections;

public class ItemDropLightManager : MonoBehaviour 
{
	public float m_MaxSize = 1.0f;
	public float m_MinSize = 0.1f;

	public float m_MaxSpeed = 10.0f;
	public float m_MinSpeed = 1.0f;

	public float m_MaxRange = 25.0f;
	public float m_MinRange = 1.0f;

	PlayerMovement m_Player;
	ParticleSystem m_Particles;

	// Use this for initialization
	void Start () 
	{
		m_Player = FindObjectOfType<PlayerMovement> ();

		m_Particles = GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float playerDistance = Vector3.Distance (transform.position, m_Player.transform.position);
		playerDistance = Mathf.Clamp (playerDistance, m_MinRange, m_MaxRange);

		float percentage = (playerDistance - m_MinRange) / (m_MaxRange - m_MinRange);

		m_Particles.startSpeed = Mathf.Lerp (m_MinSpeed, m_MaxSpeed, percentage);
		m_Particles.startSize = Mathf.Lerp (m_MinSize, m_MaxSize, percentage);
	}
}
