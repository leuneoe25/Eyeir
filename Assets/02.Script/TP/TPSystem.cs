using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TPSystem : MonoBehaviour
{
    #region Singleton
    //private static TPSystem instance = null;
    //void Awake()
    //{
    //    if (null == instance)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
    //public static TPSystem Instance
    //{
    //    get
    //    {
    //        if (null == instance)
    //        {
    //            return null;
    //        }
    //        return instance;
    //    }
    //}
    #endregion
    [SerializeField] private Image Pade;

    private void Start()
    {
    }

    IEnumerator PadeIn(GameObject Player, Vector2 Position)
    {
        Pade.gameObject.SetActive(true);
        Pade.color = new Color(0, 0, 0, 0);

        while (Pade.color.a < 1)
        {
            Pade.color += new Color(0, 0, 0, 0.1f); ;
            yield return new WaitForSeconds(0.05f);
        }

        Player.transform.position = Position;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(PadeOut());
    }
    IEnumerator PadeOut()
    {
        Pade.color = new Color(0, 0, 0, 1);

        while (Pade.color.a > 0)
        {
            Pade.color -= new Color(0, 0, 0, 0.1f); ;
            yield return new WaitForSeconds(0.05f);
        }
        Pade.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if(SceneManager.GetActiveScene().name != PlayScene)    //플레이중인 씬과 초기씬이 다르다면
        //{
        //    Debug.Log("a");
             

        //    //gameObject.SetActive(true);
        //    //state = true;
        //    Instantiate(Player, pos, Quaternion.identity);//위치를 이동시킨다.
        //    PlayScene = SceneManager.GetActiveScene().name;     //초기씬을 현재씬으로 덮는다.
        //}
    }
    public void TP(GameObject Player, Vector2 Position)
    {
        
        StartCoroutine(PadeIn(Player, Position));
             //씬 이동
       // gameObject.SetActive(true);
       // state = true;
       
    }

    
}
