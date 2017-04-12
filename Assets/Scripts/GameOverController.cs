using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	public GameObject objectScore, objectHighscore;

	// Use this for initialization
	void Start () {
		string score = PlayerPrefs.GetString("score");
		string highscore = PlayerPrefs.GetString("highscore");
		objectScore.GetComponent<Text>().text = score;
		if(int.Parse(score) > int.Parse(highscore)){
			highscore = score;
			PlayerPrefs.SetString ("highscore", score);
		}
		objectHighscore.GetComponent<Text>().text = highscore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void LoadScene(string sceneName) {
		SceneManager.LoadScene (sceneName);
	}
}
