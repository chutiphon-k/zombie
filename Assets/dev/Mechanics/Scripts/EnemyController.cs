using UnityEngine;

public class EnemyController : MonoBehaviour {

  public int HP;
  public float movementSpeed;
  public float attackSpeed;
  public string targetTag;
  public LayerMask characterLayer;
  public Transform hitboxTransform;

  float currentFlip = 1;
  float actualSpeed;
  float deltaX = 0.0f;
  float attackTime = 0.0f;
  bool alive = true;
  int characterLayerHash;

  // Drivers
    Collider2D attack;
    GameObject target;

  // Dependencies
    Animator animator;
    Rigidbody2D rb2d;

  void Start() {
    rb2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();

    target = GameObject.FindWithTag(targetTag);
    characterLayerHash = characterLayer.GetHashCode();
  }

  void Update() {
    AnimatorSetVariables();
    if(alive) CheckAlive();
  }

	void FixedUpdate() {
		HorizontalMove();
    TryAttack();
	}

  // Functions
		// TODO: หาวิธีการ move และการ attack เพื่อที่จะเอาไปใส่ใน animation ได้
    void AnimatorSetVariables() {
      animator.SetBool("isMoving", Mathf.Abs(rb2d.velocity.x) > 0);
      animator.SetBool("isAttacking", attack != null);
    }

    void HorizontalMove() {
      // *** SeekForTarget *** //
        // (I) Seek for targets within 'R' radius around position
        // (II) When detected many , choose the closer one 
        // (III) Move to target



      deltaX = target == null ? 0 : target.transform.position.x - transform.position.x;
      float direction = deltaX < 0.0f ? -1.0f : 1.0f;

      // Flip Character
      float oldXScale = rb2d.transform.localScale.x;
      float oldYScale = rb2d.transform.localScale.y;
      transform.localScale = new Vector2(
        direction == currentFlip ? oldXScale : -oldXScale
        , oldYScale
      );
      currentFlip = Mathf.Sign(transform.localScale.x);

      // Move by velocity
      rb2d.velocity = new Vector2(deltaX * actualSpeed, rb2d.velocity.y);
    }

    void TryAttack() {
      actualSpeed = Mathf.Abs(deltaX) < 1.0f ? 0.0f : movementSpeed;

      attack = Physics2D.OverlapBox(
        hitboxTransform.position,
        new Vector2(1.0f, 1.0f),
        0.0f,
        characterLayerHash
      );
      if(attack != null) {
        if(Time.fixedTime > attackTime) {
          // TODO: Implement real hit
          Debug.Log("Hit");
          Collider2D hit = attack;
          hit.SendMessage("StatusUpdate");

          attackTime = Time.fixedTime + (1.0f / attackSpeed);
        }
      }
    }

    public void StatusUpdate() {
      HP -= 1;
      // TODO: set hurt animation trigger
    }

    void CheckAlive() {
      // Please see PlayerController's CheckAlive() for more info
      if(HP <= 0) {
        alive = false;
        actualSpeed = 0.0f;
        animator.SetTrigger("Dead");
        Destroy(gameObject, 0.667f);
      }
      else
        alive = true;
    }

}