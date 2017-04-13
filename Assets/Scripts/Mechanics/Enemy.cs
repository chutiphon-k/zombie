using UnityEngine;

public class Enemy : Character {

  public float deadAnimationTime;

  private GameObject player;

/* Unity's API ************************************************************* */

  protected override void Update() {
    base.Update();
  }

  protected override void Awake () {
    base.Awake();
    player = GameObject.Find("Player");
  }

  void OnDisable() {
    if(player != null)
      player.GetComponent<Player>().UI_UpdateScore(1);
    Destroy(gameObject); 
  }

/* ************************************************************************* */

/* Private Functions ******************************************************* */

  protected override Vector2 GetMovement() {
    // Calculate unit direction to player
    Vector2 unitDirection = (player.transform.position - transform.position).normalized;
    return unitDirection;
  }

  protected override bool GetAttack() {
    return opponents.Contains(player);
  }

  protected override void Hit(GameObject target) { 
    target.SendMessage("TakeDamage", stats.ATK);
  }

/* ************************************************************************* */

}


