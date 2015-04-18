using UnityEngine;
using System.Collections;

public class ChaseCamera : CameraBase
{
    public Vector3 CameraDistance = new Vector3(0, 2, -5);
    public GameObject LookAtObject;

    Vector3 m_Offset;
    // Use this for initialization
    void Start()
    {
        m_Offset = LookAtObject.transform.position + CameraDistance;

        transform.position = m_Offset;
    }

    // Update is called once per frame
    void Update()
    {
        m_Offset = LookAtObject.transform.position + CameraDistance;

        transform.position = m_Offset;
    }
}
