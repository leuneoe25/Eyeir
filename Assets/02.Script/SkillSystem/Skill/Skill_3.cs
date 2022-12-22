using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3 : Skill
{
    [SerializeField] private float Damage;
    [SerializeField] private int level;
    private float coolTime = 0;
    public float MaxCoolTime = 20f;
    void Start()
    {
        
    }
    public override void ExcutSkill(PlayerState ps, GameObject Character, GameObject Effect = null)
    {
        if (ps.isSkilling)
        {
            Debug.Log("IS Skilling");
            return;
        }
        if (coolTime > 0)
            return;
        StartCoroutine(Excut(ps, Character, Effect));
    }
    public override float GetDamage()
    {
        return Damage;
    }
    public override float GetCoolTime()
    {
        return coolTime / MaxCoolTime;
    }

    public override int GetSkillLevel()
    {
        return level;
    }

    public override void SkillLevelUp()
    {
        if (level >= 2)
            return;
        level++;
    }
    private IEnumerator Excut(PlayerState ps, GameObject Character, GameObject Effect)
    {
        ps.isSkilling = true;
        ps.StopHook = true;
        Debug.Log("Skill 3");
        //캐릭터 이미지가 보고있는 방향이 반대임
        //Character.transform.localScale = new Vector3(-Character.transform.localScale.x, 1, 1);
        ps.SetAnimator(PlayerState.StateAni.Idle);
        //Debug.Log()
        Effect.SetActive(true);
        Effect.transform.position = Character.transform.position;
        //if (Character.transform.localScale.x > 0)
        //{
        //    Effect.transform.position = new Vector2(Character.transform.position.x - 5, Character.transform.position.y);
        //    Effect.transform.localScale = new Vector3(1, 1, 1);
        //}
        //else
        //{
        //    Effect.transform.position = new Vector2(Character.transform.position.x + 5, Character.transform.position.y);
        //    Effect.transform.localScale = new Vector3(-1, 1, 1);
        //}
        yield return new WaitForSeconds(0.5f);
        Effect.SetActive(false);
        //캐릭터 이미지가 보고있는 방향이 반대임
        //Character.transform.localScale = new Vector3(-Character.transform.localScale.x, 1, 1);
        coolTime = MaxCoolTime;
        ps.SetAnimator(PlayerState.StateAni.Idle);
        ps.isSkilling = false;
        ps.StopHook = false;
    }
    public override void Clear()
    {
        level = 1;
        coolTime = 0;
    }
    void Update()
    {
        if(coolTime > 0)
        {
            coolTime -= Time.deltaTime;
        }
        else if( coolTime != 0)
        {
            coolTime = 0;
        }
        
    }

}
