using UnityEngine;
using System.Collections;

public class CameraAutoFocus : MonoBehaviour {

	bool autofocusActivated = false;

	// Use this for initialization
	void Start () {
//#if UNITY_ANDROID
		Debug.Log ("Trying Autofocus");
		autofocusActivated = Vuforia.CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
//#endif	
	}

	// Update is called once per frame
	void Update () {
//#if UNITY_ANDROID	
		if (!autofocusActivated) {
			Debug.Log ("Trying Autofocus");
			autofocusActivated = Vuforia.CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		}
//#endif



	}


	void OnApplicationPause (bool pause)
	{
		if(pause)
		{
			// we are in background
			Debug.Log("Application is background");
			autofocusActivated = false;
		}
		else
		{
			// we are in foreground again.
		}
	}
}
