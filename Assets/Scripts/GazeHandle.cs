using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeHandle : MonoBehaviour, TimeInputHandler {
	public GazeEnable x;
	public void HandleTimedInput () {
		if (gameObject.name == "Play")
			GetComponent<Play> ().Check ();
		else {

			GetComponent<BoxCollider> ().enabled = (false);
			GetComponent<Image> ().color = Color.clear;
			Walkthrough.instance.start = true;
			x.enabled = true;
		}
	}

}