using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5.0f;

    CharacterController m_PlayerController;

    // Use this for initialization
    void Start()
    {
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

        Vector3 moveDir = new Vector3(rightDir, 0.0f, forwardDir);

        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        }

        moveDir *= MoveSpeed * Time.deltaTime;

        m_PlayerController.Move(moveDir);

    }
}
