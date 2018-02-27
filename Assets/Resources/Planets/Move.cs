using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	public GameObject Sun;
	public float RotaionSpeed;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		transform.RotateAround (Sun.transform.position, Vector3.up, RotaionSpeed*Time.deltaTime*3);
		transform.Rotate (Vector3.up, -30f * Time.deltaTime);
	}
}
