using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
  // Start is called before the first frame update
  private CarBotScript currentCar;
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (!other.isTrigger)
    {
      currentCar = other.gameObject.GetComponent<CarBotScript>();
      char currentDirection = currentCar.pathing[0];
      currentCar.setDirection(currentDirection);
      currentCar.pathing.Remove(currentDirection);
    }

  }
}
