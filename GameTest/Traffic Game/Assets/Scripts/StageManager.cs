using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
  public static int carsLeft = 0;
  public static int carLimit = 10;

  public static int carsCount;
  void Start()
  {
    ;
  }

  // Update is called once per frame
  void Update()
  {
    GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
    carsLeft = cars.Length;
    print(carsLeft);
  }
}
