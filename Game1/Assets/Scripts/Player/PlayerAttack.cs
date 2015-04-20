using UnityEngine;
using System.Collections;

public class PlayerAttack : Attack 
{
	

	// Update is called once per frame
	void Update () 
	{
		if(m_WeaponEquipped != null)
		{
			if (m_WeaponDrawn)
			{
				if(InputManager.Instance.PlayerAttack1())
				{
					DoAttack();
				}

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
			m_WeaponEquipped.transform.localScale = Vector3.Lerp (m_WeaponEquipped.transform.localScale, Vector3.one, m_LerpSpeed * Time.deltaTime);
			
			if(!m_WeaponDrawn && m_WeaponEquipped.transform.localPosition.magnitude < 0.1f)
			{
				m_WeaponEquipped.transform.RotateAround(m_WeaponEquipped.transform.position, Vector3.up, m_RotationSpeed * Time.deltaTime);
			}
			else
			{
				m_WeaponEquipped.transform.localRotation = Quaternion.Lerp(m_WeaponEquipped.transform.localRotation, Quaternion.identity, m_LerpSpeed * Time.deltaTime);
			}
		}
		
		m_DrunkTimer -= Time.deltaTime;
	}
}
