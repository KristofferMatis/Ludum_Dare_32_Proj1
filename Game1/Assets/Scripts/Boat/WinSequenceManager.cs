using UnityEngine;
using System.Collections;

public class WinSequenceManager : Singleton<WinSequenceManager> 
{
	public Transform m_WaterTransform;

	public float m_BoatSpeed;

	public GameObject m_Boat;
	public GameObject m_Player;

	public Vector3 m_PlayerOffset;

	bool m_WinSequenceActivated;

	public Renderer m_EndScreen;

	public float m_ScreenFadeTime;

	float m_ScreenFadeTimer;

	bool m_IsFadingIn;

	public float m_IntermissionTime;
	float m_IntermissionTimer;

	public float m_WinSequenceTime;

	// Update is called once per frame
	void Update () 
	{
		Vector3 newPosition = transform.position;
		newPosition.y = m_WaterTransform.position.y;
		transform.position = newPosition;

		if(m_WinSequenceActivated)
		{
			ExclamationMark.Instance.DisplayMark(false);

			if(m_ScreenFadeTimer > 0.0f)
			{
				m_ScreenFadeTimer -= Time.deltaTime;

				float percentage = Mathf.Clamp01(m_ScreenFadeTimer / m_ScreenFadeTime);

				Color newColor = m_EndScreen.material.color;

				if(m_IsFadingIn)
				{
					newColor.a = Mathf.Lerp (0.0f, 1.0f, percentage);

					transform.position += transform.right * m_BoatSpeed * Time.deltaTime;
				}
				else
				{
					newColor.a = Mathf.Lerp (1.0f, 0.0f, percentage);
				}

				m_EndScreen.material.color = newColor;

				if(m_ScreenFadeTimer <= 0.0f)
				{
					if(!m_IsFadingIn)
					{
						m_IntermissionTimer = m_IntermissionTime;

						SetUpWinSequence();
					}
				}
			}
			else if(m_IntermissionTimer > 0.0f)
			{
				m_IntermissionTimer -= Time.deltaTime;

				if(m_IntermissionTimer <= 0.0f)
				{
					m_IsFadingIn = true;

					m_ScreenFadeTimer = m_ScreenFadeTime;
				}
			}
			else
			{
				transform.position += transform.right * m_BoatSpeed * Time.deltaTime;

				m_WinSequenceTime -= Time.deltaTime;

				if(m_WinSequenceTime <= 0.0f)
				{
					Application.LoadLevel ("SplashScene");
				}
			}
		}
	}

	[ContextMenu("Start End Sequence")]
	public void StartWinSequence()
	{
		m_ScreenFadeTimer = m_ScreenFadeTime;

		m_WinSequenceActivated = true;
	}

	void SetUpWinSequence()
	{
		m_Boat.transform.parent = transform;
		m_Player.transform.parent = m_Boat.transform;

		m_Player.transform.localPosition = m_PlayerOffset;
		m_Boat.transform.localPosition = Vector3.zero;

		m_Boat.transform.right = transform.right;
		m_Player.transform.forward = transform.right;

		m_Player.GetComponent<PlayerMovement>().enabled = false;
		m_Player.GetComponent<PlayerAttack>().enabled = false;
		m_Player.GetComponent<Health>().enabled = false;
	}
}
