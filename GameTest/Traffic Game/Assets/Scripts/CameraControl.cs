using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
  public float panSpeed = 9f;
  public float panBorderThickness = 50f;
  public Vector2 Limit;
  public float zoomSpeed = 1;
  public float targetOrtho;
  public float smoothSpeed = 2.0f;
  public float minOrtho = 1f;
  public float maxOrtho = 20.0f;

  // Update is called once per frame
  void Start() 
  {
    targetOrtho = Camera.main.orthographicSize;
  }

  void Update()
  {
    Vector3 pos = transform.position;

    if (Input.mousePosition.y >= Screen.height - panBorderThickness) {
      pos.y += panSpeed * Time.deltaTime;
    }

    if (Input.mousePosition.y <= panBorderThickness) {
      pos.y -= panSpeed * Time.deltaTime;
    }

    if (Input.mousePosition.x >= Screen.width - panBorderThickness) {
      pos.x += panSpeed * Time.deltaTime;
    }

    if (Input.mousePosition.x <= panBorderThickness) {
      pos.x -= panSpeed * Time.deltaTime;
    }

    pos.x = Mathf.Clamp(pos.x, -Limit.x, Limit.x);
    pos.y = Mathf.Clamp(pos.y, -Limit.y, Limit.y);

    float scroll = Input.GetAxis("Mouse ScrollWheel");
    if (scroll != 0.0f) {
      targetOrtho -= scroll * zoomSpeed;
      targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
    }

    Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

    transform.position = pos;
  }
}
