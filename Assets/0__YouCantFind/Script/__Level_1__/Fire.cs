using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") )
        {
            if(gameManager.haveWater)
            {
                Destroy(this.gameObject);
                gameManager.haveWater = false;
                gameManager.canGet = true;
            }
        }
    }
}
