using UnityEngine;
using System.Collections;

public class SplashCamera : MonoBehaviour 
{
	public float m_RotationSpeed = 10.0f;
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3 (0.0f, m_RotationSpeed * Time.deltaTime, 0.0f));
	}
}
