using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseAttachment : MonoBehaviour 
{
	[SerializeField]
	public WeaponStats m_Stats;

	WeaponStats m_TotalStats;

	BoxCollider m_Collider;

	BaseAttachment m_ParentAttachment;

	List<MiscEffects> m_MiscEffects = new List<MiscEffects>();

	protected void Start()
	{
		m_Collider = GetComponent<BoxCollider> ();

		SetTotalStatsAfterCrafting ();
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
				stats.m_PlayerSpeed += attachmentStats.m_PlayerSpeed;
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

	public GameObject RemoveAttachment(int mountPointIndex)
	{
		if(mountPointIndex < m_Stats.m_MountPoints.Count && m_Stats.m_MountPoints [mountPointIndex].childCount > 0)
		{
			GameObject attachment = m_Stats.m_MountPoints [mountPointIndex].GetChild (0).gameObject;
			attachment.transform.parent = null;
			attachment.GetComponent<BaseAttachment>().SetParentAttachment(null);

			return attachment;
		}
		else
		{
			return null;
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

	public void ToggleCollider(bool isToggled)
	{
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
			Vector3 knockbackSpeed = (health.transform.position - transform.position).normalized;
			knockbackSpeed.y = 1.0f;
			knockbackSpeed *= m_TotalStats.m_Knockback;

			health.Damage(m_TotalStats.m_Damage, knockbackSpeed);

			foreach(MiscEffects effect in m_MiscEffects)
			{
				effect.DoEffect(health);
			}
		}
	}

	public void SetTotalStatsAfterCrafting()
	{
		m_TotalStats = GetStats ();

		foreach(MiscEffects effect in GetComponentsInChildren<MiscEffects>())
		{
			m_MiscEffects.Add (effect);
		}
	}
}
