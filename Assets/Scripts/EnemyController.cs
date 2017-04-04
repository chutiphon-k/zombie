using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	// How fast enemy move to the player
	public float moveSpeed;

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// Continuously move towards player
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed);
	}
	
}


