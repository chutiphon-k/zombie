using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpawnEnemyController : MonoBehaviour {

	public GameObject enemy;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnEnemy (3f));
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SpawnEnemy(float waitTime) {
		while (true) {
			// Random spawn enemy at random position
			Vector2 position = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
			Instantiate (enemy, position, Quaternion.identity);
			
			// Spawn delay
			yield return new WaitForSeconds(waitTime);
		}
	}
	
}
