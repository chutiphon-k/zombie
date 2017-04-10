/* Cleaned */
using UnityEngine;

public class Enemy : Character {

  public float deadAnimationTime;

  private GameObject player;

  private bool isWaiting = false;
  private float startAttackTime = 0.0f;
	bool facingRight;

/* Unity's API ************************************************************* */

  protected override void Update() {
    base.Update();
    SeekForAttack();

		if (player.transform.position.x - transform.position.x < 0 && !facingRight) {
			Flip ();
		} 
		else if (player.transform.position.x - transform.position.x > 0 && facingRight) {
			Flip ();
		}
	}
	void Flip (){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

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

      opponents.RemoveAt(0);
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


