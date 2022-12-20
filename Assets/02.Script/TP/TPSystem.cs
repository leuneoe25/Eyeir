using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private void Start()
    {
        nowScene = "";  //초기값
    }
    private void Update()
    {
        if( nowScene != 현재씬)    //플레이중인 씬과 초기씬이 다르다면
        {
            Vector2                //위치를 이동시킨다.
            nowScene = 현재씬;     //초기씬을 현재씬으로 덮는다.
        }
    }
    public void sa(string tp2, Vector2 Position)    //
    {

        SceneManager.LoadScene(tp2);        //순간이동
        Position();
    }
}
