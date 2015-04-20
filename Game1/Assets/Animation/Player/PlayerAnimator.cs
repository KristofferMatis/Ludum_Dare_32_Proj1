using UnityEngine;
using System.Collections;

public class PlayerAnimator : Singleton<PlayerAnimator>
{
	public Animator i_Animator;
    public enum Animations
    {
        Idle,
        Sprint,
        Run,

        Death,
		
        Bash,
        Slash,
        Stab,  
    };

    int[] m_AnimationLayers = new int[] { 0,0,0,  3,   1,1,1  };
    string[] m_AnimationNames = new string[] { "Idle", "Sprint", "Run",   
                                                "Death",   
                                                "Bash", "Slash", "Stab"};

    public void PlayAnimation(Animations animation)
    {
        if(animation > Animations.Death)
        {//Attack or Transition
            i_Animator.Play(m_AnimationNames[(int)animation], m_AnimationLayers[(int)animation]);            
        }
        else if(animation <= Animations.Death)
        {//basic motion or Death
            i_Animator.Play(m_AnimationNames[(int)animation], m_AnimationLayers[(int)animation]);
        }
    }
}
