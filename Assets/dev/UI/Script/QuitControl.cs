using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class QuitControl : MonoBehaviour {

	public GameObject QuitPanel;
	public GameObject Shadow;
	public bool isQuit;

	public void Start () {
		isQuit = false;

	}

	public void Update () {
		QuitPanel.SetActive (isQuit);
		Shadow.SetActive (isQuit);
	
	}

	public void switchQuit () {
		if (isQuit)
			isQuit = false;
		else
			isQuit = true;
	}

	public void DoQuit () {
		Debug.Log ("Quit Game");
		Application.Quit ();
		//UnityEditor.EditorApplication.Exit (0);
	}


}
