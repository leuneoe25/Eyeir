using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMove : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private BossStat bs;
    private float move = 2;
    bool isDie = false;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bs.BossPattern)
        {
            Vector3 dir = Player.transform.position - startPos;
            transform.position = startPos + (dir.normalized * move);
        }
        
    }
}
