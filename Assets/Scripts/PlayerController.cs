using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour {

	// mousePosition in screen's pixel
	private Vector2 mousePosition;
	
	// Define how fast can a player follow mouse
	public float lerpSpeed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		// Get mousePosition from screen's pixel => Scene's coordinate
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		// Smooths player's movement to follow mouse
		transform.position = Vector2.Lerp(transform.position, mousePosition, lerpSpeed);
	}
	
}
