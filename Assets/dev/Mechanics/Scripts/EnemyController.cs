using UnityEngine;

public class EnemyController : MonoBehaviour {

  public float movementSpeed;
  public float attackSpeed;
  public string targetTag;
  public LayerMask characterLayer;
  public Transform hitboxTransform;

  float currentFlip = 1;
  float actualSpeed;
  float deltaX = 0.0f;
  float attackTime = 0.0f;
  int characterLayerHash;
	GameObject target;

  // Drivers
    bool attack;

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
  }

	void FixedUpdate() {
		HorizontalMove();
    TryAttack();
	}

  // Functions
		// TODO: หาวิธีการ move และการ attack เพื่อที่จะเอาไปใส่ใน animation ได้
    void AnimatorSetVariables() {
      animator.SetBool("isMoving", Mathf.Abs(rb2d.velocity.x) > 0);
      animator.SetBool("isAttacking", attack);
    }

    void HorizontalMove() {
      deltaX = target.transform.position.x - transform.position.x;
      float direction = deltaX < 0.0f ? -1.0f : 1.0f;

      float oldXScale = rb2d.transform.localScale.x;
      float oldYScale = rb2d.transform.localScale.y;
      transform.localScale = new Vector2(
        direction == currentFlip ? oldXScale : -oldXScale
        , oldYScale
      );
      currentFlip = Mathf.Sign(transform.localScale.x);

      rb2d.velocity = new Vector2(deltaX * actualSpeed, rb2d.velocity.y);
    }

    void TryAttack() {
      actualSpeed = Mathf.Abs(deltaX) < 1.0f ? 0.0f : movementSpeed;
      if(attack = Physics2D.OverlapBox(
        hitboxTransform.position,
        new Vector2(1.0f, 2.0f),
        0.0f,
        characterLayerHash 
      )) {
        if(Time.fixedTime > attackTime) {
          // TODO: Implement real hit
          Debug.Log("Hit"); 
          attackTime = Time.fixedTime + (1.0f / attackSpeed);
        }
      }
    }

}