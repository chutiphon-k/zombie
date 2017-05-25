using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// public GameObject bulletPrefab;
	// public Transform bulletSpawn;
	public bool isLocalPlayer = false;

	Vector3 oldPosition;
	Vector3 currentPosition;
	Quaternion oldRotation;
	Quaternion currentRotation;

	// Use this for initialization
	void Start () {
		oldPosition = transform.position;
		currentPosition = oldPosition;
		oldRotation = transform.rotation;
		currentRotation = oldRotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer) return;

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
		transform.Translate(x, 0, 0);
		currentPosition = transform.position;
	}
}
