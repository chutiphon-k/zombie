using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Character {

  public int score = 0; 
	public UIBarScript HPBar;

  private GameObject uiScore;

/* Unity's API ************************************************************* */

  protected override void Update () {
    base.Update(); 
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

  protected override bool GetAttack() {
    return Input.GetKey(KeyCode.Space);
  }

  protected override void Hit(GameObject target) {
    Vector2 unitDirection = (target.transform.position - transform.position).normalized;
    target.GetComponent<Enemy>().Knockback(unitDirection * 150);
    target.GetComponent<Enemy>().TakeDamage(stats.ATK);
  }

  private string scoreIntToString() {
    string strScore = score.ToString();
    string tmp = "";

    for(int i = strScore.Length; i < 3; i++) {
      tmp += "0";
    }
    return tmp + score;  
  }

/* ************************************************************************* */

/* Public Functions ******************************************************** */

  public override void TakeDamage(int receivedDamage) {
    stats.HP -= receivedDamage; 
    if(stats.HP <= 0) {
      stats.HP = 0;
      UI_UpdateHP(receivedDamage);
      gameObject.SetActive(false);
    }
    else UI_UpdateHP(receivedDamage);
  } 

  public void UI_UpdateScore(int value) {
    if(gameObject == null) return;
    score += value;
    uiScore.GetComponent<Text>().text = scoreIntToString();
  }

  public void UI_UpdateHP(int value) {
    if(gameObject == null) return;
    HPBar.UpdateValue(stats.HP, stats.MAXHP);
  }

/* ************************************************************************* */

}