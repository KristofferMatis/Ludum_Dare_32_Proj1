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

        BashStart,
        Bash,
        BashEnd,

        SlashStart,
        Slash,
        SlashEnd,

        StabStart,
        Stab,
        StabEnd       
    };

    int[] m_AnimationLayers = new int[] { 0,0,0,  4,  1,2,1, 1,2,1, 1,2,1  };
    string[] m_AnimationNames = new string[] { "Idle", "Sprint", "Run",   
                                                "Death",   
                                                "Bash Start", "Bash", "Bash End", 
                                                "Slash Start", "Slash", "Slash End", 
                                                "Stab Start", "Stab", "Stab End"};

    public void PlayAnimation(Animations animation, float AnimationLengthInSeconds = -1.0f)
    {
        if(animation > Animations.Death)
        {//Attack or Transition
            if(animation != Animations.Bash && animation != Animations.Slash && animation != Animations.Stab)
            {

            }
        }
        else if(animation <= Animations.Death)
        {//basic motion or Death
            i_Animator.Play(m_AnimationNames[(int)animation], m_AnimationLayers[(int)animation]);
        }
    }
}
