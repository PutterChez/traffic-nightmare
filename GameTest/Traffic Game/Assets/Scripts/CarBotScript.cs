using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBotScript : MonoBehaviour
{
  public float speed = 2f;
  public float timeToDie = 2f;
  public char direction;
  public List<char> pathing = new List<char>();
  private float movement_horizontal = 0f, movement_vertical = 0f;
  private bool canMove = true;

  private Rigidbody2D rigidBody;
  private TrafficController current_traffic_light;
  private CarBotScript otherCar;

  // Use this for initialization
  void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (gameObject.tag == "Accident") {
      canMove = false;
    }

    if (canMove) {
      move(direction);
    }
    else {
        if (current_traffic_light != null) {
            if (current_traffic_light.light_setting == 1)
                canMove = true;

            else{
                if ((current_traffic_light.tag == "TrafficLightN") && (direction == 'n'))
                    rigidBody.velocity = Vector2.zero;

                if ((current_traffic_light.tag == "TrafficLightE") && (direction == 'e'))
                    rigidBody.velocity = Vector2.zero;

                if ((current_traffic_light.tag == "TrafficLightS") && (direction == 's'))
                    rigidBody.velocity = Vector2.zero;

                if ((current_traffic_light.tag == "TrafficLightW") && (direction == 'w'))
                    rigidBody.velocity = Vector2.zero;
            }
        }

        else {
            rigidBody.velocity = Vector2.zero;
        }
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Car" || other.gameObject.tag == "Accident")
    {
      canMove = false;
      gameObject.tag = "Accident";
      other.gameObject.tag = "Accident";
      print("Car Crash!!!");
      Destroy(gameObject, timeToDie);
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Accident") {
      otherCar = FindObjectOfType<CarBotScript>();
      canMove = false;
      Invoke("enableMove", otherCar.timeToDie);
    }

    else {
        current_traffic_light = other.gameObject.GetComponent<TrafficController>();
    }
  } 

  public void enableMove() {
    canMove = true;
  }

  public void disableMove(){
    canMove = false;
  }

    public void setDirection(char e)
  {
    this.direction = e;
  }

  void move(char direction)
  {
    float smooth = 8.0f;
    float angle = 0.0f;
    switch (direction)
    {
      case 'n': rigidBody.velocity = new Vector2(0, 1 * speed); angle = 0.0f; break;
      case 's': rigidBody.velocity = new Vector2(0, -1 * speed); angle = 180.0f; break;
      case 'e': rigidBody.velocity = new Vector2(1 * speed, 0); angle = 270.0f; break;
      case 'w': rigidBody.velocity = new Vector2(-1 * speed, 0); angle = 90.0f; break;
    }
    Quaternion target = Quaternion.Euler(0, 0, angle);
    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
  }
}
