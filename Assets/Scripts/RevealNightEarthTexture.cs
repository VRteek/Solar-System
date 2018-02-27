using System.Collections;
using UnityEngine;

public class RevealNightEarthTexture : MonoBehaviour {
	public GameObject goLight;
	Transform tfLight;
	// Use this for initialization
	void Start () {

		if (goLight) {
			tfLight = goLight.transform;
		}

	}

	// Update is called once per frame
	void Update () {

		if (tfLight) {
			tfLight.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 4.3f);
			GetComponent<Renderer> ().material.SetVector ("_LightPos", tfLight.position);
			GetComponent<Renderer> ().material.SetVector ("_LightDir", tfLight.forward);
		}
	}
}