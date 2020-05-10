using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBotScript : MonoBehaviour
{
  public float speed;
  public float timeToDie;
  public char direction;
  public float timeToMove;
  public List<string> command = new List<string>();
  public List<char> pathing = new List<char>();
  private float movement_horizontal = 0f, movement_vertical = 0f;
  private bool canMove = true;
  private bool inScene = false;

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
    if (gameObject.tag == "Accident")
    {
      canMove = false;
    }

    if (canMove)
    {
      move(direction);
    }
    else
    {
      if (current_traffic_light != null)
      {
        if (current_traffic_light.light_setting == 1)
          canMove = true;

        else
        {
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

      else
      {
        rigidBody.velocity = Vector2.zero;
      }
    }
  }

  public void updatePath()
  {
    string nextCommand = command[0];
    if (direction == 'n')
    {
      if (nextCommand == "Left")
      {
        pathing.Add('n');
        pathing.Add('w');
        pathing.Add('w');
      }
      else if (nextCommand == "Forward")
      {
        pathing.Add('n');
        pathing.Add('n');
        pathing.Add('n');
      }
      else if (nextCommand == "Right")
      {
        pathing.Add('n');
        pathing.Add('n');
        pathing.Add('e');
        pathing.Add('e');
        pathing.Add('e');
      }
      else
      {
        print("Command update : Error");
      }
    }
    else if (direction == 'e')
    {
      if (nextCommand == "Left")
      {
        pathing.Add('e');
        pathing.Add('n');
        pathing.Add('n');
      }
      else if (nextCommand == "Forward")
      {
        pathing.Add('e');
        pathing.Add('e');
        pathing.Add('e');
      }
      else if (nextCommand == "Right")
      {
        pathing.Add('e');
        pathing.Add('e');
        pathing.Add('s');
        pathing.Add('s');
        pathing.Add('s');
      }
      else
      {
        print("Command update : Error");
      }
    }
    else if (direction == 's')
    {
      if (nextCommand == "Left")
      {
        pathing.Add('s');
        pathing.Add('e');
        pathing.Add('e');
      }
      else if (nextCommand == "Forward")
      {
        pathing.Add('s');
        pathing.Add('s');
        pathing.Add('s');
      }
      else if (nextCommand == "Right")
      {
        pathing.Add('s');
        pathing.Add('s');
        pathing.Add('w');
        pathing.Add('w');
        pathing.Add('w');
      }
      else
      {
        print("Command update : Error");
      }
    }
    else if (direction == 'w')
    {
      if (nextCommand == "Left")
      {
        pathing.Add('w');
        pathing.Add('s');
        pathing.Add('s');
      }
      else if (nextCommand == "Forward")
      {
        pathing.Add('w');
        pathing.Add('w');
        pathing.Add('w');
      }
      else if (nextCommand == "Right")
      {
        pathing.Add('w');
        pathing.Add('w');
        pathing.Add('n');
        pathing.Add('n');
        pathing.Add('n');
      }
      else
      {
        print("Command update : Error");
      }
    }
    command.RemoveAt(0);
  }

  // private void OnCollisionEnter2D(Collision2D other)
  // {
  //   if (other.gameObject.tag == "Car" || other.gameObject.tag == "Accident")
  //   {
  //     canMove = false;
  //     gameObject.tag = "Accident";
  //     other.gameObject.tag = "Accident";
  //     Destroy(gameObject, timeToDie);
  //   }
  // }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Accident")
    {
      otherCar = FindObjectOfType<CarBotScript>();
      canMove = false;
      Invoke("enableMove", otherCar.timeToDie);
    }
    else if (other.gameObject.tag == "Car")
    {
      canMove = false;
    }

    else
    {
      current_traffic_light = other.gameObject.GetComponent<TrafficController>();
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag == "Car")
    {
      Invoke("enableMove", timeToMove);
    }
  }

  public void enableMove()
  {
    canMove = true;
  }

  public void disableMove()
  {
    canMove = false;
  }

  public void setDirection(char e)
  {
    this.direction = e;
  }

  public void setInScene(bool e) {
    inScene = e;
  }

  public bool isInScene() {
    return inScene;
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
