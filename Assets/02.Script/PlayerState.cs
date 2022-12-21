using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public float Speed = 10;
    public float JumpPower = 20;

    public bool isWallWalk = false;
    public bool Rope = false;
    public bool isGround = false;
    public float WallWalkPosX = 0f;
    public bool isJumping = false;
    public bool isStop = false;

    public int PlayerHp = 3;
    private int NowHp;
    public int nowSkill = -1;
    public bool StopHook = false;
    public bool isIceTile = false;
    private Animator animator;
    private StateAni beforeState = StateAni.Idle;
    public bool isSkilling = false;
    public GameObject[] Hp;
    public CinemachineImpulseSource impulseSource;
    public Image DamagePade;
    private bool isdelay = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        NowHp = PlayerHp;

        SetHpUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            AddHp();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Damaged();
        }
    }
    public void SetHpUI()
    {
        for(int i = 0;i < 11; i++)
        {
            if(i>= PlayerHp)
            {
                Hp[i].gameObject.SetActive(false);
                continue;
            }
            Hp[i].gameObject.SetActive(true);
            if (i< NowHp)
            {
                Hp[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Hp[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);

            }
            else
            {
                Hp[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                Hp[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    public void AddHp()
    {
        if(PlayerHp>=11)
        {
            Debug.Log("MAX HP!!");
            return;
        }
        PlayerHp++;
        NowHp++;
        SetHpUI();
    }
    public void Damaged()
    {
        
        if (!ItemManager.Instance.Usedefense() && !isdelay)
        {
            Debug.Log("dddd");
            NowHp -= 1;
            impulseSource.GenerateImpulse(0.7f);
            if (NowHp==0)
            {
              Debug.Log("DIE");
            }
            StartCoroutine(DamagePadeIn());

        }
        StartCoroutine(delay());
        SetHpUI();
    }
    public void Heal()
    {
        NowHp++;
        if(NowHp>PlayerHp)
        {
            Debug.Log("MAX HP!!");
            NowHp = PlayerHp;
        }
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
    public int GetHP()
    {
        return NowHp;
    }
    public enum StateAni
    {
        Idle,
        Run,
        Hooking,
        Slide,
        jump,
        Stab,
        Cut

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            
            Damaged();
        }
        if(!ItemManager.Instance.DTrap)
        {
            if (collision.CompareTag("Trap"))
            {
                Damaged();
            }
        }
    }
    IEnumerator DamagePadeIn()
    {
        DamagePade.gameObject.SetActive(true);
        DamagePade.color = new Color(0.594f, 0, 0, 0);
        
        while (DamagePade.color.a < 0.3f)
        {
            DamagePade.color += new Color(0, 0, 0, 0.1f); ;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DamagePadeOut());
    }
    IEnumerator DamagePadeOut()
    {
        DamagePade.color = new Color(0.594f, 0, 0, 0.3f);

        while (DamagePade.color.a > 0)
        {
            DamagePade.color -= new Color(0, 0, 0, 0.1f); ;
            yield return new WaitForSeconds(0.05f);
        }
        DamagePade.gameObject.SetActive(false);
    }
    IEnumerator delay()
    {
        isdelay = true;
        yield return new WaitForSeconds(3f);
        isdelay = false;
    }
    
}
