using UnityEngine;
using System.Collections;

public class WavesSoundManager : MonoBehaviour 
{
	GameObject m_Water;

	public float m_MaxOffset = 1.0f;

	AudioSource m_AudioSource;

	// Use this for initialization
	void Start () 
	{
		m_Water = GameObject.FindGameObjectWithTag (Constants.WATER_TAG);

		m_AudioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float volume = 0.0f;

		if(transform.position.y > m_Water.transform.position.y && transform.position.y < m_Water.transform.position.y + m_MaxOffset)
		{
			volume = 1.0f - ((transform.position.y - m_Water.transform.position.y) / m_MaxOffset);
		}

		m_AudioSource.volume = Mathf.Lerp (m_AudioSource.volume, volume, Time.deltaTime);
	}
}
