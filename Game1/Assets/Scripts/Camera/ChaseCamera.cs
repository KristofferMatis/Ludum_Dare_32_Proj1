using UnityEngine;
using System.Collections;

public class ChaseCamera : CameraBase
{
    public Vector3 CameraDistance = new Vector3(0, 2, -5);
	public float CameraRaiseSpeed = 1.0f;
	public float CameraMaxHeight = 4.0f;
	public float CameraMinHeight = -0.5f;
	public float CameraMoveEaseSpeed = 4.0f;
	public float CameraLookEaseSpeed = 3.0f;
	public float CameraRotationSpeed = 4.0f;
	public float WobbleStrength = 0.5f;
	public float WobbleSpeed = 1.2f;
    public GameObject LookAtObject;

	float m_CurrentAngle = 0.0f;
    Vector3 m_Offset;
    // Use this for initialization
    void Start()
    {
        m_Offset = LookAtObject.transform.position + CameraDistance;

        transform.position = m_Offset;
    }

	public override void CameraHandleFixedUpdate()
	{
	
	}

	public override void CameraHandleUpdate()
	{
		MoveCamera ();
		LookAt ();
	}

	void MoveCamera()
	{
		float lookLRDir = InputManager.Instance.PlayerLookRightLeft ();
		float lookUDDir = InputManager.Instance.PlayerLookUpDown ();


		ChangeCameraDistanceHeight (ref CameraDistance);
		m_Offset = LookAtObject.transform.position + CameraDistance;


		//testing only with mac because controller is wierd and no mouse :p
		if(Input.GetKey(KeyCode.E))
		{
			m_CurrentAngle -= CameraRotationSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Q))
		{
			m_CurrentAngle += CameraRotationSpeed * Time.deltaTime;
		}

		Quaternion rotation = Quaternion.Euler (0, m_CurrentAngle, 0);

		Vector3 goal = RotateAroundPoint (m_Offset, LookAtObject.transform.position, rotation);

		transform.position = Vector3.Lerp (transform.position, goal, CameraMoveEaseSpeed * Time.deltaTime);

	}

	void LookAt()
	{
		Vector3 dir = LookAtObject.transform.position - transform.position;

		Quaternion rotation = Quaternion.LookRotation (dir, Vector3.up);
		Vector3 temp;

		float isMovingForward = InputManager.Instance.PlayerMoveForwardBack();
		float isMovingRight = InputManager.Instance.PlayerMoveRightLeft();

		if(isMovingForward != 0 || isMovingRight != 0)
		{
			temp = new Vector3(0,0,Mathf.Sin (Time.time * WobbleSpeed) * WobbleStrength);
			rotation *= Quaternion.Euler (temp);
		}

		transform.rotation = Quaternion.Lerp (transform.rotation, rotation, CameraLookEaseSpeed * Time.deltaTime);
	}

	void ChangeCameraDistanceHeight(ref Vector3 cameraDist)
	{

		if(Input.GetKey(KeyCode.R) && CameraDistance.y <= CameraMaxHeight)
		{
			cameraDist.y += CameraRaiseSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.F) && CameraDistance.y >= CameraMinHeight)
		{
			cameraDist.y -= CameraRaiseSpeed * Time.deltaTime;
		}
	}

	Vector3 RotateAroundPoint(Vector3 aPos, Vector3 aPivot, Quaternion angle)
	{
		return angle * (aPos - aPivot) + aPivot;
	}
}
