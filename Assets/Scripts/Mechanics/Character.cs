/* Cleaned */
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

  public class UnitDebugger {

    public SpriteRenderer sprite;

    public UnitDebugger(GameObject gameObject) {
      sprite = gameObject.transform.Find("Sprite").GetComponentInChildren<SpriteRenderer>();
    }

    public void SpriteState(Color stateColor) {
      sprite.color = stateColor;
    } 

    public void CheckForMovement(Vector2 movement) {
      Debug.Log("movement : " + movement.x.ToString() + ' ' + movement.y.ToString());
    }

    public void CheckForFacingDirection(Vector2 direction) {
      Debug.Log("facingDirection : " + direction);
    }

  }

  private class Facing {

    private Vector2 facingDirection;

    public Facing(Vector2 direction) {
      facingDirection = direction;
    }

    public Vector2 Get() {
      return facingDirection;
    }

    public void Set(Vector2 vec) {

      Vector2 newDirection = Vector2.zero;

      if(vec.x > 0.1f) newDirection.x = 1.0f;
      else if(vec.x < -0.1f) newDirection.x = -1.0f;
      else newDirection.x = 0.0f;

      if(vec.y > 0.1f) newDirection.y = 1.0f;
      else if(vec.y < -0.1f) newDirection.y = -1.0f;
      else newDirection.y = 0.0f;

      if(newDirection.Equals(Vector2.zero)) return;

      facingDirection = newDirection;

    }

    public int GetCWFormat() {
      if(facingDirection == Vector2.up) return 0;
      else if(facingDirection == Vector2.up + Vector2.right) return 1;
      else if(facingDirection == Vector2.right) return 2;
      else if(facingDirection == Vector2.down + Vector2.right) return 3;
      else if(facingDirection == Vector2.down) return 4;
      else if(facingDirection == Vector2.down + Vector2.left) return 5;
      else if(facingDirection == Vector2.left) return 6;
      else if(facingDirection == Vector2.up + Vector2.left) return 7;
      return -1;
    }

  }

  public UnitDebugger debugger;

  public static List<GameObject> opponents = new List<GameObject>();
  public CharacterStats stats = new CharacterStats(); 

  protected Rigidbody2D rb2d;
  protected Vector2 movement;

  private HitBoxController hitBox;
  private Facing facing;

/* Unity's API ************************************************************* */

  protected virtual void Awake() {
    debugger = new UnitDebugger(gameObject);

    rb2d = GetComponent<Rigidbody2D>();
    facing = new Facing(Vector2.right);
    hitBox = transform.Find("HitBoxes").GetComponent<HitBoxController>();
  }

  protected virtual void Update() {
    movement = GetMovement();
    SetHitbox(movement);
  }

  protected virtual void FixedUpdate() {
    rb2d.velocity = movement * stats.MVSPD; 
  }

/* ************************************************************************* */

/* Private Functions ******************************************************* */

  protected abstract Vector2 GetMovement();

  protected abstract void Hit(); 

  protected virtual void TakeDamage(int receivedDamage) {
    stats.HP -= receivedDamage; 
    if(stats.HP <= 0) {
      gameObject.SetActive(false);
    }
  } 

  private void SetHitbox(Vector2 raw) {
    facing.Set(raw);
    hitBox.SendMessage("TurnOn", facing.GetCWFormat());
  }

/* ************************************************************************* */

}