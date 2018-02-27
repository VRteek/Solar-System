using UnityEngine;
using System.Collections;

public class FullScreen : MonoBehaviour {

	public string SceneName;
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("escape")) {
			Application.LoadLevel(SceneName);
		}
	}

	void OnMouseDown()
	{
		//Handheld.PlayFullScreenMovie ("http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4");
	//	Handheld.PlayFullScreenMovie ("https://www.dropbox.com/s/iuibw0xv4c4nbef/Solar_System.mp4?dl=1");
	}
}
