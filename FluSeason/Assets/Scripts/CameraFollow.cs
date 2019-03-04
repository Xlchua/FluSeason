using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    public Vector2 minCameraPos;
    public Vector2 maxCameraPos;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.transform.position + offset;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                                         Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                                                     transform.position.z);
    }
}
