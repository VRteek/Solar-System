using UnityEngine;
using System.Collections;

public class Move_to : MonoBehaviour {

	public Transform Near_Camera;
	void OnMouseDown()
	{
		if (transform.position.y-Near_Camera.position.y <1f) {

		}
		else {
			transform.position=Vector3.Lerp(transform.position,Near_Camera.position,0.5f);
		}
	}
}
