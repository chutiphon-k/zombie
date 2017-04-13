using System.Collections.Generic;

using UnityEngine;

public abstract class Character : MonoBehaviour {

  protected JustForDebugging _test; 

  [SerializeField] protected CharacterStats stats = new CharacterStats(); 
  protected List<GameObject> opponents = new List<GameObject>();

  private Rigidbody2D rb2d;
  private Vector2 movement; 
  private HitBoxController hitBox;
  private Facing facing;

  private bool wasPushed = false;
  private Vector2 knockbackForce = Vector2.zero; 

  private bool attack = false;
  private bool isWaiting = false;
  private float startAttackTime = 0.0f; 

/* Unity's API ************************************************************* */

  protected virtual void Awake() {
    _test = new JustForDebugging(gameObject);

    rb2d = GetComponent<Rigidbody2D>(); 
    hitBox = transform.Find("HitBoxes").GetComponent<HitBoxController>();
    facing = new Facing(Vector2.right);
  }

  protected virtual void Update() {
    movement = GetMovement();
    attack = GetAttack();

    SetHitbox(movement);
    OnAttack();
  }

  protected virtual void FixedUpdate() {
    rb2d.velocity = movement * stats.MVSPD; 
    OnKnockback();
  }

/* ************************************************************************* */

/* Private Functions ******************************************************* */

  protected abstract Vector2 GetMovement();

  protected abstract void Hit(GameObject target);

  protected abstract bool GetAttack(); 

  private void OnKnockback() {
    if(!wasPushed) return;
    rb2d.AddForce(knockbackForce, ForceMode2D.Impulse);
    wasPushed = false;
  }

  private void OnAttack() { 
    if(attack) {

      _test.SpriteState(Color.green);

      if(!isWaiting) {
        isWaiting = true;
        startAttackTime = Time.timeSinceLevelLoad;
      }
      else if(Time.timeSinceLevelLoad >= startAttackTime + (1.0f / stats.ATKSPD)) {
        isWaiting = false;

        _test.SpriteState(Color.red);

        // Attack all opponents
        while(opponents.Count > 0) { 
          if(opponents[0] != null) {
            GameObject target = opponents[0];

            Hit(target);

            opponents.Remove(target);
          }
          else opponents.RemoveAt(0); 
        } 

      } 

    }
    else {
      startAttackTime = Time.timeSinceLevelLoad;

      _test.SpriteState(Color.cyan);
    }

  }

  private void SetHitbox(Vector2 raw) {
    facing.Set(raw);
    hitBox.TurnOn(facing.GetCWFormat());
  }

/* ************************************************************************* */

/* Public Functions ******************************************************** */

  public virtual void TakeDamage(int receivedDamage) {
    stats.HP -= receivedDamage; 
    if(stats.HP <= 0) {
      gameObject.SetActive(false);
    }
  } 

  public void Knockback(Vector2 force) {
    knockbackForce = force;
    wasPushed = true;
  }

  public List<GameObject> GetOpponents() { 
    return opponents;
  } 

/* ************************************************************************* */

}