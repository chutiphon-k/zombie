using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movementSpeed = 1.0f;

	private float[] movement = new float[2];

	private Rigidbody2D rb2d;

	// For testing purpose
	private SpriteRenderer sp;
	
	
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();

		// For testing purpose
 		sp = GetComponent<SpriteRenderer>();
		sp.color = Color.green;
	}

	// Update is called once per frame
	void Update () {
		movement[0] = Input.GetAxis("Horizontal");
		movement[1] = Input.GetAxis("Vertical");

		Debug.Log("Movement : " + movement[0].ToString() + ' ' + movement[1].ToString());

		rb2d.velocity = new Vector2(movement[0], movement[1]) * movementSpeed;	
	}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionStay2D(Collision2D other) {
			// Debug.Log(other.gameObject.name);
			if(Input.GetKeyDown(KeyCode.Space)) {
				Debug.Log("Attack!");
				sp.color = Color.red;
			} else sp.color = Color.green;
	}

}
