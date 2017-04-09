using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPageController : MonoBehaviour {
	public GameObject objectHighscore;

	// Use this for initialization
	void Start () {
		if(!PlayerPrefs.HasKey("highscore")){
			PlayerPrefs.SetString ("highscore", "000");
			objectHighscore.GetComponent<Text>().text = "000";			
		} else {
			objectHighscore.GetComponent<Text>().text = PlayerPrefs.GetString("highscore");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene (sceneName);
	}
}
