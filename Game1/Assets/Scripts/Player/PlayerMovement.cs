using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5.0f;

	GameObject mainCam;
    CharacterController m_PlayerController;

	float m_KnockbackTimer;

	Vector3 m_CurrentSpeed;
	float m_Gravity = -10.0f;

    // Use this for initialization
    void Start()
    {
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");
        m_PlayerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
		if(m_KnockbackTimer <= 0.0f)
		{
	        float forwardDir = InputManager.Instance.PlayerMoveForwardBack();
	        float rightDir = InputManager.Instance.PlayerMoveRightLeft();

			Vector3 t = mainCam.transform.forward;
			Vector3 t1 = mainCam.transform.right;
			Vector3 dirForward = new Vector3(t.x * forwardDir, 0.0f, t.z * forwardDir);
			Vector3 dirRight = new Vector3(t1.x * rightDir, 0.0f, t1.z * rightDir);

			float previousVerticalSpeed = m_CurrentSpeed.y;

			m_CurrentSpeed = dirForward + dirRight;
			if (m_CurrentSpeed != Vector3.zero)
	        {
				transform.rotation = Quaternion.LookRotation(m_CurrentSpeed, Vector3.up);
	        }

			m_CurrentSpeed *= MoveSpeed * Time.deltaTime;

			m_CurrentSpeed.y = previousVerticalSpeed;
		}
		else
		{
			m_KnockbackTimer -= Time.deltaTime;
		}

		bool isGrounded = (m_PlayerController.Move(m_CurrentSpeed) & CollisionFlags.Below) != 0;

		if(!isGrounded)
		{
			m_CurrentSpeed.y += m_Gravity * Time.deltaTime;
		}
    }

	public void Knockback(Vector3 knockbackSpeed)
	{
		m_CurrentSpeed = knockbackSpeed;
		
		m_KnockbackTimer = Constants.KNOCKBACK_TIME;
	}
}
