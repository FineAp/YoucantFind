using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject SM_Win;
    public GameObject SM_Die;
    public GameObject SM_BG;

    public bool isBG = false;
    public bool isWin= false;
    public bool isDies = false;

    void Start()
    {
        isBG = true;
        if(isBG)
        {
            SM_BG.SetActive(true);
        }
    }

    void Update()
    {
        if(isWin)
        {
            SM_BG.SetActive(false);
            SM_Win.SetActive(true);
            isBG = false;

        }

        else if(isDies)
        {
            SM_BG.SetActive(false);
            SM_Die.SetActive(true);
            isBG = false;
            print(isDies);

        }
    }

}
