using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public Transform target;
	public float speed = 0.5f;
	Vector3 oldPosition;
	Vector3 currentPosition;

	// Use this for initialization
	void Start () {
		oldPosition = transform.position;
		currentPosition = oldPosition;
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);

		currentPosition = transform.position;

		if(currentPosition != oldPosition){
			NetworkManager.instance.GetComponent<NetworkManager>().CommandEnemyMove(transform.position);
			oldPosition = currentPosition;
		}
	}
}
