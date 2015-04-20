using UnityEngine;
using System.Collections;

public class ScreenMessenger : MonoBehaviour
{
	public const string INVENTORY_FULL_STRING = "Inventory Full";
	const float DRAW_TIMER = 4f;
	float m_DrawTimer = -1f;
	Rect m_DrawBox;
	string m_DrawString = "";


	// Use this for initialization
	void Start ()
	{
		m_DrawBox = new Rect (Screen.width / 2f - 100f, Screen.height / 2f - 200f, 200f, 30f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_DrawTimer > 0f)
		{
			m_DrawTimer -= Time.deltaTime;
		}
	}

	public void DrawMessage (string message)
	{
		m_DrawTimer = DRAW_TIMER;
		m_DrawString = message;
	}

	void OnGUI ()
	{
		if (m_DrawTimer > 0f)
		{
			GUI.Box(m_DrawBox, m_DrawString);
		}
	}
}
