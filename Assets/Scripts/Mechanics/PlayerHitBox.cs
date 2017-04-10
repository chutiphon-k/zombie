/* Cleaned */
using UnityEngine;

public class PlayerHitBox : MonoBehaviour {

	// void OnTriggerStay2D(Collider2D other) {
	// 	if(other.tag == "Enemy" && !Player.opponents.Contains(other.gameObject))
	// 		Player.opponents.Add(other.gameObject);
	// }

	// void OnTriggerExit2D(Collider2D other) {
	// 	if(other.tag == "Enemy" && Player.opponents.Contains(other.gameObject))
	// 		Player.opponents.Remove(other.gameObject); 
	// }

	// void OnDisable() {
	// 	Player.opponents.Clear(); 
	// }

	public string targetTag;
	public GameObject owner;

	void OnTriggerStay2D(Collider2D other) {
		if(other.tag == targetTag && !owner.GetComponent<Player>().GetOpponents().Contains(other.gameObject))
			owner.GetComponent<Player>().GetOpponents().Add(other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == targetTag && owner.GetComponent<Player>().GetOpponents().Contains(other.gameObject))
			owner.GetComponent<Player>().GetOpponents().Remove(other.gameObject); 
	}

	void OnDisable() {
		owner.GetComponent<Player>().GetOpponents().Clear(); 
	}

}
