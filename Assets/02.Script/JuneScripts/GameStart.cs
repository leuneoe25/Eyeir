using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void Game_s()
    {
        Debug.Log("start");
        SceneManager.LoadScene("SampleScene");
    }
    public void Game_e()
    {
        Debug.Log("end");
        Application.Quit();
    }
}
