using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hell : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float x;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition += new Vector3(x,0,0) * Speed;
    }
}
