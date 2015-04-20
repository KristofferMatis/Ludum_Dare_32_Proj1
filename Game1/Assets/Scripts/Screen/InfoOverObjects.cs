using UnityEngine;
using System.Collections;

public class InfoOverObjects : MonoBehaviour
{
	GameObject m_Player;
	public bool m_Draw = false;
	public string m_Text;
	public Vector2 m_BoxSize = new Vector3 (200f, 35f);
	public float m_OffsetOverObject = 300f;
	Rect m_DrawBox;


	// Use this for initialization
	void Start ()
	{
		m_Player = GameObject.FindGameObjectWithTag ("Player");
		m_DrawBox = new Rect (0f, 0f, m_BoxSize.x, m_BoxSize.y);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == m_Player)
		{
			m_Draw = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject == m_Player)
		{
			m_Draw = false;
		}
	}

	void OnGUI ()
	{
		if (m_Draw && Vector3.Dot ((transform.position - Camera.main.transform.position).normalized, Camera.main.transform.forward) > 0f)
		{
			Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
			m_DrawBox.x = pos.x - m_DrawBox.width/2f;
			m_DrawBox.y = m_OffsetOverObject + m_DrawBox.height/2f;
			GUI.Box(m_DrawBox, m_Text);
		}
	}
}
