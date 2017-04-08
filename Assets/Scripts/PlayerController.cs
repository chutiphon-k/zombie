// using System.Collections;
// using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour {

  public class UnitTest {

    public static Color idle = Color.cyan;
    public static Color waitAttackDelay = Color.green;
    public static Color attack = Color.red;
    static SpriteRenderer sprite = GameObject.Find("Sprite").GetComponentInChildren<SpriteRenderer>();

    public static void SpriteState(Color stateColor) {
      sprite.color = stateColor;
    }

    public static void CheckForMovement(Vector2 movement) {
      Debug.Log("movement : " + movement.x.ToString() + ' ' + movement.y.ToString());
    }

    public static void CheckForFacingDirection(Vector2 direction) {
      Debug.Log("facingDirection : " + direction);
    }

  }

  public class PlayerFacing {

    private Vector2 facingDirection;

    public PlayerFacing(Vector2 direction) {
      facingDirection = direction;
    }

    public Vector2 Get() {
      return facingDirection;
    }

    public void Set(Vector2 vec) {

      Vector2 newDirection = Vector2.zero;

      if(vec.Equals(Vector2.zero)) return;

      if(vec.x > 0.0f) newDirection.x = Mathf.Ceil(vec.x);
      else if(vec.x < 0.0f) newDirection.x = Mathf.Floor(vec.x);

      if(vec.y > 0.0f) newDirection.y = Mathf.Ceil(vec.y);
      else if(vec.y < 0.0f) newDirection.y = Mathf.Floor(vec.y);

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

  public float attackAnimationTime;
  public float movementSpeed;
  public Vector2 playerColliderSize;
  public BoxCollider2D[] hitboxColliders; // ** WARNING : Please lay in CW order ** //
  public PlayerFacing playerFacing = new PlayerFacing(Vector2.right);

  private bool isLocking = false;
  private float startAttackTime = 0.0f;
  private Vector2 movement;
  private Rigidbody2D rb2d;

  void Awake () {
    rb2d = GetComponent<Rigidbody2D>();
    GetComponent<BoxCollider2D>().size = playerColliderSize;
    for(int i = 0; i < hitboxColliders.Length; i++) {
      hitboxColliders[i].gameObject.SetActive(false);
    }
  }

  void Update () {
    movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    // Debug.Log(this.gameObject.transform.position);s
    SetHitbox(movement);
    DetectAttack(Input.GetKey(KeyCode.Space));

    // UnitTest.CheckForFacingDirection(playerFacing.Get());
  }

  void FixedUpdate() {
    rb2d.velocity = movement * movementSpeed;
  }

  /* ************************************************************************************** */
    void SetHitbox(Vector2 raw) {
      int target;
      playerFacing.Set(raw);
      target = playerFacing.GetCWFormat();
      hitboxColliders[target].gameObject.SetActive(true);
      for(int i = 0; i < hitboxColliders.Length; i++) {
        if(i != target) hitboxColliders[i].gameObject.SetActive(false);
      }
    }

    void DetectAttack(bool isAttacking) {
      if(isAttacking) {

        UnitTest.SpriteState(UnitTest.waitAttackDelay);

        if(!isLocking) {
          isLocking = true;
          startAttackTime = Time.timeSinceLevelLoad;
        }
        else if(Time.timeSinceLevelLoad >= (startAttackTime + attackAnimationTime)) {
          isLocking = false;

          UnitTest.SpriteState(UnitTest.attack);

          // Debug.Log("Attack!");
          hitboxColliders[playerFacing.GetCWFormat()].SendMessage("Hit");
        }
      }
      else {
        startAttackTime = Time.timeSinceLevelLoad;

        UnitTest.SpriteState(UnitTest.idle);
      }
    }
  /* ************************************************************************************** */

}