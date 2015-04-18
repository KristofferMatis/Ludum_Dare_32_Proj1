using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5.0f;

	GameObject mainCam;
    CharacterController m_PlayerController;

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
        float forwardDir = InputManager.Instance.PlayerMoveForwardBack();
        float rightDir = InputManager.Instance.PlayerMoveRightLeft();

		Vector3 t = mainCam.transform.forward;
		Vector3 t1 = mainCam.transform.right;
		Vector3 dirForward = new Vector3(t.x * forwardDir, 0.0f, t.z * forwardDir);
		Vector3 dirRight = new Vector3(t1.x * rightDir, 0.0f, t1.z * rightDir);

		Vector3 moveDir = dirForward + dirRight;
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        }

        moveDir *= MoveSpeed * Time.deltaTime;

		m_PlayerController.Move(moveDir);

    }
}
