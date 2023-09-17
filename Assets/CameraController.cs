using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform xy1;
    public Transform xy2;

    void FixedUpdate()
    {
        float num = Camera.main.orthographicSize / Camera.main.pixelHeight;
        xy1.position = new Vector2(-Camera.main.pixelWidth * num, Camera.main.orthographicSize);
        xy2.position = new Vector2(Camera.main.pixelWidth * num, -Camera.main.orthographicSize);
    }
}
