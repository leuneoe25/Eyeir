using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeBoss : Boss
{
    [SerializeField] private BossStat bs;
    [SerializeField] private Image Hpbar;
    private int Stat = -1;
    private bool isPattern = false;
    private bool StartPattern = true;
    //[Header("Pattern 1")]
    //[SerializeField] private GameObject[] AttackArea_1;
    //[SerializeField] private GameObject[] Attack_1;


    Coroutine nowPattern = null;
    [Header("Pattern_1")]
    public GameObject[] AtackArea_1;
    public GameObject[] Atack_1;
    public GameObject[] Hell_Atack_1;
    [Header("Pattern_2")]
    public GameObject Ray;
    public GameObject center;
    public int rotateSpeed;
    [Header("Pattern_3")]
    public GameObject[] AtackArea_3;
    public GameObject Atack_3;

    private void Awake()
    {
    }

    void Update()
    {
        //if (!isPattern)
        //    StartCoroutine(Pattern_2());
        if (bs.BossPattern)
        {
            if (isPattern)
                return;
            if (StartPattern)
            {
                StartCoroutine(PtternStart());
                return;
            }
            RandomPattern();
        }
    }
    private void RandomPattern()
    {

        int s = Random.Range(0, 4);

        while (Stat == s)
        {
            s = Random.Range(0, 4);
        }
        switch (s)
        {
            case 0:
                nowPattern = StartCoroutine(Pattern_1());
                break;
            case 1:
                nowPattern = StartCoroutine(Pattern_2());
                break;
            case 2:
                nowPattern = StartCoroutine(Pattern_3());
                break;
        }
        Debug.Log("Stat : " + s);
    }
    IEnumerator PtternStart()
    {
        bs.Player.GetComponent<PlayerState>().isStop = true;
        bs.Player.GetComponent<PlayerState>().StopHook = true;
        Debug.Log("Start");
        isPattern = true;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        StartCoroutine(HpbarAni());
        yield return new WaitForSeconds(1f);
        StartCoroutine(PadeBoss());
    }
    #region Start
    IEnumerator HpbarAni()
    {
        Hpbar.fillAmount = 0;
        while (Hpbar.fillAmount < 1)
        {
            Hpbar.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        bs.Player.GetComponent<PlayerState>().isStop = false;
        bs.Player.GetComponent<PlayerState>().StopHook = false;
    }
    IEnumerator PadeBoss()
    {
        while (gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color.a < 1)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        bs.Player.GetComponent<PlayerState>().isStop = false;
        bs.Player.GetComponent<PlayerState>().StopHook = false;
        StartPattern = false;
        isPattern = false;
        yield return new WaitForSeconds(3f);
    }
    #endregion
    IEnumerator Pattern_1()
    {
        isPattern = true;
        for (int i = 0; i < AtackArea_1.Length; i++)
        {
            AtackArea_1[i].SetActive(true);
            yield return new WaitForSeconds(1.5f);
            AtackArea_1[i].SetActive(false);
            //hell
            Vector2 v = Atack_1[i].transform.position;
            Atack_1[i].SetActive(true);
            //Hell
            Hell_Atack_1[i].SetActive(true);
            yield return new WaitForSeconds(1f);
            Hell_Atack_1[i].transform.position = v;
            Hell_Atack_1[i].SetActive(false);
        }

        for (int i = 0; i < AtackArea_1.Length; i++)
        {
            AtackArea_1[i].SetActive(false);
            Atack_1[i].SetActive(false);
        }
        yield return new WaitForSeconds(3f);
        isPattern = false;
    }
    IEnumerator Pattern_2()
    {
        isPattern = true;
        float time = 3;
        center.SetActive(true);
        while (true)
        {
            if (time < 0)
                break;
            Vector2 dir = center.transform.position - bs.Player.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle-90, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(center.transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
            center.transform.rotation = rotation;
            time -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        center.SetActive(false);
        yield return new WaitForSeconds(1f);
        Ray.SetActive(true);
        Ray.transform.rotation = center.transform.rotation;
        yield return new WaitForSeconds(0.5f);
        Ray.SetActive(false);
        yield return new WaitForSeconds(3f);
        isPattern = false;
    }
    IEnumerator Pattern_3()
    {
        isPattern = true;
        for(int i= 0;i< AtackArea_3.Length; i++)
        {
            AtackArea_3[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        Atack_3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Atack_3.SetActive(false);
        for (int i = 0; i < AtackArea_3.Length; i++)
        {
            AtackArea_3[i].SetActive(false);
        }
        yield return new WaitForSeconds(3f);
        isPattern = false;
    }



    public override void Clear()
    {
        StopCoroutine(nowPattern);

        //for (int i = 0; i < 4; i++)
        //{
        //    AttackArea_1[i].SetActive(false);
        //}
        //AttackArea_2.SetActive(false);
    }


}
