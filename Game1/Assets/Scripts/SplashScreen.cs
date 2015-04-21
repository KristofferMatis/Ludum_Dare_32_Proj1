using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour 
{	
	// Update is called once per frame
	void Update () 
	{
		if(Input.anyKeyDown)
		{
			Application.LoadLevel("MainScene");
		}
	}
}
