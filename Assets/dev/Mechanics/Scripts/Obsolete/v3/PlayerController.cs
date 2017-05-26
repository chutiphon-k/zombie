namespace ObsoleteV3 {

  using UnityEngine;

  public class PlayerController : MonoBehaviour {

    public float moveSpeed, rayRange, attackSpeed;

    Rigidbody2D rb2d;
    float horizontal, vertical, nextAttack;
    Vector2 facingDirection;

    void Start() {
      rb2d = GetComponent<Rigidbody2D>(); 
      facingDirection = Vector2.right; // Initiate facing direction to Right direction
      nextAttack = 0.0f; // First attack can proceed at 0 sec
    }

    void FixedUpdate() {
      horizontal = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ?
        (Input.GetKey(KeyCode.LeftArrow) ? -1.0f : 1.0f) : 0.0f;
      vertical = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) ?
        (Input.GetKey(KeyCode.DownArrow) ? -1.0f : 1.0f) : 0.0f;

      // Change facing direction only when character moves
      // FIXME: ไม่จำเป็นต้องปล่อยปุ่มพร้อมกันในแนวเฉียง เพื่อหันหน้าเฉียง (อาจใช้วิธีหน่วงเวลารับ)
      if(!new Vector2(horizontal, vertical).Equals(Vector2.zero)) {
        facingDirection = new Vector2(horizontal, vertical);
      }

      // Move by velocity
      rb2d.velocity = new Vector2(horizontal, vertical) * moveSpeed; 

      // Cast a ray from given facing direction
      // only when attack button is hitting
      RaycastHit2D hit;
      if(Input.GetKey(KeyCode.Space)) {
        if(Time.time > nextAttack) {
          nextAttack = Time.time + (1.0f / attackSpeed); 
          hit = Physics2D.Raycast(transform.position, facingDirection, facingDirection.magnitude * rayRange);
          Debug.DrawRay(transform.position, facingDirection.normalized * rayRange, Color.green);
          Debug.Log("Attack");
        }
        else Debug.DrawRay(transform.position, facingDirection.normalized * rayRange, Color.red);
      }

      // Debug.Log("movement: " + horizontal.ToString() + ' ' + vertical.ToString());
      // Debug.Log("facingdir: " + facingDirection.ToString());
      if(Input.GetKey(KeyCode.Z)) Debug.Break();
    }

  }

}