using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject Player;
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards(this.gameObject.transform.position,Player.transform.position, moveSpeed*Time.deltaTime);
	}
}


