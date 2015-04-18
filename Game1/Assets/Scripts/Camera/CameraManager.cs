using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager>
{
    public CameraBase CameraMovement;
    
    CameraBase m_CurrentCameraAction;
    // Use this for initialization
    void Start()
    {
		m_CurrentCameraAction = CameraMovement;
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
