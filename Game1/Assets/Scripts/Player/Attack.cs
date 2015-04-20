using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour 
{
	public Transform m_MenuPivotPoint;
	public Transform m_HandPivotPoint;

	public BaseBaseWeapon m_WeaponEquipped;

	public WeaponDrop m_InitialWeapon;

	float m_StartUpTimer;
	float m_RecoveryTimer;

	bool m_WeaponDrawn;
	bool m_IsAttacking;

	public float m_LerpSpeed = 15.0f;
	public float m_RotationSpeed = 5.0f;

	float m_DrunkTimer;

	void Start()
	{
		if(m_WeaponEquipped)
		{
			EquipWeapon (m_WeaponEquipped);
            DrawWeapon(true);
		}
	}
	
	// Update is called once per frame
	public void Update () 
	{		
		if(m_WeaponEquipped != null)
		{
			if (m_WeaponDrawn)
			{
				m_WeaponEquipped.transform.parent = m_HandPivotPoint;

				if(m_IsAttacking)
				{
					m_WeaponEquipped.ToggleCollider(true);
				}
				else
				{
					m_WeaponEquipped.ToggleCollider(false);
				}

				if(m_DrunkTimer > 0.0f)
				{
					m_WeaponEquipped.SetDrunk (true);
				}
				else
				{					
					m_WeaponEquipped.SetDrunk (false);
				}
			}
			else
			{
				m_WeaponEquipped.transform.parent = m_MenuPivotPoint;
			}

			m_WeaponEquipped.transform.localPosition = Vector3.Lerp (m_WeaponEquipped.transform.localPosition, Vector3.zero, m_LerpSpeed * Time.deltaTime);
			m_WeaponEquipped.transform.localRotation = Quaternion.Lerp(m_WeaponEquipped.transform.localRotation, Quaternion.identity, m_LerpSpeed * Time.deltaTime);
			m_WeaponEquipped.transform.localScale = Vector3.Lerp (m_WeaponEquipped.transform.localScale, Vector3.one, m_LerpSpeed * Time.deltaTime);

			if(!m_WeaponDrawn && m_WeaponEquipped.transform.localPosition.magnitude < 0.1f)
			{
				m_WeaponEquipped.transform.RotateAround(m_WeaponEquipped.transform.position, Vector3.up, m_RotationSpeed * Time.deltaTime);
			}
		}
		
		m_DrunkTimer -= Time.deltaTime;
	}

	public void EquipWeapon(BaseBaseWeapon weapon)
	{		
		if(weapon != null)
		{
			weapon.SetTotalStatsAfterCrafting ();
			m_WeaponEquipped = weapon;
			m_WeaponEquipped.gameObject.tag = tag;
		}
		else
		{
			if(m_WeaponEquipped != null)
			{
				Destroy(m_WeaponEquipped.gameObject);

				m_WeaponEquipped = null;
			}
		}
	}

	public void DrawWeapon(bool isDrawn)
	{
		m_WeaponDrawn = isDrawn;
	}

	public void DoAttack()
	{
		m_StartUpTimer = m_WeaponEquipped.TotalStats.m_StartUpTime;
	}

	public void SetDrunkEffect(float drunkTime)
	{
		m_DrunkTimer = drunkTime;
	}

	public bool IsAttacking()
	{
		return m_IsAttacking;
	}
}