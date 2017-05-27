using UnityEngine;

public class Bullet : MonoBehaviour {

  public float TTL;

  void Awake() {
    Destroy(gameObject, TTL);
  }

  void OnTriggerEnter2D(Collider2D other) {
      
  }

}