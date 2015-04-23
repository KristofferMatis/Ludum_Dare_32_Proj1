using UnityEngine;
using System.Collections;

public class PlayerAttack : Attack, AnimationCallBackListener
{	
	// Update is called once per frame
    public bool i_CanSpam = true; //TODO remove this line
    bool m_CanAttack = true;
	void Update () 
	{
        base.Update();

        if (!m_CanAttack && !i_CanSpam)
            return;

		if(m_WeaponEquipped == null)
		    return;

		if (!m_WeaponDrawn)
			return;

		if(InputManager.Instance.PlayerAttack1())
		{
			DoAttack();
            m_CanAttack = false;
		}        
	}

    public void OnAnimationCallBack(AnimationEvents animationEvent)
    {
        switch (animationEvent)
        {
            case AnimationEvents.AttackStart:
                break;
            case AnimationEvents.WindUpDone:
                break;
            case AnimationEvents.AttackDone:
                break;
            case AnimationEvents.RecoveryDone:
                m_CanAttack = true;
                break;
        };
    }
}
