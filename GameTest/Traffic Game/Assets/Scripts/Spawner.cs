using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject prefab;
  public float spawnTime;
  public char carDirection;
  public int startSpawn;
  public int stopSpawn;
  public List<string> command = new List<string>();
  private float spawnTimer;
  private CarBotScript currentCar;

  void Start()
  {
    ResetSpawnTimer();
  }

  // Update is called once per frame
  void Update()
  {
    spawnTimer -= Time.deltaTime;
    if (spawnTimer <= 0.0f) {
      if (StageManager.carsLeft < StageManager.carLimit && startSpawn <= StageManager.carsCount && StageManager.carsCount < stopSpawn) {
        GameObject obj = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
        currentCar = obj.GetComponent<CarBotScript>();
        currentCar.direction = carDirection;

        int size = command.Count;
        for (int i = 0; i < size; i++)
        {
          currentCar.command.Add(command[i]);
        }
        StageManager.carsCount += 1;
        ResetSpawnTimer();
      }
    }
  }

  void ResetSpawnTimer()
  {
    spawnTimer = (float)(spawnTime);
  }
}
