using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour 
{
	public Transform m_HolsterPivotPoint;
	public Transform m_HandPivotPoint;

	public BaseBaseWeapon m_WeaponEquipped;

	bool m_WeaponDrawn;

	void Start()
	{
		if(m_WeaponEquipped)
		{
			EquipWeapon (m_WeaponEquipped);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void EquipWeapon(BaseBaseWeapon weapon)
	{		
		weapon.SetTotalStatsAfterCrafting ();
		m_WeaponEquipped = weapon;

		DrawWeapon ();
	}

	void DrawWeapon()
	{
		m_WeaponDrawn = !m_WeaponDrawn;

		if(m_WeaponDrawn)
		{			
			m_WeaponEquipped.transform.parent = m_HandPivotPoint;
		}
		else
		{			
			m_WeaponEquipped.transform.parent = m_HolsterPivotPoint;
		}

		m_WeaponEquipped.transform.localPosition = Vector3.zero;
		m_WeaponEquipped.transform.localRotation = Quaternion.identity;
		m_WeaponEquipped.transform.localScale = Vector3.one;
		m_WeaponEquipped.ToggleCollider (m_WeaponDrawn);
	}
}
