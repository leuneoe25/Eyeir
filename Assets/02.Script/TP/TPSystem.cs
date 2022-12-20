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
    private string PlayScene;
    private Vector2 pos;
    public GameObject Player;

    private void Start()
    {
        nowScene = "";  //�ʱⰪ
        PlayScene = SceneManager.GetActiveScene().name;
        Debug.Log("");
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name != PlayScene)    //�÷������� ���� �ʱ���� �ٸ��ٸ�
        {

            Instantiate(Player, pos, Quaternion.identity);//��ġ�� �̵���Ų��.
            PlayScene = SceneManager.GetActiveScene().name;     //�ʱ���� ��������� ���´�.
        }
    }
    public void TP(string tp2, Vector2 Position)
    {
        //pos = Position;
        SceneManager.LoadScene(tp2);        //�� �̵�
    }
}
