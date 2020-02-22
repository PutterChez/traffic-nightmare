using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour {

	public List<Sprite> light_sprites;
	public Sprite off, green, yellow, red;

	public int light_setting = 	0;

	// Use this for initialization
	void Start () {
		light_sprites.Add (off);
		light_sprites.Add (green);
		light_sprites.Add (yellow);
		light_sprites.Add (red);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Vector3 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Collider2D coll = this.gameObject.GetComponent<Collider2D> ();

			if (coll.OverlapPoint (wp)) {
				
				if (light_setting < 3)
					light_setting++;
				else
					light_setting = 0;
				
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = light_sprites[light_setting];
			}
		}
	}

	public int getLightSetting(){
		return light_setting;
	}
}
