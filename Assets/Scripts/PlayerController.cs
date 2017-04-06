using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour {

  public float attackAnimationTime;
	public float movementSpeed;

	private Vector2 movement;
	private string facingDirection;

	private Rigidbody2D rb2d;
	public WeaponController weaponController;
	private Coroutine attackTask = null;

	void Awake () {
		/* Variables initialize */
			rb2d = GetComponent<Rigidbody2D>();
			facingDirection = "right";

		_InitColor();
	}

	void Update () {
		movement = GetInput2D();
		SetDirectionFrom(movement);
		GetAttack(KeyCode.Space);
	}

	void FixedUpdate() {
		rb2d.velocity = movement * movementSpeed;
	}

	/* ***** PUBLIC FUNCITONS ***** */
		public string GetDirection() {
			return facingDirection;
		}
	/* **************************** */

	/* ***** PRIVATE FUNCTIONS ***** */
		Vector2 GetInput2D() {
			return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		}

		void SetDirectionFrom(Vector2 vec) {
			Vector2 newDirection = Vector2.zero;
			string directionStr = "";

			if(vec.Equals(Vector2.zero)) return;

			if(vec.x > 0.0f) newDirection.x = Mathf.Ceil(vec.x);
			else if(vec.x < 0.0f) newDirection.x = Mathf.Floor(vec.x);

			if(vec.y > 0.0f) newDirection.y = Mathf.Ceil(vec.y);
			else if(vec.y < 0.0f) newDirection.y = Mathf.Floor(vec.y);

			if(newDirection.y > 0) directionStr = "up";
			else if(newDirection.y < 0) directionStr = "down";

			if(newDirection.x < 0) directionStr = "left";
			else if(newDirection.x > 0) directionStr = "right";
			
			facingDirection = directionStr;
			// _DebugDirection();
		}

		void GetAttack(KeyCode attackButton) {
			if(Input.GetKey(attackButton)) {
				if(!weaponController.GetAttackState()) {
					attackTask = StartCoroutine(weaponController.AttackingManager(attackAnimationTime));
				}
			}
			else {
				weaponController.SetAttackState(false);
				if(attackTask != null) StopCoroutine(attackTask);
				_AttackIdle();
			}
		}
	/* ***************************** */

	/* ***** TESTING / DEBUGGING ***** */
		Color startColor;

		void _InitColor() {
			startColor = weaponController.GetComponentInChildren<SpriteRenderer>().color;
		}

		void _DebugMovement() {
			Debug.Log("Movement : " + movement.x.ToString() + ' ' + movement.y.ToString());
		}

		void _DebugDirection() {
			Debug.Log("facingDirection : " + facingDirection);
		}

		public void _Attacking() {
				weaponController.GetComponentInChildren<SpriteRenderer>().color = Color.red;
		}

		public void _AttackWait() {
				weaponController.GetComponentInChildren<SpriteRenderer>().color = Color.green;
		}
		
		public void _AttackIdle() {
			weaponController.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
		}
	/* ******************************* */

}
