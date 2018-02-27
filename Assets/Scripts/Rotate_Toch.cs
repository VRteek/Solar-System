using UnityEngine;
using System.Collections;

public class Rotate_Toch : MonoBehaviour {
	
	float speed = 0.4f;
	bool canRotate = false;
	Transform cachedTransform;
	
	public bool CanRotate {
		get { return canRotate; } 
		private set { canRotate = value; } 
	}
	
	void Start () {
		// Make reference to transform
		cachedTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			
			// Switch through touch events
			switch (Input.GetTouch (0).phase) {
			case TouchPhase.Began:  
				if (VerifyTouch (touch))
					CanRotate = true;
				break;
			case TouchPhase.Moved:  
				if (CanRotate)
					RotateObject (touch);
				break;
			case TouchPhase.Ended:  
				CanRotate = false;
				break;
			}
		}
	}
	
	bool VerifyTouch (Touch touch) {
		Ray ray = Camera.main.ScreenPointToRay (touch.position);
		RaycastHit hit;
		
		// Check if there is a collider attached already, otherwise add one on the fly
		if (GetComponent<Collider>() == null) gameObject.AddComponent (typeof(BoxCollider));
		
		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.gameObject == this.gameObject)
				return true;
		}
		return false;
	}
	
	void RotateObject (Touch touch) {
		cachedTransform.Rotate (new Vector3 (touch.deltaPosition.y, -touch.deltaPosition.x, 0) * speed, Space.World);
	}   
	

	
}
