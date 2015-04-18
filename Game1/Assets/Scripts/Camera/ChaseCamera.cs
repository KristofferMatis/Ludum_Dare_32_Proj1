using UnityEngine;
using System.Collections;

public class ChaseCamera : CameraBase
{
    public Vector3 CameraDistance = new Vector3(0, 2, -5);
	public float CameraMoveEaseSpeed = 4.0f;
	public float CameraLookEaseSpeed = 3.0f;
	public float CameraRotationSpeed = 4.0f;
    public GameObject LookAtObject;

	float m_CurrentAngle = 0.0f;
    Vector3 m_Offset;
    // Use this for initialization
    void Start()
    {
        m_Offset = LookAtObject.transform.position + CameraDistance;

        transform.position = m_Offset;
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

		m_Offset = LookAtObject.transform.position + CameraDistance;

		Quaternion rotation =  Quaternion.Euler(0, m_CurrentAngle,0);

		//testing only with mac because controller is wierd and no mouse :p
		if(Input.GetKey(KeyCode.E))
		{
			m_CurrentAngle -= CameraRotationSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Q))
		{
			m_CurrentAngle += CameraRotationSpeed * Time.deltaTime;
		}
		Vector3 goal = RotateAroundPoint (m_Offset, LookAtObject.transform.position, rotation);

		transform.position = Vector3.Lerp (transform.position, goal, CameraMoveEaseSpeed * Time.deltaTime);

	}

	void LookAt()
	{
		Vector3 dir = LookAtObject.transform.position - transform.position;

		Quaternion rotation = Quaternion.LookRotation (dir, Vector3.up);

		transform.rotation = Quaternion.Lerp (transform.rotation, rotation, CameraLookEaseSpeed * Time.deltaTime);
	}

	Vector3 RotateAroundPoint(Vector3 aPos, Vector3 aPivot, Quaternion angle)
	{
		return angle * (aPos - aPivot) + aPivot;
	}
}
