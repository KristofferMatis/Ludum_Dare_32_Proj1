using UnityEngine;
using System.Collections;

public class EnemyAnimator : MonoBehaviour 
{
	public Animator i_Animator;
	public enum EnemyAnimations
	{
		Idle,
		Sprint,
		Run,
		
		Death,
		
		Attack 
	};
	
	int[] m_AnimationLayers = new int[] { 0,0,0,  3,   1 };
	string[] m_AnimationNames = new string[] { "Idle", "Sprint", "Run",   
		"Death",   
		"Attack",};
	
	public void PlayAnimation(EnemyAnimations animation)
	{
		if(animation > EnemyAnimations.Death)
		{//Attack or Transition
			i_Animator.Play(m_AnimationNames[(int)animation], m_AnimationLayers[(int)animation]);            
		}
		else if(animation <= EnemyAnimations.Death)
		{//basic motion or Death
			i_Animator.Play(m_AnimationNames[(int)animation], m_AnimationLayers[(int)animation]);
		}
	}
}
