using UnityEngine;
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

		m_BoatManager = BoatManager.Instance;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!m_FirstUpdate)
		{
			m_FirstUpdate = true;	

			int i = 0;
			
			foreach(string type in m_BoatManager.ObjectsNecessary.Keys)
			{
				m_Words[i].material.SetTexture ("_MainTex", Resources.Load<Texture>("Textures/Words/" + type));
				m_Words[i].material.SetTexture ("_ScratchTex", Resources.Load<Texture>("Textures/Words/" + type + "-Scratch"));

				i++;
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
