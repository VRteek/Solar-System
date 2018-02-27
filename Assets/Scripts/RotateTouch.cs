using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lean.Touch {
	public class RotateTouch : MonoBehaviour {

		
		// Use this for initialization
		private float movePos;
		private float rotationSpeed = 0.2F;
		private float lerpSpeed = 0.2F;
		private Vector3 theSpeed;
		private Vector3 avgSpeed;
		private bool isDragging = true;
		private Vector3 targetSpeedX;

		protected virtual void OnEnable () {
			// Hook into the events we need
			LeanTouch.OnFingerDown += OnFingerDown;
			LeanTouch.OnFingerSet += OnFingerSet;
			LeanTouch.OnFingerUp += OnFingerUp;
			LeanTouch.OnGesture += OnGesture;
		}

		protected virtual void OnDisable () {
			// Unhook the events
			LeanTouch.OnFingerDown -= OnFingerDown;
			LeanTouch.OnFingerSet -= OnFingerSet;
			LeanTouch.OnFingerUp -= OnFingerUp;
			LeanTouch.OnGesture -= OnGesture;
		}

		public void OnFingerDown (LeanFinger finger) {
			isDragging = true;
		}

		public void OnFingerSet (LeanFinger finger) {
			isDragging = false;
			//theSpeed = avgSpeed;
			float i = Time.deltaTime * lerpSpeed;
			theSpeed = Vector3.Lerp (theSpeed, Vector3.zero, 0.02f);
		}

		public void OnFingerUp (LeanFinger finger) {
			isDragging = false;
			float i = Time.deltaTime * lerpSpeed;
			theSpeed = Vector3.Lerp (theSpeed, Vector3.zero, 0.02f);
		}

	

		public void OnGesture (List<LeanFinger> fingers) {
			isDragging = true;
			movePos = LeanGesture.GetScreenDelta (fingers).x;
			theSpeed = new Vector3 (movePos, 0.0f, 0.0F);
			avgSpeed = Vector3.Lerp (avgSpeed, theSpeed, Time.deltaTime);
		}

		// Update is called once per frame
		void LateUpdate () {
			
			transform.Rotate (-Vector3.up * theSpeed.x * rotationSpeed);
		}
	
	}
}