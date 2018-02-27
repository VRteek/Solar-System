using UnityEngine;
using System.Collections;

public class LoadScne : MonoBehaviour {


	void Start () {
		StartCoroutine (LoadScene());
	}

	IEnumerator LoadScene()
	{
		yield return new WaitForSeconds (2.5f);
		Application.LoadLevel (1);

	}

}
