using UnityEngine;
using System.Collections;

public class FieryBarrel : MonoBehaviour 
{
	GameObject m_FlamesParticles;
	
	void Start()
	{
		m_FlamesParticles = Resources.Load<GameObject> ("Prefabs/Weapons/Effects/FireComplex");
	}
	
	void OnTriggerEnter(Collider otherCollider)
	{
		EnemyController enemy = otherCollider.gameObject.GetComponent<EnemyController> ();
		
		if(enemy && enemy.m_Fearful)
		{
			enemy.GetComponent<Health>().SetOnFire(30.0f, 0.0f, m_FlamesParticles);
		}
	}
}
