using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponsSpawningManager : MonoBehaviour 
{
	public List<Transform> m_DropOffSpawnPoints;
	public List<Transform> m_PlaneSmokeSpawnPoints;

	GameObject m_DropOffPrefab;
	ParticleSystem m_PlaneSmoke;

	public float m_LeashDistance;

	bool m_SmokeIsDisappearing;
	float m_DisappearingSpeed = 0.1f;

	void Start()
	{
		m_DropOffPrefab = Resources.Load<GameObject> ("Prefabs/Weapons/DropOffCrate");

		GameObject planeSmokePrefab = Resources.Load<GameObject>("Prefabs/Events/PlaneSmoke");
		planeSmokePrefab = (GameObject)Instantiate (planeSmokePrefab);
		m_PlaneSmoke = planeSmokePrefab.GetComponent<ParticleSystem> ();
	}

	void Update()
	{
		if(m_SmokeIsDisappearing)
		{
			float newSize = m_PlaneSmoke.startSize;
			newSize = Mathf.Lerp (newSize, 0.0f, m_DisappearingSpeed * Time.deltaTime);
			m_PlaneSmoke.startSize = newSize;
		}
	}

	[ContextMenu("StopSmoke")]
	public void StopSmoke()
	{
		m_SmokeIsDisappearing = true;
	}

	[ContextMenu("New items")]
	public void SpawnNewItems()
	{
		foreach(Transform dropoffPoint in m_DropOffSpawnPoints)
		{
			Vector3 leashZone = Random.insideUnitSphere * m_LeashDistance;
			Instantiate (m_DropOffPrefab, dropoffPoint.position + leashZone, dropoffPoint.rotation);
		}

		Transform planePoint = m_PlaneSmokeSpawnPoints [Random.Range (0, m_PlaneSmokeSpawnPoints.Count)];
		m_PlaneSmoke.transform.parent = planePoint;
		m_PlaneSmoke.transform.localPosition = Vector3.zero;
		m_PlaneSmoke.Play ();

		m_PlaneSmoke.startSize = 1.0f;

		m_SmokeIsDisappearing = false;

		// TODO: shaky cam !!! + plane crash sound or something
	}
}
