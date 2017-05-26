using UnityEngine;

namespace ObsoleteV2 { 

  public class PlayerController : CharacterController {
    
    void Awake() {
      movementController = new PlayerMovement(gameObject);
      attackController = new PlayerAttack();
      facingController = new FacingController(gameObject, Vector2.right);
    }

    void Update() {

    }

    void FixedUpdate() {
      movementController.Move();
      facingController.Refresh();
    }

  }

} 