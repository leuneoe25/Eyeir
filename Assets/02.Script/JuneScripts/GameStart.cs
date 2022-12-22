using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void Game_s()
    {
        Debug.Log("start");
        SceneManager.LoadScene("StartEffect");
    }
    public void Game_e()
    {
        Debug.Log("end");
        Application.Quit();
    }
}
