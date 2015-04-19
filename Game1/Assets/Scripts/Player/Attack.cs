using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour 
{
	public Transform m_MenuPivotPoint;
	public Transform m_HandPivotPoint;

	public BaseBaseWeapon m_WeaponEquipped;

	float m_StartUpTimer;
	float m_RecoveryTimer;

	bool m_WeaponDrawn;

	public float m_LerpSpeed = 1.0f;
	public float m_RotationSpeed = 5.0f;

	void Start()
	{
		if(m_WeaponEquipped)
		{
			EquipWeapon (m_WeaponEquipped);
		}
	}
	
	// Update is called once per frame
	public void Update () 
	{		
		m_WeaponEquipped.transform.localPosition = Vector3.Lerp (m_WeaponEquipped.transform.localPosition, Vector3.zero, m_LerpSpeed * Time.deltaTime);
		m_WeaponEquipped.transform.localRotation = Quaternion.Lerp(m_WeaponEquipped.transform.localRotation, Quaternion.identity, m_LerpSpeed * Time.deltaTime);
		m_WeaponEquipped.transform.localScale = Vector3.Lerp (m_WeaponEquipped.transform.localScale, Vector3.one, m_LerpSpeed * Time.deltaTime);

		if(!m_WeaponDrawn && m_WeaponEquipped.transform.localPosition.magnitude < 0.1f)
		{
			m_WeaponEquipped.transform.RotateAround(m_WeaponEquipped.transform.position, Vector3.up, m_RotationSpeed * Time.deltaTime);
		}
	}

	public void EquipWeapon(BaseBaseWeapon weapon)
	{		
		if(weapon != null)
		{
			weapon.SetTotalStatsAfterCrafting ();
			m_WeaponEquipped = weapon;

			DrawWeapon (true);
		}
		else
		{
			if(m_WeaponEquipped != null)
			{
				Destroy(m_WeaponEquipped);

				m_WeaponEquipped = null;
			}
		}
	}

	public void DrawWeapon(bool isDrawn)
	{
		m_WeaponDrawn = isDrawn;

		if(m_WeaponDrawn)
		{			
			m_WeaponEquipped.transform.parent = m_HandPivotPoint;
		}
		else
		{			
			m_WeaponEquipped.transform.parent = m_MenuPivotPoint;
		}
	}
}
