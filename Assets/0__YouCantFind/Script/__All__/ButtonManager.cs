using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject Map;
    public GameObject winUI;
    public GameObject level_2_Map;

    private int count = 0;

    public void OppenMap()
    {
        Map.SetActive(true);
        winUI.SetActive(false);

    }
    public void CloseMap()
    {
        Map.SetActive(false);

    }
    
    public void Level_1()
    {
        SceneManager.LoadScene("___Level_1___");
        
    }

    public void Level_2()
    {
        SceneManager.LoadScene("___Level_2___");
        
    }

    public void Level_3()
    {
        SceneManager.LoadScene("___Level_3___");
        
    }
    
    public void Game()
    {
        SceneManager.LoadScene("___GAME___");
    }

    public void Level_2_OpenMap()
    {
        print(count);
        if(count == 0)
        {
            level_2_Map.SetActive(true);
            count += 1;
        }
        else
        {
            level_2_Map.SetActive(false);
            count = 0;
        }

    }

    public void Exit()
    {
        Application.Quit();
    }

}
