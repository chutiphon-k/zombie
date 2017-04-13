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

      if(opponents[0] != null) {
        GameObject target = opponents[0];
        Vector2 unitDirection = (target.transform.position - transform.position).normalized;
        target.SendMessage("Knockback", unitDirection * 150);
        target.SendMessage("TakeDamage", stats.ATK);
        opponents.Remove(target);
      }
      else opponents.RemoveAt(0);

    }
  }

  protected override void TakeDamage(int receivedDamage) {
    updateHP(receivedDamage);
    // stats.HP -= receivedDamage; 
    // if(stats.HP <= 0) {
    //   gameObject.SetActive(false);
    // }
    base.TakeDamage(receivedDamage);
  } 

  void DetectAttack(bool isAttacking) {
    if(isAttacking) {

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

      _test.SpriteState(Color.cyan);
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