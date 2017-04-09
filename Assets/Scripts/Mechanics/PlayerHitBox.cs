/* Cleaned */
using UnityEngine;

public class PlayerHitBox : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other) {
		if(other.tag == "Enemy" && !Player.opponents.Contains(other.gameObject))
			Player.opponents.Add(other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Enemy" && Player.opponents.Contains(other.gameObject))
			Player.opponents.Remove(other.gameObject); 
	}

	void OnDisable() {
		Player.opponents.Clear(); 
	}

}
