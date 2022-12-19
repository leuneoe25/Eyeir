using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSystem : MonoBehaviour
{
    public float gas = 100;
    public float Usage = 1f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gas);
    }
    public float GetGas()
    {
        return gas;
    }
    public void UseGas()
    {
        gas -= Usage * Time.deltaTime;
    }
}
