using UnityEngine;

public class PlayerController : MonoBehaviour {

  public int HP;
  public float movementSpeed;
  public float jumpForces;
  public LayerMask groundLayer;
  public Transform groundCheckerTransform;

  float currentFlip = 1;
  float cameraZOffset;
  bool grounded = true;
  bool alive = true;
  int groundLayerHash;

  // Communicators
    public bool Fire { get {return fire;} } 
    public float facingDirection { get {return currentFlip;} }

  // Inputs
    float horizontal;
    bool run;
    bool fire;
    bool jump;

  // Dependencies
    Animator animator;
    Rigidbody2D rb2d;
    GameObject mycamera;

  void Start() {
    animator = GetComponent<Animator>();
    rb2d = GetComponent<Rigidbody2D>();
    mycamera = GameObject.FindWithTag("MainCamera");

    groundLayerHash = groundLayer.GetHashCode();
    cameraZOffset = mycamera.transform.position.z - transform.position.z;
  }

  void Update() {
    ManageInputs();
    AnimatorSetVariables();
    if(alive) CheckAlive();
  }

  void FixedUpdate() {
    HorizontalMove();
    VerticalMove();
  }

  void LateUpdate() {
    Staring();  
  }

  // Functions
    // TODO: กลับมาเปลี่ยนค่า argument ที่ 2 ให้ตรงกับ mechanic จริงๆ
    void AnimatorSetVariables() {
      animator.SetBool("isMoving", horizontal != 0.0f);
      animator.SetBool("isRunning", run);
      animator.SetBool("isFiring", fire); 
      animator.SetBool("isGrounded", grounded);
    }

    void ManageInputs() {
      horizontal = Input.GetAxisRaw("Horizontal");
      run = Input.GetKey(KeyCode.LeftShift);
      fire = run ? false : Input.GetKey(KeyCode.Z);
      jump = Input.GetKey(KeyCode.UpArrow);
    }

    void HorizontalMove() {
      // Flip character
      transform.localScale = new Vector2(
        horizontal == 0.0f ? currentFlip : (
          horizontal == -1.0f ? -1.0f : 1.0f
        ), 1
      );
      currentFlip = Mathf.Sign(transform.localScale.x);

      // Change movement velocity
      rb2d.velocity = new Vector2(
        horizontal * (run ? 1.5f : 1.0f) * movementSpeed , rb2d.velocity.y
      );
    }

    void VerticalMove() {
      // Check whether player is on ground
      grounded = Physics2D.OverlapBox(
        groundCheckerTransform.position,
        new Vector2(1.0f, 0.1f),
        0.0f,
        groundLayerHash
      );

      // Add jumping forces
      rb2d.AddForce(
         grounded && jump ? new Vector2(0.0f, jumpForces) : Vector2.zero
         , ForceMode2D.Impulse
      );
    }
    
    public void StatusUpdate() {
      HP -= 1;
      // TODO: set hurt animation trigger
    }

    void CheckAlive() { 
      // Determines whether alive state is true or false to run this function
      if(HP <= 0) {
        // Because when player died, dead animation must be triggered only once
        alive = false;
        movementSpeed = 0.0f;
        animator.SetTrigger("Dead");
        Destroy(gameObject, 2.0f);
      }
      else
        alive = true;
    }

    void Staring() {
      mycamera.transform.position = transform.position + new Vector3( 0.0f, 0.0f, cameraZOffset );
    }

}