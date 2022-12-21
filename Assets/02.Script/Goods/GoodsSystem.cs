using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsSystem : MonoBehaviour
{
    #region Singleton
    private static GoodsSystem instance = null;
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
    public static GoodsSystem Instance
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
        ItemManager.Instance.UISet();
    }
}
