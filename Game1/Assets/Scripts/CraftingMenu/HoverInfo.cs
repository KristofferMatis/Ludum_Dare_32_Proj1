using UnityEngine;
using System.Collections;

public class HoverInfo
{
	// Use this for initialization
    public void Start() 
    {
	    
	}
	
	// Update is called once per frame
	public void Update () 
    {
        if (!CraftingMenu.Instance.IsActive)
            return;
	}
}
