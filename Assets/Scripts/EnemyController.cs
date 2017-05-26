using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float speed = 0.6f;
	Vector3 oldPosition;
	Vector3 currentPosition;

	void Start () {
		oldPosition = transform.position;
		currentPosition = oldPosition;
	}
	
	void Update () {
		float step = speed * Time.deltaTime;
		GameObject cantonment = GameObject.Find("Cantonment");
		transform.position = Vector3.MoveTowards(transform.position, cantonment.transform.position, step);

		currentPosition = transform.position;

		if(currentPosition != oldPosition){
			NetworkManager.instance.GetComponent<NetworkManager>().CommandEnemyMove(transform.position);
			oldPosition = currentPosition;
		}
	}
}
