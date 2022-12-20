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
        nowScene = "";  //�ʱⰪ
    }
    private void Update()
    {
        if( nowScene != �����)    //�÷������� ���� �ʱ���� �ٸ��ٸ�
        {
            Vector2                //��ġ�� �̵���Ų��.
            nowScene = �����;     //�ʱ���� ��������� ���´�.
        }
    }
    public void sa(string tp2, Vector2 Position)    //
    {

        SceneManager.LoadScene(tp2);        //�����̵�
        Position();
    }
}
