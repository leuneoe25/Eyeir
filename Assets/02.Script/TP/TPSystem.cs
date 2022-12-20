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
        //nowScene = "";  //초기값
        //PlayScene = SceneManager.GetActiveScene().name;
        //Debug.Log("");
    }

    IEnumerator CountAttackDelay()
    {
        
        while(gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color.a < 1)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
            color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        


    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name != PlayScene)    //플레이중인 씬과 초기씬이 다르다면
        {
             CountAttackDelay();

            //gameObject.SetActive(true);
            //state = true;
            Instantiate(Player, pos, Quaternion.identity);//위치를 이동시킨다.
            PlayScene = SceneManager.GetActiveScene().name;     //초기씬을 현재씬으로 덮는다.
        }
    }
    public void TP(string tp2, Vector2 Position)
    {
        pos = Position;
        SceneManager.LoadScene(tp2);        //씬 이동
       // gameObject.SetActive(true);
       // state = true;
       
    }

    
}
