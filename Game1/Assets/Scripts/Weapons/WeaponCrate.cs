using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponCrate : MonoBehaviour 
{
	public Transform m_Barrel;

	public float m_FallSpeed;
	public float m_ExplosionForce;
	public float m_ExplosionRadius;

	public float m_ChanceOfSpawningWeapon;
	public float m_ChanceOfSpawningMiscEffect;

	public float m_GroundCheckDistance = 1.0f;

	List<Rigidbody> m_Planks = new List<Rigidbody>();
	public Animation m_ParachuteAnimation;

	bool m_IsBroken;

	public List<GameObject> m_WeaponPrefabs;
	public List<MiscEffectType> m_MiscEffects;

	void Start()
	{
		//m_ParachuteAnimation.Play(Constants.PARACHUTE_FLIGHT_ANIMATION);

		foreach(Transform child in m_Barrel)
		{
			if(child != m_Barrel)
			{
				m_Planks.Add(child.GetComponent<Rigidbody>());
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position -= Vector3.up * m_FallSpeed * Time.deltaTime;

		if(!m_IsBroken)
		{
			RaycastHit hitInfo;

			if(Physics.Raycast(m_Barrel.position, Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				if(hitInfo.collider.tag == Constants.GROUND_TAG)
				{
					BreakBarrel(hitInfo.point);

					if(Random.value <= m_ChanceOfSpawningWeapon)
					{
						SpawnWeapon (m_Barrel.position);
					}

					m_IsBroken = true;
				}
			}
		}
	}

	void BreakBarrel(Vector3 position)
	{
		//m_ParachuteAnimation.Play(Constants.PARACHUTE_LANDING_ANIMATION);

		foreach(Rigidbody rigidbody in m_Planks)
		{
            //TODO:Spawn a new Weapon Piece

			rigidbody.isKinematic = false;

			rigidbody.AddExplosionForce(m_ExplosionForce, m_Barrel.position, m_ExplosionRadius);

			rigidbody.transform.parent = null;
		}
	}

	void SpawnWeapon(Vector3 position)
	{
        
        Instantiate (m_WeaponPrefabs [Random.Range (0, m_WeaponPrefabs.Count)], position, Quaternion.identity);
        /*
        float miscEffectTest = 0.0f;
        int index = 0;

        do 
        {
            miscEffectTest = Random.value;

            if (index < m_MiscEffects.Count && miscEffectTest <= m_ChanceOfSpawningMiscEffect)
            {
                switch(m_MiscEffects[index])
                {
                    case MiscEffectType.e_Flaming:
                    {
                        newWeapon.AddComponent<Flaming> ();
                        break;
                    }

                    case MiscEffectType.e_Scaling:
                    {
                        newWeapon.AddComponent<Scaling> ();
                        break;
                    }

                    case MiscEffectType.e_Drunk:
                    {
                        newWeapon.AddComponent<Drunk> ();
                        break;
                    }

                    case MiscEffectType.e_Afraid:
                    {
                        newWeapon.AddComponent<Afraid> ();
                        break;
                    }
                }
                index++;
            }
        }
        while(miscEffectTest <= m_ChanceOfSpawningMiscEffect && index < m_MiscEffects.Count);
        */
    }
}
