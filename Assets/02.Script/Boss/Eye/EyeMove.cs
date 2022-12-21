using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMove : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private float move = 2;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = Player.transform.position - startPos;
        transform.position = startPos + (dir.normalized* move);
    }
}
