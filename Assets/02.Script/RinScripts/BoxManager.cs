using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : ItemManager
{
    private bool stay = false;
    void Start()
    {
        //아이템 랜덤호출
        stay = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && stay)
        {

            //z를 누르시오 표시
            RandomItem();
            //상자 사라지기
            StartCoroutine(DestoryBox());
        }
    }

    public void RandomItem()
    {
        int I = Random.RandomRange(0, 8);
        ItemManager.Instance.AddItem((ItemName)I);
        int c = Random.RandomRange(20, 31);

        if (ItemManager.Instance.moreCoin)
            c += 10;
        ItemManager.Instance.Coin += c;
        #region rin
        //if (I == 0)
        //{
        //    Debug.Log(Item.BrokenMirror);

        //}
        //else if (I == 1)
        //{
        //    Debug.Log(Item.Match);
        //}
        //else if (I == 2)
        //{
        //    Debug.Log(Item.Icicle);
        //}
        //else if (I == 3)
        //{
        //    Debug.Log(Item.Snowball);
        //}
        //else if(I == 4)
        //{
        //    Debug.Log(Item.Eyecrystal);
        //}
        //else if(I == 5)
        //{
        //    Debug.Log(Item.Eyecrystal);
        //}
        //else if(I == 6)
        //{
        //    Debug.Log(Item.Wand);
        //}
        //else if(I == 7)
        //{
        //    Debug.Log(Item.Holly);
        //}
        #endregion
    }
    IEnumerator DestoryBox()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //페이드 아웃 실행
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stay = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stay = false;
        }
    }
}