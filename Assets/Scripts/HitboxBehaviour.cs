// using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HitboxBehaviour : MonoBehaviour {

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
			enemies.RemoveAt(0);
			Destroy(toKill);
		}
	}

}
