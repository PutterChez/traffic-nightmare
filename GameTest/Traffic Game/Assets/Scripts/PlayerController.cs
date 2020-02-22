using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 5f;
	private float movement_horizontal = 0f, movement_vertical = 0f;
	private Rigidbody2D rigidBody;

	private Vector3 pos;
	private bool canMove = true;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();

		pos = rigidBody.position;
	}

	// Update is called once per frame
	void Update () {

		if (canMove) {

			movement_horizontal = Input.GetAxis ("Horizontal");

			if (movement_horizontal > 0) {
				pos += new Vector3 (0.5f, 0f);
				canMove = false;
			} else if (movement_horizontal < 0) {
				pos += new Vector3 (-0.5f, 0f);
				canMove = false;
			}

			movement_vertical = Input.GetAxis ("Vertical");

			if (movement_vertical > 0) {
				pos += new Vector3 (0f, 0.5f);
				canMove = false;
			} else if (movement_vertical < 0) {
				pos += new Vector3 (0f, -0.5f);
				canMove = false;
			}
		}

		if (!transform.position.Equals (pos))
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed);
		else {
			canMove = true;
			StartCoroutine (waiter ());
		}
	}

	IEnumerator waiter(){
		yield return new WaitForSeconds (1);
	}
}
