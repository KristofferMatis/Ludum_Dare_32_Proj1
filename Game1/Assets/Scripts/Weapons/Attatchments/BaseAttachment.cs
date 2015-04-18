using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseAttachment : MonoBehaviour 
{
	public WeaponStats m_Stats;

	public WeaponStats GetStats()
	{
		WeaponStats stats = new WeaponStats (m_Stats);

		foreach(Transform attachment in m_Stats.m_MountPoints)
		{
			if(attachment.GetChild (0) != null)
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

	public void AddAttachment(GameObject newAttachment, int mountPointIndex)
	{
		if(mountPointIndex < m_Stats.m_MountPoints.Count)
		{
			newAttachment.transform.parent = m_Stats.m_MountPoints[mountPointIndex];
		}
	}

	public GameObject RemoveAttachment(int mountPointIndex)
	{
		if(mountPointIndex < m_Stats.m_MountPoints.Count)
		{
			GameObject attachment = m_Stats.m_MountPoints [mountPointIndex].GetChild (0).gameObject;
			attachment.transform.parent = null;

			return attachment;
		}
		else
		{
			return null;
		}
	}

	public GameObject GetAttachment(int mountPointIndex)
	{
		if(mountPointIndex < m_Stats.m_MountPoints.Count)
		{
			GameObject attachment = m_Stats.m_MountPoints [mountPointIndex].GetChild (0).gameObject;			
			return attachment;
		}
		else
		{
			return null;
		}
	}
}
