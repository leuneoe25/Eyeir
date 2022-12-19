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
    public int nowSkill = -1;

    public SkillCommand command;
    private Animator animator;
    private StateAni beforeState = StateAni.Idle;
    void Start()
    {
        animator = GetComponent<Animator>();
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
    public void SetAnimator(StateAni state)
    {
        //if(beforeState == StateAni.Idle && state == StateAni.Run)
        //{
        //    state = StateAni.beforRun;
        //}
        //else
        //{
        //    beforeState = state;
        //}
        animator.SetInteger("State", (int)state);
    }
    public enum StateAni
    {
        Idle,
        Run,
        Hooking,
        Slide,
        jump

    }
}
