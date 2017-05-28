using UnityEngine;

public class Bullet : MonoBehaviour {

  public float TTL;

  void Awake() {
    Destroy(gameObject, TTL);
  }
  
  void OnCollisionEnter2D(Collision2D other) {
    if(other.gameObject.tag == "Enemy") {
      other.gameObject.SendMessage("StatusUpdate");     
      Destroy(gameObject);
    }
  }

}