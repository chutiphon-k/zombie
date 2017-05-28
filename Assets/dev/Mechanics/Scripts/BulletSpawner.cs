using UnityEngine;

public class BulletSpawner : MonoBehaviour {

  public float bulletRange;
  public float fireRates;
  public GameObject bullet;

  float shotTime = 0.0f;

  PlayerController playerController;

  void Start() {
    playerController = GetComponentInParent<PlayerController>();
  }

  void Update() {
    if(playerController.Fire) { 
      // You can shoot when shooting time has come
      if (Time.time > shotTime) {
        Shot();
        shotTime = Time.time + (1.0f / fireRates);
      } 
    }
  }

  // Functions
    void Shot() {
      // Shot spawn
      Rigidbody2D shotRB2D = Instantiate(
        bullet, transform.position, Quaternion.identity
      ).GetComponent<Rigidbody2D>();

      // Add bullet forces with direction
      shotRB2D.AddForce(
        new Vector2(playerController.facingDirection * bulletRange, 0.0f),
        ForceMode2D.Impulse
      );
    }

}