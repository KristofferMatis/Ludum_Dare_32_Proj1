using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface AnimationCallBackListener
{
	void OnAnimationCallBack(AnimationEvents animationEvent);
}

public enum AnimationEvents
{
	AttackStart,
	AttackHalf,
	AttackDone
};

public class AnimationCallBackManager : MonoBehaviour 
{
	string[] m_AnimationEvents = new string[]{"Attack Start", "Attack Half", "Attack Done"};
	List<AnimationCallBackListener> m_Listeners = new List<AnimationCallBackListener> ();
	
	public void AnimationCallBack(string animationEvent)
	{
		for(int i =0; i < m_AnimationEvents.Length; i++)
		{
			if(m_AnimationEvents[i].Equals(animationEvent, System.StringComparison.OrdinalIgnoreCase))
			{
				for(int c = 0; c < m_Listeners.Count; c++)
				{
					m_Listeners[c].OnAnimationCallBack((AnimationEvents)i);
				}
				return;
			}
		}
	}
	
	public void AddListener(AnimationCallBackListener listener)
	{
		if(!m_Listeners.Contains(listener))
			m_Listeners.Add (listener);
	}
	
	public void RemoveListener(AnimationCallBackListener listener)
	{
		if(m_Listeners.Contains(listener))
			m_Listeners.Remove (listener);
	}
}
