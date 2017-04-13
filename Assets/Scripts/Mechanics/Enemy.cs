/* Cleaned */
using UnityEngine;

public class Enemy : Character {

  public float deadAnimationTime;

  private GameObject player;

  private bool isWaiting = false;
  private float startAttackTime = 0.0f;

/* Unity's API ************************************************************* */

  protected override void Update() {
    base.Update();
    SeekForAttack();
  }

  protected override void Awake () {
    base.Awake();
    player = GameObject.Find("Player");
  }

  void OnDisable() {
    if(player.activeSelf)
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

      if(opponents[0] != null) {
        GameObject target = opponents[0];
        target.SendMessage("TakeDamage", stats.ATK);
      }
      else opponents.RemoveAt(0);

    }
  }

  private void SeekForAttack() {
    if(opponents.Contains(player)) {

      _test.SpriteState(Color.green);

      if(!isWaiting) {
        isWaiting = true;
        startAttackTime = Time.timeSinceLevelLoad;
      }
      else if(Time.timeSinceLevelLoad >= startAttackTime + (1.0f / stats.ATKSPD)) {
        isWaiting = false;

        _test.SpriteState(Color.red);

        Hit();
      }
    }
    else {
      startAttackTime = Time.timeSinceLevelLoad;

      _test.SpriteState(Color.yellow);
    } 
  }

/* ************************************************************************* */

}


