using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform player;
    private Transform cameraTransform;
    public Vector3 cameraOffset = new Vector3(0f, 1.5f, -3f);

    void Start()
    {
        cameraTransform = transform;
    }

    void LateUpdate()
    {
        cameraTransform.position = player.position + cameraOffset;
        
    }

}
