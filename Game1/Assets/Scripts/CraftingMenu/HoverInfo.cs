using UnityEngine;
using System.Collections;

public class HoverInfo
{
    public CraftingMenu m_CraftingMenu;

	// Use this for initialization
    public void Start() 
    {
	    
	}
	
	// Update is called once per frame
	public void Update () 
    {
        if (!m_CraftingMenu.IsActive)
            return;
	}
}
