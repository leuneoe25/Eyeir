using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
