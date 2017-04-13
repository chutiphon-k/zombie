using UnityEngine;

public class Facing {

  private Vector2 facingDirection;

  public Facing(Vector2 direction) {
    facingDirection = direction;
  }

  public Vector2 Get() {
    return facingDirection;
  }

  public void Set(Vector2 vec) {

    Vector2 newDirection = Vector2.zero;

    if (vec.x > 0.1f) newDirection.x = 1.0f;
    else if (vec.x < -0.1f) newDirection.x = -1.0f;
    else newDirection.x = 0.0f;

    if (vec.y > 0.1f) newDirection.y = 1.0f;
    else if (vec.y < -0.1f) newDirection.y = -1.0f;
    else newDirection.y = 0.0f;

    if (newDirection.Equals(Vector2.zero)) return;

    facingDirection = newDirection;

  }

  public int GetCWFormat() {

    if (facingDirection == Vector2.up) return 0;
    else if (facingDirection == Vector2.up + Vector2.right) return 1;
    else if (facingDirection == Vector2.right) return 2;
    else if (facingDirection == Vector2.down + Vector2.right) return 3;
    else if (facingDirection == Vector2.down) return 4;
    else if (facingDirection == Vector2.down + Vector2.left) return 5;
    else if (facingDirection == Vector2.left) return 6;
    else if (facingDirection == Vector2.up + Vector2.left) return 7;
    return -1;

  }

}