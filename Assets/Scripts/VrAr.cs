using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Vuforia;

public class VrAr : MonoBehaviour {

	// Use this for initialization
	void Awake () {

		StartCoroutine (LoadDevice ("cardboard"));
		
			QualitySettings.SetQualityLevel(0);

	}
	IEnumerator LoadDevice (string newDevice) {
		XRSettings.LoadDeviceByName (newDevice);
		yield return null;
		XRSettings.enabled = true;
	}
}