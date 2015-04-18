using UnityEngine;
using System.Collections;

public class Flaming : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		MeshFilter meshFilter = GetComponentInChildren<MeshFilter> ();

		GameObject flameParticles = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/Flaming"), meshFilter.transform.position, meshFilter.transform.rotation);

		flameParticles.transform.parent = meshFilter.transform;

		flameParticles.transform.localPosition = Vector3.zero;
		flameParticles.transform.localRotation = Quaternion.identity;
		flameParticles.transform.localScale = Vector3.one;

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
