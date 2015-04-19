using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager>
{
    public CameraBase i_CameraMovement;
    
    CameraBase m_CurrentCameraAction;
    public CameraBase CurrentCameraAction
    {
        get { return m_CurrentCameraAction; }
        set 
        { 
            m_CurrentCameraAction.Deactivate();
            m_CurrentCameraAction = value;
            m_CurrentCameraAction.Activate();
        }
    }


    // Use this for initialization
    void Awake()
    {
		m_CurrentCameraAction = i_CameraMovement;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		m_CurrentCameraAction.CameraHandleFixedUpdate ();
    }

	void Update()
	{
		m_CurrentCameraAction.CameraHandleUpdate ();
	}
}
