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

    Color color;

    private bool state;

    private void Start()
    {
        //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        //color.a = 0;
        //renderer.color = color;
        ////state = false;
        //nowScene = "";  //�ʱⰪ
        //PlayScene = SceneManager.GetActiveScene().name;
        //Debug.Log("");
    }

    IEnumerator CountAttackDelay()
    {
        
        while(gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color.a < 1)
        {
            Debug.Log("b");
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
            color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        


    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name != PlayScene)    //�÷������� ���� �ʱ���� �ٸ��ٸ�
        {
            Debug.Log("a");
             

            //gameObject.SetActive(true);
            //state = true;
            Instantiate(Player, pos, Quaternion.identity);//��ġ�� �̵���Ų��.
            PlayScene = SceneManager.GetActiveScene().name;     //�ʱ���� ��������� ���´�.
        }
    }
    public void TP(string tp2, Vector2 Position)
    {
        pos = Position;
        StartCoroutine(CountAttackDelay());
        SceneManager.LoadScene(tp2);        //�� �̵�
       // gameObject.SetActive(true);
       // state = true;
       
    }

    
}
