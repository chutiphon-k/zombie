using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// public GameObject bulletPrefab;
	// public Transform bulletSpawn;
	public bool isLocalPlayer = false;

	Vector3 oldPosition;
	Vector3 currentPosition;

	// Use this for initialization
	void Start () {
		oldPosition = transform.position;
		currentPosition = oldPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer) return;

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
		transform.Translate(x, 0, 0);
		currentPosition = transform.position;

		if(currentPosition != oldPosition){
			NetworkManager.instance.GetComponent<NetworkManager>().CommandEnemyMove(transform.position);
			oldPosition = currentPosition;
		}

		// if(Input.GetKeyDown(KeyCode.Space)){
			// NetworkManager n = NetworkManager.instance.GetComponent<NetworkManager>();
			// n.CommandShoot();
		// }
	}
}
