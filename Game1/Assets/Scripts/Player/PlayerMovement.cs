using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 8.0f;
    public float RunSpeed = 10.0f;

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
            Vector2 input = new Vector2(InputManager.Instance.PlayerMoveRightLeft(), InputManager.Instance.PlayerMoveForwardBack());
            input.Normalize();

			Vector3 t = mainCam.transform.forward;
			Vector3 t1 = mainCam.transform.right;
            Vector3 dirForward = (!CraftingMenu.Instance.IsActive) ? new Vector3(t.x * input.y, 0.0f, t.z * input.y) : Vector3.zero;
            Vector3 dirRight = (!CraftingMenu.Instance.IsActive) ? new Vector3(t1.x * input.x, 0.0f, t1.z * input.x) : Vector3.zero;

			float previousVerticalSpeed = m_CurrentSpeed.y;

			m_CurrentSpeed = dirForward + dirRight;
			if (m_CurrentSpeed != Vector3.zero)
	        {
				transform.rotation = Quaternion.LookRotation(m_CurrentSpeed, Vector3.up);
	        }

			m_CurrentSpeed *= ((!InputManager.Instance.PlayerSprint()) ? MoveSpeed: RunSpeed) * Time.deltaTime;

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
