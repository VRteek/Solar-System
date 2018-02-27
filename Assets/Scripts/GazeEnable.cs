using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeEnable : MonoBehaviour {
	public SpriteRenderer gaze, center, border;
	public Image button;
	public Color[] x;

	void Start () {

	}
	// Update is called once per frame
	void Update () {

		if (transform.localRotation.eulerAngles.x >= 35 && transform.localRotation.eulerAngles.x <= 100) {
			gaze.color = Color.Lerp (Color.clear, x[0], transform.localRotation.eulerAngles.x / 70);
			center.color = Color.Lerp (Color.clear, x[1], transform.localRotation.eulerAngles.x / 70);
			border.color = Color.Lerp (Color.clear, x[1], transform.localRotation.eulerAngles.x / 70);
			button.color = Color.Lerp (Color.clear, x[1], transform.localRotation.eulerAngles.x / 70);
		} else {
			button.color = border.color = center.color = gaze.color = Color.clear;
		}
	}
}