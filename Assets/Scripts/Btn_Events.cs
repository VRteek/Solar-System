using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Btn_Events : MonoBehaviour {

	public InputField Username;
	public InputField PassWord;
	public Text WrongPass;

	public void LogIN()
	{
		if (Username.text == "admin" && PassWord.text == "admin") {
			Application.LoadLevel ("Choice_Scene");
		} else {
			WrongPass.text="Wrong PassWord Please Try Again";
		}
	}

	public void LoadScene(string Scene_Name)
	{

		Application.LoadLevel (Scene_Name);
	}

}
