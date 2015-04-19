using UnityEngine;
using System.Collections;

public class BaseBaseWeapon : BaseAttachment 
{
    public Transform i_MountPoint;
    const float MOUNTING_LERP_SPEED = 0.5f;
	
	// Update is called once per frame
	void Update () 
    {
	    if(i_MountPoint != null)
        {
            transform.position = Vector3.Lerp(transform.position, i_MountPoint.position, MOUNTING_LERP_SPEED * Time.deltaTime);
        }
	}
}
