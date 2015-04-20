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

	float m_SpawnTime = 120.0f;
	float m_SpawnTimer;

	float m_PlaneSpawnTimer;

	public List<AudioClip> m_PlaneCrashSounds;
	public AudioClip m_WeaponDropSound;

	public AudioSource m_PlaneAudioSource;

	Transform m_NextPlanePoint;

	DayNightCycle m_DayNightCycle;

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

		m_SpawnTimer -= Time.deltaTime;

		if(m_SpawnTimer <= 0.0f)
		{
			m_SpawnTimer = m_SpawnTime;

			SpawnNewItems();
		}

		if(m_PlaneSpawnTimer > 0.0f)
		{
			m_PlaneSpawnTimer -= Time.deltaTime;

			if(m_PlaneSpawnTimer <= 0.0f)
			{			
				m_PlaneSmoke.transform.parent = m_NextPlanePoint;
				m_PlaneSmoke.transform.localPosition = Vector3.zero;
				m_PlaneSmoke.Play ();
				
				m_PlaneSmoke.startSize = 1.0f;
				
				m_SmokeIsDisappearing = false;

				m_DayNightCycle.SpawnEnemies();
			}
		}
	}
	
	public void StopSmoke()
	{
		m_SmokeIsDisappearing = true;
	}

	void SpawnNewItems()
	{
		Transform dropoffPoint = m_DropOffSpawnPoints [Random.Range (0, m_DropOffSpawnPoints.Count)];

		Vector3 leashZone = Random.insideUnitSphere * m_LeashDistance;
		Instantiate (m_DropOffPrefab, dropoffPoint.position + leashZone, dropoffPoint.rotation);

		m_PlaneAudioSource.PlayOneShot (m_WeaponDropSound);
	}

	public void SpawnPlane(DayNightCycle dayNightCycle)
	{		
		m_DayNightCycle = dayNightCycle;

		m_NextPlanePoint = m_PlaneSmokeSpawnPoints [Random.Range (0, m_PlaneSmokeSpawnPoints.Count)];

		int randomIndex = Random.Range (0, m_PlaneCrashSounds.Count);

		m_NextPlanePoint.GetComponent<AudioSource>().PlayOneShot (m_PlaneCrashSounds[randomIndex]);

		m_PlaneSpawnTimer = m_PlaneCrashSounds[randomIndex].length;
	}
}
