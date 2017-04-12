using UnityEngine;

public class EnemyHitBox : MonoBehaviour {

	public string targetTag;
	public GameObject owner;

	void OnTriggerStay2D(Collider2D other) {
		if(other.tag == targetTag && !owner.GetComponent<Enemy>().GetOpponents().Contains(other.gameObject))
			owner.GetComponent<Enemy>().GetOpponents().Add(other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == targetTag && owner.GetComponent<Enemy>().GetOpponents().Contains(other.gameObject))
			owner.GetComponent<Enemy>().GetOpponents().Remove(other.gameObject); 
	}

	void OnDisable() {
		owner.GetComponent<Enemy>().GetOpponents().Clear(); 
	}

}
