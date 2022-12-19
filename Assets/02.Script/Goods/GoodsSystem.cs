using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsSystem : MonoBehaviour
{
    private int Coin = 100;

    public int GetCoin()
    {
        return Coin;
    }
    public bool UseCoin(int usage)
    {
        if (Coin >= usage)
        {
            Coin -= usage;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddCoin(int add)
    {
        Coin += add;
    }
}
