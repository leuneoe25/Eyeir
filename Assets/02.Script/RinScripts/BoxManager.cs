using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : ItemManager
{
    enum Item
    {
        BrokenMirror,
        Match,
        Icicle,
        Snowball,
        Eyecrystal,
        Wand,
        Holly

    }

    void Start()
    {
        //아이템 랜덤호출
        RandomItem();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomItem()
    {
        int I = Random.RandomRange(0, 8);
        if(I == 0)
        {
            Debug.Log(Item.BrokenMirror);

        }
        else if (I == 1)
        {
            Debug.Log(Item.Match);
        }
        else if (I == 2)
        {
            Debug.Log(Item.Icicle);
        }
        else if (I == 3)
        {
            Debug.Log(Item.Snowball);
        }
        else if(I == 4)
        {
            Debug.Log(Item.Eyecrystal);
        }
        else if(I == 5)
        {
            Debug.Log(Item.Eyecrystal);
        }
        else if(I == 6)
        {
            Debug.Log(Item.Wand);
        }
        else if(I == 7)
        {
            Debug.Log(Item.Holly);
        }
    }
}