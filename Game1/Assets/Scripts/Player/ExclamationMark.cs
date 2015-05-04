using UnityEngine;
using System.Collections;

public class ExclamationMark : Singleton<ExclamationMark> 
{
	GameObject m_CameraObject;

	Renderer m_PlaneRenderer;

	void Start()
	{
		m_CameraObject = Camera.main.gameObject;

		m_PlaneRenderer = GetComponentInChildren<Renderer>();

		DisplayMark (false);
	}

	void Update()
	{
		transform.forward = m_CameraObject.transform.position - transform.position;
	}

	public void DisplayMark(bool isDisplayed)
	{
		m_PlaneRenderer.enabled = isDisplayed;
	}
}
