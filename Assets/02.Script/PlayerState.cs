using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float Speed = 10;
    public float JumpPower = 20;
    public bool isWallWalk = false;
    public bool Rope = false;
    public bool isGround = false;
    public float WallWalkPosX = 0f;
    public bool isJumping = false;

    public int PlayerHp = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damaged()
    {
        PlayerHp--;
    }
    public void StartWallWalk(float PosX)
    {
        isWallWalk = true;
        //rigidbody.gravityScale = 0;
        WallWalkPosX = PosX;
    }
}
