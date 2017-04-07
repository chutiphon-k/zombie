// using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HitboxBehaviour : MonoBehaviour {

	private List<GameObject> enemies = new List<GameObject>();

	void OnTriggerStay2D(Collider2D other) {
		Debug.Log(other.name + "has entered hitzone");
		if(other.tag == "Enemy" && !enemies.Contains(other.gameObject)) {
			enemies.Add(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		Debug.Log(other.name + "has exited hitzone");
		if(other.tag == "Enemy" && enemies.Contains(other.gameObject)) {
			enemies.Remove(other.gameObject);
		}
	}

	void Hit() {
		while(enemies.Count > 0) {
			GameObject toKill = enemies[0];
			enemies.RemoveAt(0);
			Destroy(toKill);
		}
	}

}
