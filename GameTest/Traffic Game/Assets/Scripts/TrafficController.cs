using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour {

	public List<Sprite> light_sprites;
	public Sprite green, red;

    public int light_setting = 1;
    private CarBotScript currentCar;

    // Use this for initialization
    void Start ()
    {
        light_sprites.Add(red);
        light_sprites.Add(green);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Collider2D coll = this.gameObject.GetComponent<Collider2D> ();

			if (coll.OverlapPoint (wp)) {
                if (light_setting == 1)
                    light_setting = 0;
                else if (light_setting == 0)
                    light_setting = 1;
			}
		}
        this.gameObject.GetComponent<SpriteRenderer>().sprite = light_sprites[light_setting];
    }

	public int getLightSetting(){
		return light_setting;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            currentCar = other.gameObject.GetComponent<CarBotScript>();

            if (light_setting == 0)
                currentCar.disableMove();
        }

    }
}
