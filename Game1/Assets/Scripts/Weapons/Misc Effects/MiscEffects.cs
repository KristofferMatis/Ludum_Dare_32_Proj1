using UnityEngine;
using System.Collections;

public enum MiscEffectType
{
	e_Flaming,
	e_Scaling,
	e_Drunk,
	e_Afraid,
	e_Slow,
	Count,
}


public class MiscEffects : MonoBehaviour 
{
    protected MiscEffectType m_Type;

    protected const string EFFECT_TYPE_FLAMING = "Flaming";
    protected const string EFFECT_TYPE_SCALING = "Scaling";
    protected const string EFFECT_TYPE_DRUNK = "Drunk";
	protected const string EFFECT_TYPE_AFRAID = "Afraid";
	protected const string EFFECT_TYPE_SLOW = "Sticky";
    public string EffectType
    {
        get 
        {
            switch(m_Type)
            {
                case MiscEffectType.e_Flaming:
                    return EFFECT_TYPE_FLAMING;
                case MiscEffectType.e_Scaling:
                    return EFFECT_TYPE_SCALING;
                case MiscEffectType.e_Drunk:
                    return EFFECT_TYPE_DRUNK;
                case MiscEffectType.e_Afraid:
					return EFFECT_TYPE_AFRAID;
				case MiscEffectType.e_Slow:
					return EFFECT_TYPE_SLOW;
                default:
                    return "";
            };
        }
    }

	public void DoEffect(Health otherHealth)
	{
		DoEffectVirtual (otherHealth);
	}

	protected virtual void DoEffectVirtual(Health otherHealth)
	{
		// Override for specific effects ;-)
	}
}
