﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	bool m_CanBeHitThisFrame = true;

    const string TEXTURES_FILE_PATH = "Textures/Hearts";
    Texture[] m_Textures;
	public int m_MaxHealth = 8;
	float m_CurrentHealth;
    Vector2 m_TextureStartPos = Vector2.zero;
    Vector2 m_TextureSpacing = new Vector2(15.0f, 0.0f);
	Rect m_TextureGUIRect;

	EnemyController m_Controller;
	PlayerMovement m_Movement;

	float m_OngoingEffectTimer;
	float m_OngoingEffectDamageRate;

	public float m_UnderwaterDamageRate;

	GameObject m_FlameParticles;

	public ParticleSystem m_UnderwaterParticles;

	GameObject m_Water;

	float m_WaterToMouthMaxDistance = 3.82f;
	float m_BaseStartLifetime;

	//AudioSource m_AudioSource;

	//public AudioClip[] m_FireClips;
	//public AudioClip[] m_DeathClips;
	//public AudioClip[] m_HurtClips;
    ////TODO:
    //public AudioClip[] m_EnemyVoiceClips;

	// Use this for initialization
	void Start () 
	{
		m_Controller = GetComponent<EnemyController> ();

		if(m_Controller == null)
		{
			m_Movement = GetComponent<PlayerMovement>();
		}

		m_TextureGUIRect = new Rect(0.0f, 0.0f, 0.05f * Screen.width, 0.05f * Screen.width);

		if(m_Movement != null)
			m_Textures = Resources.LoadAll<Texture>(TEXTURES_FILE_PATH);

		m_CurrentHealth = m_MaxHealth;

		m_BaseStartLifetime = m_UnderwaterParticles.startLifetime;

		m_Water = GameObject.FindGameObjectWithTag (Constants.WATER_TAG);

		//m_AudioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_CanBeHitThisFrame = true;

		if(m_OngoingEffectTimer > 0.0f)
		{
			m_CurrentHealth -= m_OngoingEffectDamageRate * Time.deltaTime;

			m_OngoingEffectTimer -= Time.deltaTime;

			if(m_OngoingEffectTimer <= 0.0f && m_FlameParticles != null)
			{
				Destroy (m_FlameParticles);
			}
		}

		if(m_UnderwaterParticles.transform.position.y < m_Water.transform.position.y)
		{
			m_CurrentHealth -= m_UnderwaterDamageRate * Time.deltaTime;

			m_UnderwaterParticles.startLifetime = m_BaseStartLifetime * 
				Mathf.Clamp(Mathf.Abs(m_UnderwaterParticles.transform.position.y - m_Water.transform.position.y) / m_WaterToMouthMaxDistance, 0.0f, 1.0f);

			if(!m_UnderwaterParticles.isPlaying)
			{
				m_UnderwaterParticles.Play();
			}
		}
		else
		{
			m_UnderwaterParticles.Stop();
		}
		
		if(m_CurrentHealth <= 0.0f)
		{
			Die ();
		}
	}

	public void Damage(int damage, Vector3 knockback)
	{
		if(m_CanBeHitThisFrame)
		{
			m_CanBeHitThisFrame = false;

            //m_AudioSource.PlayOneShot(m_FireClips[Random.Range(0, m_FireClips.Length)]);

			m_CurrentHealth -= damage;

			Knockback(knockback);
		}
	}

	void Die()
	{
		if(m_FlameParticles != null)
		{
			Destroy (m_FlameParticles);
			m_FlameParticles = null;
		}

		if(m_Controller != null)
		{
			m_Controller.SetState(EnemyController.EnemyState.Dead);
		}
		else if (m_Movement != null)
		{
			m_Movement.Die ();

			LoseSequenceManager.Instance.StartLoseSequence();
		}

		//if(m_DeathClips.Length > 0)
		//{
		//	m_AudioSource.PlayOneShot (m_DeathClips[Random.Range (0, m_DeathClips.Length)]);
		//}
	}

	void Knockback(Vector3 knockback)
	{
		if(m_Controller)
		{
			m_Controller.Knockback(knockback);
		}
		else if(m_Movement)
		{
			m_Movement.Knockback(knockback);
		}

		//if(m_HurtClips.Length > 0)
		//{
		//	m_AudioSource.PlayOneShot (m_HurtClips[Random.Range (0, m_HurtClips.Length)]);
		//}
	}
		
	public void SetOnFire(float time, float damageRate, GameObject flameParticles)
	{
		m_OngoingEffectTimer = time;
		m_OngoingEffectDamageRate = damageRate;

		if(m_FlameParticles == null)
		{
			GameObject flameObject = (GameObject)Instantiate (flameParticles, transform.position, transform.rotation);
			flameObject.transform.parent = transform;
			flameObject.transform.localScale = Vector3.one;

			m_FlameParticles = flameObject;

			//if(m_FireClips.Length > 0)
			//{
			//	m_AudioSource.PlayOneShot(m_FireClips[Random.Range (0, m_FireClips.Length)]);
			//}
		}
	}

    void OnGUI()
    {
		if(Event.current.type != EventType.Repaint)
		{
			return;
		}
        if (m_Movement == null)
            return;
        if (CraftingMenu.Instance.IsActive)
            return;

		int numberOfHearts = (int)Mathf.Lerp (0, m_Textures.Length, m_CurrentHealth / m_MaxHealth) + 1;

        GUI.color = Color.white;
        for(int i = 0; i < numberOfHearts && i < m_Textures.Length; i++)
        {
            m_TextureGUIRect.position = m_TextureStartPos + (m_TextureSpacing * (float)(i + 1)) + new Vector2 (m_TextureGUIRect.width * i, 0.0f);
            GUI.DrawTexture(m_TextureGUIRect, m_Textures[i]);
        }
    }
}
