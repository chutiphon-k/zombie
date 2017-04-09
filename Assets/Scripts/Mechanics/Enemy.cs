/* Cleaned */
using UnityEngine;

public class Enemy : Character {

  public float deadAnimationTime;

  private GameObject player;

/* Unity's API ************************************************************* */

  protected override void Awake () {
    base.Awake();
    player = GameObject.Find("Player");
  }

  void OnDisable() {
    player.GetComponent<Player>().updateScore(1);
    Destroy(gameObject); 
  }

/* ************************************************************************* */

/* Private Functions ******************************************************* */

  protected override Vector2 GetMovement() {
    // Calculate unit direction to player
    Vector2 unitDirection = (player.transform.position - transform.position).normalized;
    return unitDirection;
  }

  protected override void Hit() { 
    while(opponents.Count > 0) {
      GameObject target = opponents[0];
      target.SendMessage("TakeDamage", stats.ATK);
      opponents.Remove(target);
    }
  }

/* ************************************************************************* */

}


