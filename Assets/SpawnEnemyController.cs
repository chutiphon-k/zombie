using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyController : MonoBehaviour {

	public GameObject myEnemy;
	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnEnemy (3f));
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SpawnEnemy(float waitTime) {
		Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0f);
		Instantiate (myEnemy, position, Quaternion.identity);

		yield return new WaitForSeconds(waitTime);
		StartCoroutine (SpawnEnemy (3f));
	}
}
