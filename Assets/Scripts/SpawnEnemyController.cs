using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpawnEnemyController : MonoBehaviour {

	public GameObject enemy;
	public float waitTime;

	void Start () {
		StartCoroutine (SpawnEnemy(waitTime));
	}

	void Update () {
		
	}

	IEnumerator SpawnEnemy(float waitTime) {
		// Start spawn
		yield return new WaitForSeconds(waitTime);
		while (true) {
			// Random spawn enemy at random position
			Vector2 position = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
			Instantiate (enemy, position, Quaternion.identity);
			
			// Spawn delay
			yield return new WaitForSeconds(waitTime);
		}
	}
	
}
