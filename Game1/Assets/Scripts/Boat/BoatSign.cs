﻿using UnityEngine;
using System.Collections;

public class BoatSign : MonoBehaviour 
{
	public float m_ScratchingSpeed;

	Renderer[] m_Words;

	BoatManager m_BoatManager;

	bool m_FirstUpdate;

	// Use this for initialization
	void Start () 
	{
		m_Words = GetComponentsInChildren<Renderer> ();

		m_BoatManager = GameObject.FindGameObjectWithTag (Constants.BOAT_TAG).GetComponent<BoatManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!m_FirstUpdate)
		{
			m_FirstUpdate = true;			
			
			for(int i = 0; i < m_BoatManager.ObjectsStillNecessary.Count; i++)
			{
				m_Words[i].material.SetTexture ("_MainTex", Resources.Load<Texture>("Textures/Words/" + m_BoatManager.ObjectsStillNecessary[i]));
				m_Words[i].material.SetTexture ("_ScratchTex", Resources.Load<Texture>("Textures/Words/" + m_BoatManager.ObjectsStillNecessary[i] + "-Scratch"));
			}
		}

		int index = 0;

		foreach(string key in m_BoatManager.ObjectsNecessary.Keys)
		{
			float scratchAmount = m_Words[index].material.GetFloat ("_ScratchAmount");
			float target = m_BoatManager.ObjectsNecessary[key] ? 0.0f : 1.0f;

			scratchAmount = Mathf.Lerp(scratchAmount, target, m_ScratchingSpeed * Time.deltaTime);

			m_Words[index].material.SetFloat ("_ScratchAmount", scratchAmount);

			index++;
		}
	}
}
