using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour {
    public GameObject PlayerObject;
    public float SpeedX;
    public float SpeedY;

    public Vector2 MinMaxX;
    void Update()
    {
        float PosX = Mathf.Lerp(PlayerObject.transform.position.x, transform.position.x, Time.deltaTime * SpeedX);
        float PosY = Mathf.Lerp(transform.position.y, PlayerObject.transform.position.y , Time.deltaTime * SpeedY);
        PosX = Mathf.Clamp(PosX,MinMaxX.x, MinMaxX.y);
        transform.position = new Vector3(PosX, PosY,-10);
    }
}
