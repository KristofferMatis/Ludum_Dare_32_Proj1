using UnityEngine;
using System.Collections;

public class ClickHandler 
{
    public CrafingMenu m_CraftingMenu;

	// Use this for initialization
    public void Start() 
	{	
	}
	
	// Update is called once per frame
    public void Update() 
	{
        if (!m_CraftingMenu.IsActive)
            return;

        if(Input.GetMouseButtonDown(0))
        {//click
            OnClickDown();
        }
        else if(Input.GetMouseButton(0))
        {//mouse being held
            OnClick();
        }
        else if(Input.GetMouseButtonUp(0))
        {//mouse Released
            OnClickUp();
        }
	}

    void OnClickDown()
    {
    }

	void OnClick()
	{
	}	

	void OnClickUp()
	{
	}
}
