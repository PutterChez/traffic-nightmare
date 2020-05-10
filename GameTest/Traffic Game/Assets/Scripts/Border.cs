using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
  private CarBotScript currentCar;
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!other.isTrigger && other.gameObject.tag == "Car")
    {
      currentCar = other.gameObject.GetComponent<CarBotScript>();
      if (currentCar.isInScene()) {
        Destroy(other.gameObject);
      }
      else {
        currentCar.setInScene(true);
      }
    }
  }
}
