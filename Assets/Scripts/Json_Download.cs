using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class parseJSON {

	public List<string> url;
	public List<int> index;
	public List<float> FileSize;
	public List<float> FileSizeRatio;

}

public class Json_Download : MonoBehaviour {

	public GameObject contents, slider0, CheckCanvas, NetworkCanvas, CompleteCanvas, interceptor;
	WWW Json_www, Voice_Over_www;
	JsonData jsonvale;
	parseJSON parsejson;
	short urlList_Counter = 0, counter = 0;
	public Text Percentage;
	public Slider slider;
	float size, downloaded, AfterPercentage_Value;
	long length;
	bool CachingFiles_Finished, Another_Download_In_Progress;
	string Selected_Language, Lan_temp, Current_Language, Current_Json;
	string Domain = "https://multilang.vrteek.com";

	void Start () {
		Screen.sleepTimeout = 0;
		StartCoroutine (Download_JsonData ());

	}

	void Update () {

		if (Voice_Over_www != null) {
			DownloadProgress ();
			if (Voice_Over_www.error != null) {

				NetworkCanvas.SetActive (true);
				interceptor.SetActive (true);
			}

		}
	}
	IEnumerator Download_JsonData () {

		//
		//string url = "http://192.168.1.135:7000/api/v1/sounds";

		//string url = "https://vrteek.000webhostapp.com/Kwakbbbbb.json";

		//string url = "https://mahmoudmostafarabah.000webhostapp.com/NEW/Kwakbbbbb.json";

		//string url = "https://mmhrabah.000webhostapp.com/New/Kwakbbbbb.json";

		string url = "https://multilang.vrteek.com/api/v1/sounds/planets";
		Json_www = new WWW (url);
		yield return Json_www;
		if (Json_www.error == null) {
			Processjson (Json_www.text);
		} else {
			Debug.Log ("ERROR: " + Json_www.error);
			NetworkCanvas.SetActive (true);
			interceptor.SetActive (true);
		}
	}
	private void Processjson (string jsonString) {
		jsonvale = JsonMapper.ToObject (jsonString);

		for (int i = 0; i < jsonvale["data"]["languages"].Count; i++) {
			print (jsonvale["data"]["languages"].Count);
			print (jsonvale["data"]["languages"][i][0]["language"].ToString ());
			GameObject Prefab = (GameObject) Resources.Load (jsonvale["data"]["languages"][i][0]["language"].ToString ());

			//instantiate and Remove (Clone) 
			GameObject Object_After_Instantiate = (GameObject) Instantiate (Prefab, Vector3.zero, Quaternion.identity, contents.transform);
			Object_After_Instantiate.name = Prefab.name;
			Object_After_Instantiate.transform.localPosition = Vector3.zero;
			Button btn = Object_After_Instantiate.GetComponent<Button> ();
			btn.onClick.AddListener (() => Caching_VoiceOver (btn.name, jsonString));
			contents.GetComponent<RectTransform> ().position = new Vector3 (140, contents.GetComponent<RectTransform> ().position.y, contents.GetComponent<RectTransform> ().position.z);

		}
	}

	public void Caching_VoiceOver (String LanguagePressed, String Json) {
		Current_Language = LanguagePressed;
		Current_Json = Json;
		// first time
		if (counter == 0) {
			LanguageChoice (Json, LanguagePressed);
			counter++;
		}
		// later times with different languages 
		else if (counter > 0 && Lan_temp != LanguagePressed) {
			CheckCanvas.SetActive (true);
			interceptor.SetActive (true);
		}
		// later times with same language
		else if (counter > 0 && Lan_temp == LanguagePressed) {
			return;
		}

	}

