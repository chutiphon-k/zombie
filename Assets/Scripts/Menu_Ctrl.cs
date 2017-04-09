/// <summary>
/// Menu Controller
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Ctrl : MonoBehaviour {

	public void LoadScene(string sceneName) {
		// PlayerPrefs.SetInt ("highscore", 100);

		if(sceneName == "GameOver"){
			string score = PlayerPrefs.GetString("score");
			string highscore = PlayerPrefs.GetString("highscore");
			Debug.Log(int.Parse(score));	
		} else if(sceneName == "StartPage"){

		}

		SceneManager.LoadScene (sceneName);
	}
}
