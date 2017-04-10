using UnityEngine;

public class JustForDebugging {

  public SpriteRenderer sprite;

  public JustForDebugging(GameObject gameObject) {
    sprite = gameObject.transform.Find("Sprite").GetComponentInChildren<SpriteRenderer>();
  }

  public void SpriteState(Color stateColor) {
    sprite.color = stateColor;
  }

  public void CheckForMovement(Vector2 movement) {
    Debug.Log("movement : " + movement.x.ToString() + ' ' + movement.y.ToString());
  }

  public void CheckForFacingDirection(Vector2 direction) {
    Debug.Log("facingDirection : " + direction);
  }

}