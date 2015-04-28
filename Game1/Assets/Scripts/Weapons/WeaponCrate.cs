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

	public float m_GroundCheckDistance = 1.0f;

	List<Rigidbody> m_Planks = new List<Rigidbody>();
	public Animation m_ParachuteAnimation;

	bool m_IsBroken;

	public List<GameObject> m_WeaponPrefabs;

	Dictionary<float, List<GameObject>> m_ItemRarities = new Dictionary<float, List<GameObject>>();

	List<float> m_Keys = new List<float>();

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

		PopulateRarityContainer ();
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

					SpawnWeapon (m_Barrel.position);

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
		GameObject prefabToInstantiate = GetWeaponPrefabForRarity (Random.value);

		if(prefabToInstantiate)
		{
        	Instantiate (prefabToInstantiate, position, Quaternion.identity);
		}
    }

	void PopulateRarityContainer()
	{
		foreach(GameObject prefab in m_WeaponPrefabs)
		{
			string type = prefab.GetComponent<WeaponDrop>().i_GamePrefab.GetComponent<BaseAttachment>().m_AttachmentName;

			float rarity = 0.0f;

			if(CraftingRecipesManager.Instance.ItemRarity(type) > 0)
			{
				rarity = 1.0f / CraftingRecipesManager.Instance.ItemRarity (type);
			}

			if(!m_ItemRarities.ContainsKey(rarity))
			{
				m_ItemRarities.Add (rarity, new List<GameObject>());

				m_Keys.Add (rarity);
			}
			
			m_ItemRarities[rarity].Add (prefab);
		}

		m_Keys.Sort ();
	}

	GameObject GetWeaponPrefabForRarity(float rarity)
	{
		GameObject result = null;

		foreach(float key in m_Keys)
		{
			if(result == null && rarity <= key)
			{
				result = m_ItemRarities[key][Random.Range (0, m_ItemRarities[key].Count)];
			}
		}

		return result;
	}
}
