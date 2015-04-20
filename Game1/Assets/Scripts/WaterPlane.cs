using UnityEngine;
using System.Collections;

public class WaterPlane : MonoBehaviour 
{
	GameObject m_Water;

	public GameObject m_WaterPlane;

	// Use this for initialization
	void Start () 
	{
		m_Water = GameObject.FindGameObjectWithTag (Constants.WATER_TAG);
	}

	void Update()
	{
		if(transform.position.y <= m_Water.transform.position.y)
		{
			m_WaterPlane.SetActive(true);
		}
		else
		{
			m_WaterPlane.SetActive(false);
		}
	}
}
