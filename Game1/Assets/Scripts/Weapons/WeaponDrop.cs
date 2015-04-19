using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDrop : MonoBehaviour 
{
    public Vector3 i_OriginalItemHidingSpot = new Vector3(-5000.0f, -5000.0f, -5000.0f);

    public GameObject i_MenuPrefab;
    GameObject m_MenuPrefab;
    public GameObject MenuPrefab
    {
        get 
        {
            if (m_MenuPrefab == null)
            {
                m_MenuPrefab = addEffects((GameObject)Instantiate(i_MenuPrefab, i_OriginalItemHidingSpot, Quaternion.identity)); 
            }
            return m_MenuPrefab;
        }
    }

    public GameObject i_GamePrefab;
    GameObject m_GamePrefab;
    public GameObject GamePrefab
    {
        get 
        {
            if (m_GamePrefab == null)
            {
                m_GamePrefab = addEffects((GameObject)Instantiate(i_GamePrefab, i_OriginalItemHidingSpot, Quaternion.identity));
            }
            return m_GamePrefab;
        }
    }


    [Range(0.001f, 0.9999f)]
    public float i_SpecialEffectsChance = 0.1f;
    List<MiscEffectType> m_Effects = new List<MiscEffectType>();


	// Use this for initialization
	void Start () 
    {
        float counter = 1.0f;
        for (float random = Random.Range(0.0f, 1.0f); random < Mathf.Pow(i_SpecialEffectsChance, counter); random = Random.Range(0.0f, 1.0f))
        {
            counter += 1.0f;
            addEffect();
        }
	}
	
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {//the player found us
            if(InputManager.Instance.PlayerInteract())
            {
                if(Inventory.Instance.setAttachment(this))
                {
                    //TODO: handle entering inventory properly
                    
					for (int i = 0; i < transform.childCount; i++)
                    {
						Destroy(transform.GetChild(i).gameObject);
                    }
                    transform.position = i_OriginalItemHidingSpot;
                }
                else
                {
                    //handle inventory full
                }
            }
        }
    }

    void addEffect()
    {
        switch((MiscEffectType)Random.Range(0, (int)MiscEffectType.Count))
        {
            case MiscEffectType.e_Flaming:
                m_Effects.Add(MiscEffectType.e_Flaming);
                break;
            case MiscEffectType.e_Scaling:
                m_Effects.Add(MiscEffectType.e_Scaling);
                break;
            default:
                break;
        };
    }

    GameObject addEffects(GameObject obj)
    {
        for (int i = 0; i < m_Effects.Count; i++)
        {
            switch (m_Effects[i])
            {

                case MiscEffectType.e_Flaming:
                    {
                        obj.AddComponent<Flaming>();
                        break;
                    }

                case MiscEffectType.e_Scaling:
                    {
                        obj.AddComponent<Scaling>();
                        break;
                    }

                case MiscEffectType.e_Drunk:
                    {
                        obj.AddComponent<Drunk>();
                        break;
                    }

                case MiscEffectType.e_Afraid:
                    {
                        obj.AddComponent<Afraid>();
                        break;
                    }
            }
        }
        return obj;
    }
}
