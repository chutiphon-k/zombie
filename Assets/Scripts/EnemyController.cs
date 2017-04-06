using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyController : MonoBehaviour {

	// How fast enemy move to the player
	public float moveSpeed;

	private GameObject player;

	void Awake () {
		// Variables initialize
			moveSpeed = 0.05f;
			
		player = GameObject.Find("Player");
	}
	
	void Update () {
		// Continuously move towards player
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed);
	}
	
}