	IEnumerator Download_VoiceOverClips (string LAN, string url, int index) {
		Lan_temp = LAN;
		if (!File.Exists (Application.persistentDataPath + "/" + "Languages/" + LAN + "/" + index + ".mp3")) {
			Voice_Over_www = new WWW (url);

			yield return Voice_Over_www;
			if (Voice_Over_www.error == null && Voice_Over_www.isDone) {

				print ("We're Gonna Download it Yeaaaaaaaaaaaaah ! ");
				print ("Caching....");
				File.WriteAllBytes (Application.persistentDataPath + "/" + "Languages/" + LAN + "/" + index + ".mp3", Voice_Over_www.bytes);
				urlList_Counter++;
				if (urlList_Counter != parsejson.url.Count) {

					// print (urlList_Counter);
					downloaded += parsejson.FileSizeRatio[urlList_Counter];

					StartCoroutine (Download_VoiceOverClips (LAN, parsejson.url[urlList_Counter], parsejson.index[urlList_Counter]));

				} else if (urlList_Counter == parsejson.url.Count) {
					// print ("finish LEeeeeeeeeh ");
					CachingFiles_Finished = true;
					Selected_Language = LAN;

				}

			} else {
				Debug.Log ("ERROR: " + Voice_Over_www.error);
				NetworkCanvas.SetActive (true);
				interceptor.SetActive (true);
			}

		} else {

			length = new System.IO.FileInfo (Application.persistentDataPath + "/" + "Languages/" + LAN + "/" + index + ".mp3").Length;

			if (length >= parsejson.FileSize[urlList_Counter]) {
				// print ("Mawgood " + "Size in Bytes : " + length);
				urlList_Counter++;
				if (urlList_Counter != parsejson.url.Count) {

					downloaded += parsejson.FileSizeRatio[urlList_Counter];
					StartCoroutine (Download_VoiceOverClips (LAN, parsejson.url[urlList_Counter], parsejson.index[urlList_Counter]));
					if (urlList_Counter == parsejson.url.Count) {
						slider0.SetActive (false);
						PlayerPrefs.SetString ("Selected_Language", LAN);
						SceneManager.LoadScene ("Menu");
					}

				}
			} else {
				StartCoroutine (Download_VoiceOverClips (LAN, parsejson.url[urlList_Counter], parsejson.index[urlList_Counter]));
			}

		}
	}

	void DownloadProgress () {

		if (urlList_Counter != parsejson.url.Count) {

			if (urlList_Counter == 0) {

				slider.value = Voice_Over_www.progress * parsejson.FileSizeRatio[0];

			}
			if (urlList_Counter > 0 && slider.value < Voice_Over_www.progress * parsejson.FileSizeRatio[urlList_Counter] + downloaded) {

				slider.value = Voice_Over_www.progress * parsejson.FileSizeRatio[urlList_Counter] + downloaded;

			}
		}

		if (CachingFiles_Finished == true) {
			Percentage.text = "" + 100;
			slider.value += 0.1f;

			if (slider.value == 1) {
				PlayerPrefs.SetString ("Selected_Language", Selected_Language);
				CompleteCanvas.SetActive (true);
				interceptor.SetActive (true);

			}
		} else {
			AfterPercentage_Value = slider.value * 100;
			Percentage.text = "" + (int) AfterPercentage_Value;
		}

	}

	public void Add_New_Language () {
		Application.OpenURL ("http://www.vrteek.com/");
	}

	void LanguageChoice (string Json, String LanguagePressed) {
		urlList_Counter = 0;
		slider.value = 0;
		AfterPercentage_Value = 0;
		downloaded = 0;
		Percentage.text = "0 ";
		string url = null;
		size = 0.0f;
		float sizePerFile = 0;

		jsonvale = JsonMapper.ToObject (Json);
		parsejson = new parseJSON ();
		parsejson.url = new List<string> ();
		parsejson.index = new List<int> ();
		parsejson.FileSizeRatio = new List<float> ();
		parsejson.FileSize = new List<float> ();
		if (!File.Exists (Application.dataPath + "/Resources/" + "Languages/" + LanguagePressed + "/")) {
			System.IO.Directory.CreateDirectory (Application.persistentDataPath + "/" + "Languages/" + LanguagePressed + "/");
		}

		for (int i = 1; i < jsonvale["data"]["languages"][LanguagePressed].Count; i++) {
			url = Domain + jsonvale["data"]["languages"][LanguagePressed][i]["url"].ToString ();
			// url = jsonvale["data"]["languages"][LanguagePressed][i]["url"].ToString ();
			
			size += float.Parse (jsonvale["data"]["languages"][LanguagePressed][i]["size"].ToString ());
			sizePerFile = float.Parse (jsonvale["data"]["languages"][LanguagePressed][i]["size"].ToString ());
			parsejson.url.Add (url);
			parsejson.index.Add (i);
			parsejson.FileSize.Add (sizePerFile);

		}
		for (int i = 0; i < parsejson.FileSize.Count; i++) {
			parsejson.FileSizeRatio.Add (parsejson.FileSize[i] / size);

		}

		slider0.SetActive (true);
		StartCoroutine (Download_VoiceOverClips (LanguagePressed, parsejson.url[0], parsejson.index[0]));

	}

	public void OnCancelDownload () {
		CheckCanvas.SetActive (false);
		interceptor.SetActive (false);
		LanguageChoice (Current_Json, Current_Language);
	}
	public void OnResumeDownload () {
		CheckCanvas.SetActive (false);
		interceptor.SetActive (false);
	}

	public void finishDownload () {
		SceneManager.LoadScene ("Menu");
	}

	public void Refresh () {
		NetworkCanvas.SetActive (false);
		interceptor.SetActive (false);
		//StartCoroutine (Download_JsonData ());
		SceneManager.LoadScene ("JSON_Download");
	}

	public void CloseNetworkCanvas () {
		NetworkCanvas.SetActive (false);
		interceptor.SetActive (false);
	}

}