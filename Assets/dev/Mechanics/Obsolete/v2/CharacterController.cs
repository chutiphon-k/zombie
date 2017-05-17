using UnityEngine;

namespace ObsoleteV2 {

  public abstract class CharacterController : MonoBehaviour {

    protected IMovable movementController;
    protected IAttackable attackController;
    protected FacingController facingController;

  }

}