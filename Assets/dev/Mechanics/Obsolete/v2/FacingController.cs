using UnityEngine;

namespace ObsoleteV2 {

  public class FacingController {
    
    GameObject gameObject; // **
    Vector2 facingDirection;

    public FacingController(GameObject gameObject, Vector2 initValue) {
      this.gameObject = gameObject; // **
      facingDirection = initValue;
    }

    // TODO: ใส่ทิศทางการหันหน้า
    // TODO: ใส่ RayCast จากการหันหน้า
    public void Refresh() {
      
    }

  }
  
}