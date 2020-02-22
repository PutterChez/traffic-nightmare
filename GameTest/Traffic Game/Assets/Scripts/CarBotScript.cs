using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBotScript : MonoBehaviour {
	public float speed = 2f;
	private float movement_horizontal = 0f, movement_vertical = 0f;
	private bool canMove = true;

	private Rigidbody2D rigidBody;
	private TrafficController current_traffic_light;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//rigidBody.velocity = new Vector2 (movement_horizontal * speed, rigidBody.velocity.y);

		if (canMove)
			rigidBody.velocity = new Vector2 (1 * speed, rigidBody.velocity.y);

		else {
			if (current_traffic_light.getLightSetting() != 1)
				rigidBody.velocity = new Vector2 (0f, 0f);
			else
				canMove = true;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		
		current_traffic_light = collider.gameObject.GetComponent<TrafficController> ();

		if ((current_traffic_light != null) && (current_traffic_light.getLightSetting () == 3))
			canMove = false;
		else
			canMove = true;
	}

	void OnCollisionStay(Collision collisionInfo){
		Debug.Log ("Stopping");
	}
}
