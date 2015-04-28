using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CraftingRecipe
{
	public MiscEffectType m_Effect;

	public List<string> m_AttachmentsRequired;
}

public class CraftingRecipesManager : Singleton<CraftingRecipesManager>
{
	public List<CraftingRecipe> m_Recipes;

	Dictionary<string, int> m_ItemRarities = new Dictionary<string, int> ();

	void Start()
	{
		foreach(CraftingRecipe recipe in m_Recipes)
		{			
			foreach(string type in recipe.m_AttachmentsRequired)
			{
				if(m_ItemRarities.ContainsKey(type))
				{
					m_ItemRarities[type]++;
				}
				else
				{
					m_ItemRarities.Add (type, 1);
				}
			}
		}
	}

	public List<MiscEffectType> GetEffectsFromAttachments(List<string> attachmentTypes)
	{
		List<MiscEffectType> result = new List<MiscEffectType> ();

		foreach(CraftingRecipe recipe in m_Recipes)
		{
			bool containsEverything = true;

			foreach(string type in recipe.m_AttachmentsRequired)
			{
				if(containsEverything)
				{
					containsEverything &= attachmentTypes.Contains (type);
				}
			}
			
			if(containsEverything)
			{
				result.Add (recipe.m_Effect);
			}
		}

		return result;
	}

	public int ItemRarity(string type)
	{
		int result = 0;

		if(m_ItemRarities.ContainsKey(type))
		{
			result = m_ItemRarities[type];
		}

		return result;
	}
}
