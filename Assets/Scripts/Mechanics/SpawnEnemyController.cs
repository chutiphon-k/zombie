/* Dirty */
using System.Collections;

using UnityEngine;

public class SpawnEnemyController : MonoBehaviour {

	public int enemyCount;
	public float frequency;
	public GameObject enemy;

	void Start () {
		StartCoroutine (SpawnEnemy(frequency));
	}

	void Update () {
		
	}

	IEnumerator SpawnEnemy(float freq) {
		// Start spawn
		yield return new WaitForSeconds(1.0f/freq);
		while (enemyCount > 0) {
			// Random spawn enemy at random position
			Vector2 position = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
			Instantiate (enemy, position, Quaternion.identity).transform.parent = transform;
			
			// Spawn delay
			yield return new WaitForSeconds(1.0f/freq);
			enemyCount--;
		}
	}
	
}
