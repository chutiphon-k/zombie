// using System.Collections;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HitboxBehaviour : MonoBehaviour {

	private GameObject player;

	public class UnitTest {

		public static int enemyCount = 0;

		public static void CheckForEnemies(List<GameObject> list) {
			if(list.Count != enemyCount) {
				enemyCount = list.Count;
				Debug.Log("Now hitbox contains : " + enemyCount.ToString() + " enemy(ies)");
			}
		}

	}

	private List<GameObject> enemies = new List<GameObject>();


	void Awake () {
		player = GameObject.Find("Player");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy" && !enemies.Contains(other.gameObject)) {
			enemies.Add(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Enemy" && enemies.Contains(other.gameObject)) {
			enemies.Remove(other.gameObject);
		}
	}

	void FixedUpdate() {
		// UnitTest.CheckForEnemies(enemies);
	}

	void OnDisable() {
		enemies.Clear();
	}

	void Hit() {		
		while(enemies.Count > 0) {
			GameObject toKill = enemies[0];
			Vector3 vectorPlayer = player.transform.position;
			Vector3 vectorEnemy = enemies[0].transform.position;
			Vector3 vectorKnockback = new Vector3();

			if(vectorPlayer.x > vectorEnemy.x){
				vectorKnockback.x = vectorEnemy.x - 1;
			} else {
				vectorKnockback.x = vectorEnemy.x + 1;
			}

			if(vectorPlayer.y > vectorEnemy.y){
				vectorKnockback.y = vectorEnemy.y - 1;
			} else {
				vectorKnockback.y = vectorEnemy.y + 1;
			}

			vectorKnockback.z = 0;

			enemies[0].transform.position = vectorKnockback;
			enemies.RemoveAt(0);
			Destroy(toKill);
		}
	}

}
