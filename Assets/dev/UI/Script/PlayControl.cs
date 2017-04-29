using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayControl : MonoBehaviour {

	public GameObject GameModePanel;
	public GameObject Shadow;
	public int GameMode;
	public bool isClicked;

	public void Start () {
		isClicked = false;
		GameMode = 0;

	}

	public void Update () {
		Shadow.SetActive (isClicked);
		GameModePanel.SetActive (isClicked);

	}

	public void switchClick () {
		if (isClicked)
			isClicked = false;
		else
			isClicked = true;
	}
		
	public void SingleClick() {
		GameMode = 1;
		Debug.Log (GameMode);
	}

	public void MultiClick() {
		GameMode = 2;
		Debug.Log (GameMode);
	}

}