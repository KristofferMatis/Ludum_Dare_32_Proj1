using UnityEngine;
using System.Collections;

public class MusicManager : Singleton<MusicManager> 
{
	AudioSource[] m_AudioSources;

	float m_AggroStateTime = 5.0f;
	float m_AggroStateTimer;

	public float m_FadeInSpeed = 0.5f;

	public float m_MaxVolume = 0.3f;

	// Use this for initialization
	void Start ()
	{
		m_AudioSources = GetComponents<AudioSource> ();

		m_AudioSources [0].volume = m_MaxVolume;
		m_AudioSources [1].volume = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float targetVolumeForNormal = 1;

		if(m_AggroStateTimer > 0.0f)
		{
			m_AggroStateTimer -= Time.deltaTime;

			targetVolumeForNormal = 0;
		}

		m_AudioSources [0].volume = Mathf.Lerp (m_AudioSources [0].volume, targetVolumeForNormal * m_MaxVolume, m_FadeInSpeed * Time.deltaTime);
		m_AudioSources [1].volume = Mathf.Lerp (m_AudioSources [1].volume, (1 - targetVolumeForNormal) * m_MaxVolume, m_FadeInSpeed * Time.deltaTime);
	}

	public void SetAggro()
	{
		m_AggroStateTimer = m_AggroStateTime;
	}
}
