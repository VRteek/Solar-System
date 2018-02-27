using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour {
	public Transform cam;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (cam.position.x, cam.position.y, cam.position.z);
		transform.rotation = cam.rotation;
	}
}