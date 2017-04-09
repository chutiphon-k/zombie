/* Cleaned */
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Character {

  public int score = 0; 
	public UIBarScript HPBar;

  private GameObject uiScore;

  private bool isWaiting = false;
  private float startAttackTime = 0.0f;

/* Unity's API ************************************************************* */

  protected override void Update () {
    base.Update(); 
    DetectAttack(Input.GetKey(KeyCode.Space));
  }

  void Start() {
    HPBar.UpdateValue(stats.HP, stats.MAXHP);
    uiScore = GameObject.Find("Score");
    uiScore.GetComponent<Text>().text = "000";
  }

  void OnDisable() { 
    SceneManager.LoadScene ("GameOver");
    PlayerPrefs.SetString ("score", scoreIntToString()); 
  }

/* ************************************************************************* */

/* Private Functions ******************************************************* */

  protected override Vector2 GetMovement() {
    return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
  }

  protected override void Hit() {
    while(opponents.Count > 0) {
      GameObject target = opponents[0];

			Vector3 vectorPlayer = transform.position;
			Vector3 vectorEnemy = opponents[0].transform.position;
			Vector3 vectorKnockback = new Vector3();

			if(vectorPlayer.x > vectorEnemy.x) {
				vectorKnockback.x = vectorEnemy.x - 1;
			}
      else {
				vectorKnockback.x = vectorEnemy.x + 1;
			}

			if(vectorPlayer.y > vectorEnemy.y) {
				vectorKnockback.y = vectorEnemy.y - 1;
			}
      else {
				vectorKnockback.y = vectorEnemy.y + 1;
			}

			vectorKnockback.z = 0;

			opponents[0].transform.position = vectorKnockback;

      target.SendMessage("TakeDamage", stats.ATK);
      opponents.Remove(target);
    }
  }

  void DetectAttack(bool isAttacking) {
    if(isAttacking) {

      debugger.SpriteState(Color.green);

      if(!isWaiting) {
        isWaiting = true;
        startAttackTime = Time.timeSinceLevelLoad;
      }
      else if(Time.timeSinceLevelLoad >= startAttackTime + (1.0f / stats.ATKSPD)) {
        isWaiting = false;

        debugger.SpriteState(Color.red);

        Hit();
      }
    }
    else {
      startAttackTime = Time.timeSinceLevelLoad;

      debugger.SpriteState(Color.cyan);
    }
  }

  string scoreIntToString() {
    string strScore = score.ToString();
    string tmp = "";

    for(int i = strScore.Length; i < 3; i++) {
      tmp += "0";
    }
    return tmp + score;  
  }

/* ************************************************************************* */

/* Public Functions ******************************************************** */

  public void updateScore(int value) {
    score += value;
    uiScore.GetComponent<Text>().text = scoreIntToString();
  }

  public void updateHP(int value) {
    if(stats.HP - value > 0) {
      stats.HP -= value;
    }
    else {
      stats.HP = 0;
    }
    HPBar.UpdateValue(stats.HP, stats.MAXHP);
  }

/* ************************************************************************* */

}