using UnityEngine;
using System.Collections;

public class LoseSequenceManager : Singleton<LoseSequenceManager> 
{
	bool m_LoseSequenceActivated;

	public Renderer m_EndScreen;

	public float m_ScreenFadeTime;
	float m_ScreenFadeTimer;

	public float m_LoseSequenceTime;

	// Update is called once per frame
	void Update () 
	{
		if(m_LoseSequenceActivated)
		{
			ExclamationMark.Instance.DisplayMark(false);

			if(m_ScreenFadeTimer > 0.0f)
			{
				m_ScreenFadeTimer -= Time.deltaTime;

				float percentage = Mathf.Clamp01(m_ScreenFadeTimer / m_ScreenFadeTime);

				Color newColor = m_EndScreen.material.color;
				newColor.a = Mathf.Lerp (1.0f, 0.0f, percentage);
				m_EndScreen.material.color = newColor;

				if(m_ScreenFadeTimer <= 0.0f)
				{
					Application.LoadLevel ("SplashScene");
				}
			}
			else
			{
				m_LoseSequenceTime -= Time.deltaTime;

				if(m_LoseSequenceTime <= 0.0f)
				{
					m_ScreenFadeTimer = m_ScreenFadeTime;
				}
			}
		}
	}

	[ContextMenu("Start End Sequence")]
	public void StartLoseSequence()
	{
		m_LoseSequenceActivated = true;
	}
}
