using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //About: Player HP
    public int maxLife = 4; //Max Hp : Can change frome developer
    public int life = 3; 
    public Image[] heartImg; 
    private Color redColor = Color.red;
    private Color blackColor = Color.black;


    //About: All UI
    public GameObject dieUI;
    public GameObject winUI;
    public GameObject exitUI;
    public GameObject time;   

    //About: All Sound  
    public SoundManager sound;

    //About: Evenet
    public bool isDie = false; //About: Player.cs -> if life = 0 , isDie = true
    public bool haveWater = false; //Player.cs -> OnTrigger.Tag.("Water") havewater = true -> Can get Iteam
    public bool canOpen = false; //player.cs -> Attack(), for Tresure.Animation

    public bool canGet = true;
    public bool getWater = false;
    public bool die = false; //player.cs -> Die(), 
    public bool timeDie = false;

    //About: Level_3 For Last_Map
    public GameObject lastTresure;
    public Text last_text;
    private GameObject last_Enemys;
    private GameObject last_Random;
    public int last_count = 10;

    //About All_Scene first Manual
    public GameObject manual;

    public Animator anim;

    void Start()
    {
        last_Enemys = GameObject.Find("Enemys");
        last_Random = GameObject.Find("Random");
        Invoke("Manual", 3f);

    }

    void Update()
    {
        Die();
        Goal();
        UpdateLife();
        CanGet();
        LastTresure();

    }
    //For Delay from first Manual
    void Manual()
    {
        manual.SetActive(false);
    }

    void Die()
    {
        if(life == 0)
        {
            dieUI.SetActive(true);
            time.SetActive(false);
            exitUI.SetActive(false);
            isDie = true;
            //All HeartUI.SetActive(flase)
            for(int i = 0; i < 4; i++)
            {
                heartImg[i].enabled = false;
            }

            sound.isDies = true;
            sound.isBG = false;

        }
        else if(die)
        {
            dieUI.SetActive(true);
            time.SetActive(false);
            exitUI.SetActive(false);
            isDie = true;
            for(int i = 0; i < 4; i++)
            {
                heartImg[i].enabled = false;
            }

            sound.isDies = true;
            sound.isBG = false;
            print(sound.isDies);

        }

        else if(timeDie)
        {
            dieUI.SetActive(true);
            time.SetActive(false);
            isDie = true;
            for(int i = 0; i < 4; i++)
            {
                heartImg[i].enabled = false;
            }

            sound.isDies = true;
            sound.isBG = false;
            print(sound.isDies);

        }
    }

    void Goal()
    {
        if(canOpen)
        {
            anim.SetTrigger("ToOpen");
            Invoke("DelayOpenGoal", 3f);

        }
    }
    //Delay open Tresure
    void DelayOpenGoal()
    {
        winUI.SetActive(true);
        sound.isWin = true;
        sound.isDies = false;
        sound.isBG = false;
    }

    void LastTresure()
    {
        last_text.text = last_count.ToString();

        if(last_count == 0)
        {
            lastTresure.SetActive(true);
            Destroy(last_Random);
            Destroy(last_Enemys);
        }
    }

    void CanGet()
    {
        if(canGet)
        {
            getWater = false;

        }
        else
        {
            getWater = true;
        }
    }

    //About Heart UI 
    public void UpdateLife()
    {
        //If player have life, life color is 'red'
        if (life >= maxLife)
        {
            foreach (Image heartImg in heartImg)
            {
                heartImg.color = redColor;
            }
        }
        //If player was attacked from Enemys color of life is 'Black'
        else
        {
            for (int i = 0; i < maxLife; i++)
            {
                if (i < life)
                {
                    heartImg[i].color = redColor;
                }
                else
                {
                    heartImg[i].color = blackColor;
                }
            }
        }
    }




}
