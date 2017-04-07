using UnityEngine;

public class EnemyController : MonoBehaviour {

  // How fast enemy moves to the player
  public float moveSpeed;

  private GameObject player;

  void Awake () {
    player = GameObject.Find("Player");
  }

  void Update () {
    // Continuously move towards player in position per second
    transform.position = Vector2.MoveTowards(
      transform.position,
      player.transform.position,
      moveSpeed * Time.deltaTime
    );
  }

}


