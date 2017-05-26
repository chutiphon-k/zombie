using UnityEngine;

namespace ObsoleteV2 { 

  public class PlayerMovement : IMovable {

    Rigidbody2D _rigidbody2D;
    CharacterStats _stats;

    public PlayerMovement(GameObject gameObject) {
      _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
      _stats = gameObject.GetComponent<CharacterStats>();
    }

    public void Move() {
      float vX = Input.GetAxisRaw("Horizontal");
      float vY = Input.GetAxisRaw("Vertical");
      _rigidbody2D.velocity = new Vector2(vX, vY) * _stats.MVSPD;
    } 

  }

}