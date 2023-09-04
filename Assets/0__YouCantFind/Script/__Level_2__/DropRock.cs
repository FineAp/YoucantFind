using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRock : MonoBehaviour
{
    public float speed = 3.5f;
    public float dropRot = 30f;

    void Start()
    {
        Invoke("Rock",speed);
    }

    void Update()
    {
        transform.Rotate(10f * Time.deltaTime, dropRot * Time.deltaTime, 0f);
    }


    void Rock()
    {
        Destroy(this.gameObject);
    }
}
