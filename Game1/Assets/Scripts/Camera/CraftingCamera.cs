using UnityEngine;
using System.Collections;

public class CraftingCamera : CameraBase 
{
    public Transform Player;
    public Transform CameraTarget;
    public float CameraOffsetLerpSpeed = 10.0f;    
    public float CameraRotationLerpSpeed = 10.0f;
    public float CameraFOV = 60.0f;
    public float CameraFOVLerpSpeed = 2.0f;

    Camera m_Camera;
    LayerMask m_MenuLayers = new LayerMask();
    string[] m_MenuLayersString = { "Interactive Menu Piece", "Drag and Drop" };

    CameraBase m_OtherCameraScript;

	void Start () 
    {
        m_MenuLayers = LayerMask.GetMask(m_MenuLayersString);
        m_Camera = gameObject.GetComponent<Camera>();
        m_Camera.cullingMask = ~m_MenuLayers.value;

        m_OtherCameraScript = CameraManager.Instance.CurrentCameraAction;
	}

    void Update()
    {
        //TODO: REMOVE THIS IT IS FOR TESTING ONLY
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
			if(!CraftingMenu.Instance.IsActive)
			{
	            CraftingMenu.Instance.EnterMenu();
	            CameraManager.Instance.CurrentCameraAction = this;
	            m_Camera.cullingMask ^= m_MenuLayers.value;
			}
        }
    }

    public void SetIsOn(bool isOn)
    {
        if (isOn)
        {
            if (!CraftingMenu.Instance.IsActive)
            {
                CraftingMenu.Instance.EnterMenu();
                CameraManager.Instance.CurrentCameraAction = this;
                m_Camera.cullingMask ^= m_MenuLayers.value;
            }
        }
        else
        {
            if (CraftingMenu.Instance.IsActive)
            {
                CraftingMenu.Instance.ExitMenu();
                CameraManager.Instance.CurrentCameraAction = m_OtherCameraScript;
                m_Camera.cullingMask ^= m_MenuLayers.value;
            }
        }
    }
	
    public override void CameraHandleUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, CameraTarget.transform.position, CameraOffsetLerpSpeed * Time.deltaTime);

        transform.forward = Vector3.RotateTowards(transform.forward, CameraTarget.forward, CameraRotationLerpSpeed * Time.deltaTime, CameraRotationLerpSpeed * Time.deltaTime);

        m_Camera.fieldOfView = Mathf.Lerp(m_Camera.fieldOfView, CameraFOV, CameraFOVLerpSpeed * Time.deltaTime);
	}
}
