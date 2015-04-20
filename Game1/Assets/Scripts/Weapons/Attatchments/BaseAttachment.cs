using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseAttachment : MonoBehaviour 
{
	public string m_AttachmentName;

	[SerializeField]
	public WeaponStats m_Stats;

	WeaponStats m_TotalStats;

	BoxCollider m_Collider;

	BaseAttachment m_ParentAttachment;

	List<MiscEffects> m_MiscEffects = new List<MiscEffects>();

	bool m_IsDrunk;

	public WeaponStats TotalStats
	{
		get
		{
			return m_TotalStats;
		}
	}

	protected virtual void Start()
	{
		m_Collider = GetComponent<BoxCollider> ();

		SetTotalStatsAfterCrafting ();

		ToggleCollider (false);
	}

	WeaponStats GetStats()
	{
		WeaponStats stats = new WeaponStats (m_Stats);

		foreach(Transform attachment in m_Stats.m_MountPoints)
		{
			if(attachment.childCount > 0)
			{
				WeaponStats attachmentStats = attachment.GetChild (0).GetComponent<BaseAttachment>().GetStats ();

				stats.m_Damage += attachmentStats.m_Damage;
				stats.m_Knockback += attachmentStats.m_Knockback;
				stats.m_AttackType += attachmentStats.m_AttackType;
				stats.m_StartUpTime += attachmentStats.m_StartUpTime;
				stats.m_RecoveryTime += attachmentStats.m_RecoveryTime;
			}
		}
		return stats;
	}

	public void SetParentAttachment(BaseAttachment attachment)
	{
		m_ParentAttachment = attachment;
	}

	public void AddAttachment(GameObject newAttachment, int mountPointIndex)
	{
		if(mountPointIndex < m_Stats.m_MountPoints.Count)
		{
			newAttachment.transform.parent = m_Stats.m_MountPoints[mountPointIndex];
			newAttachment.transform.localPosition = Vector3.zero;
			newAttachment.transform.rotation = Quaternion.identity;

			newAttachment.GetComponent<BaseAttachment>().SetParentAttachment(this);
		}
	}

	public int AddAttachment(GameObject newAttachment)
	{
		int indexAttached = -1;
		bool attached = false;

		foreach(Transform mountPoint in m_Stats.m_MountPoints)
		{
			if(!attached)
			{
				if(mountPoint.childCount == 0)
				{
					newAttachment.transform.parent = mountPoint;
					newAttachment.transform.localPosition = Vector3.zero;
					newAttachment.transform.rotation = Quaternion.identity;
					
					newAttachment.GetComponent<BaseAttachment>().SetParentAttachment(this);

					attached = true;
				}

				indexAttached++;
			}
		}

		return indexAttached;
	}

	public void RemoveAttachment(int mountPointIndex)
	{
		if(mountPointIndex < m_Stats.m_MountPoints.Count && m_Stats.m_MountPoints [mountPointIndex].childCount > 0)
		{
			GameObject attachment = m_Stats.m_MountPoints [mountPointIndex].GetChild (0).gameObject;
			attachment.transform.parent = null;
			attachment.GetComponent<BaseAttachment>().SetParentAttachment(null);

			Destroy (attachment);
		}
	}

	public GameObject GetAttachment(int mountPointIndex)
	{
		if(mountPointIndex < m_Stats.m_MountPoints.Count && m_Stats.m_MountPoints [mountPointIndex].childCount > 0)
		{
			GameObject attachment = m_Stats.m_MountPoints [mountPointIndex].GetChild (0).gameObject;			
			return attachment;
		}
		else
		{
			return null;
		}
	}

	public void SwapAttachment(int firstIndex, int secondIndex)
	{
		Transform temp = m_Stats.m_MountPoints [firstIndex].GetChild (0);
		temp.parent = m_Stats.m_MountPoints [secondIndex];
		temp.localPosition = Vector3.zero;
		temp.localRotation = Quaternion.identity;
		temp.localScale = Vector3.one;

		temp = m_Stats.m_MountPoints [secondIndex].GetChild (0);
		temp.parent = m_Stats.m_MountPoints [firstIndex];
		temp.localPosition = Vector3.zero;
		temp.localRotation = Quaternion.identity;
		temp.localScale = Vector3.one;
	}

	public void ToggleCollider(bool isToggled)
	{
		if(m_Collider == null)
		{
			m_Collider = GetComponent<BoxCollider> ();
		}

		m_Collider.enabled = isToggled;

		foreach(Transform attachment in m_Stats.m_MountPoints)
		{
			if(attachment.childCount > 0)
			{
				attachment.GetChild (0).GetComponent<BaseAttachment>().ToggleCollider(isToggled);
			}
		}
	}

	void OnTriggerEnter(Collider otherCollider)
	{
		Health otherHealth = otherCollider.GetComponent<Health>();

		if(otherHealth != null)
		{
			HandleHit (otherHealth);
		}
	}

	public void HandleHit(Health health)
	{
		if(m_ParentAttachment != null)
		{
			m_ParentAttachment.HandleHit (health);
		}
		else
		{
			if(health.GetComponentInChildren<BaseAttachment>() != this && (m_IsDrunk || health.CompareTag(tag)))
			{
				Vector3 knockbackSpeed = (health.transform.position - transform.position).normalized;
				knockbackSpeed.y = Constants.KNOCKBACK_VERTICAL_SPEED;
				knockbackSpeed *= m_TotalStats.m_Knockback;

				health.Damage(m_TotalStats.m_Damage, knockbackSpeed);

				foreach(MiscEffects effect in m_MiscEffects)
				{
					effect.DoEffect(health);
				}
			}
		}
	}

	public void SetTotalStatsAfterCrafting()
	{
		m_TotalStats = GetStats ();

		m_MiscEffects.Clear ();

		foreach(MiscEffects effect in GetComponentsInChildren<MiscEffects>())
		{
			m_MiscEffects.Add (effect);
		}
	}

	public void SetDrunk(bool isDrunk)
	{
		m_IsDrunk = isDrunk;
	}
}
