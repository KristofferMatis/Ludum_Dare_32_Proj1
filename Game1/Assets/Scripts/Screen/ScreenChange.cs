using UnityEngine;
using System.Collections;

public class ScreenChange : MonoBehaviour
{
	public string m_NextLevelOnPress = "MainScene";
	public KeyCode m_KeyToGo = KeyCode.Space;
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(m_KeyToGo))
		{
			Application.LoadLevel (m_NextLevelOnPress);
		}
	}
}
