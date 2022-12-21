using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowBoss : Boss
{
    [SerializeField] private BossStat bs;
    [SerializeField] private Image Hpbar;
    private int Stat = -1;
    private bool isPattern = false;
    private bool StartPattern = true;
    [Header("Pattern")]
    [SerializeField] private GameObject[] AttackArea;

    Coroutine nowPattern = null;
    
    void Update()
    {
        if(bs.BossPattern)
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
        while(Stat == s)
        {
            s = Random.Range(0, 4);
        }
        switch(s)
        {
            case 0:
                nowPattern = StartCoroutine( Pattern_1());
                break;
            case 1:
                nowPattern = StartCoroutine(Pattern_2());
                break;
            case 2:
                nowPattern = StartCoroutine(Pattern_3());
                break;
        }
        Debug.Log("Stat : " +s);
    }
    IEnumerator PtternStart()
    {
        bs.Player.GetComponent<PlayerState>().isStop = true;
        bs.Player.GetComponent<PlayerState>().StopHook = true;
        Debug.Log("Start");
        isPattern = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
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
    }
    IEnumerator PadeBoss()
    {
        while (gameObject.GetComponent<SpriteRenderer>().color.a < 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
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
        for(int i=0;i<4;i++)
        {
            AttackArea[i].SetActive(true);
            yield return new WaitForSeconds(2f);
            AttackArea[i].SetActive(false);
            //°ø°Ý ÈÄ 
            yield return new WaitForSeconds(0.5f);
        }
           
        isPattern = false;
    }
    IEnumerator Pattern_2()
    {
        isPattern = true;
        yield return new WaitForSeconds(3f);
        isPattern = false;
    }
    IEnumerator Pattern_3()
    {
        isPattern = true;
        yield return new WaitForSeconds(3f);
        isPattern = false;
    }
    public override void Clear()
    {
        StopCoroutine(nowPattern);

        for(int i= 0;i<4;i++)
        {
            AttackArea[i].SetActive(false);
        }
    }
}
