using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TPSystem : MonoBehaviour
{
    #region Singleton
    private static TPSystem instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static TPSystem Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion
    private string nowScene;
    private string PlayScene;
    private Vector2 pos;
    public GameObject Player;

    private bool state;

    private void Start()
    {
        state = true;
        nowScene = "";  //초기값
        PlayScene = SceneManager.GetActiveScene().name;
        Debug.Log("");
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name != PlayScene)    //플레이중인 씬과 초기씬이 다르다면
        {
            gameObject.SetActive(false);
            state = false;
            Instantiate(Player, pos, Quaternion.identity);//위치를 이동시킨다.
            PlayScene = SceneManager.GetActiveScene().name;     //초기씬을 현재씬으로 덮는다.
        }
    }
    public void TP(string tp2, Vector2 Position)
    {
        pos = Position;
        SceneManager.LoadScene(tp2);        //씬 이동
        gameObject.SetActive(true);
        state = true;
    }

    
}
