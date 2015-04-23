using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
	public Transform m_MenuPivotPoint;
	public Transform m_HandPivotPoint;

	public BaseBaseWeapon m_WeaponEquipped;

	public WeaponDrop m_InitialWeapon;

	protected float m_StartUpTimer;
	protected float m_RecoveryTimer;

	protected bool m_WeaponDrawn;
	protected bool m_IsAttacking;

	public float m_LerpSpeed = 15.0f;
	public float m_RotationSpeed = 25.0f;

	protected float m_DrunkTimer;

	protected PlayerAnimator m_Animator;

	AudioSource m_AudioSource;

	public AudioClip[] m_AttackClips;

	float m_AttackTime = 0.5f;
	protected float m_Timer;

	void Start()
	{
		if(m_WeaponEquipped)
		{
			EquipWeapon (m_WeaponEquipped);
            DrawWeapon(true);
		}
        m_Animator = gameObject.GetComponentInChildren<PlayerAnimator>();

		m_AudioSource = GetComponent<AudioSource> ();
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

					m_Timer -= Time.deltaTime;

					if(m_Timer <= 0.0f)
					{
						m_IsAttacking = false;
					}
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
		if(m_WeaponEquipped != null)
		{
			//m_StartUpTimer = m_WeaponEquipped.TotalStats.m_StartUpTime;

			m_Timer = m_AttackTime;

            if (m_Animator != null)
            {
                switch (m_WeaponEquipped.m_Stats.m_AttackType)
                {
                    case "Bash":
                    	m_Animator.PlayAnimation(PlayerAnimator.Animations.Bash);
                        break;
                    case "Thrust":
						m_Animator.PlayAnimation(PlayerAnimator.Animations.Stab);
                        break;
                    case "Slash":
						m_Animator.PlayAnimation(PlayerAnimator.Animations.Slash);
                        break;
                    default:
                        break;
                }
            }

			if(m_AttackClips.Length > 0)
			{
				m_AudioSource.PlayOneShot (m_AttackClips[Random.Range (0, m_AttackClips.Length)]);
			}

			m_IsAttacking = true;
		}
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