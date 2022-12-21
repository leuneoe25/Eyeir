using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : Skill
{
    [SerializeField] private float Damage;
    [SerializeField] private int level;
    private float coolTime = 0;
    public float MaxCoolTime = 0.5f;

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
    public override float GetDamage()
    {
        return Damage;
    }

    private IEnumerator Excut(PlayerState ps, GameObject Character, GameObject Effect)
    {
        ps.isSkilling = true;
        ps.StopHook = true;
        Debug.Log("Skill 2");
        //ĳ���� �̹����� �����ִ� ������ �ݴ���
        ps.SetAnimator(PlayerState.StateAni.Cut);
        Effect.SetActive(true);
        if (Character.transform.localScale.x > 0)
        {
            Effect.transform.position = new Vector2(Character.transform.position.x - 5, Character.transform.position.y);
            Effect.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Effect.transform.position = new Vector2(Character.transform.position.x + 5, Character.transform.position.y);
            Effect.transform.localScale = new Vector3(-1, 1, 1);
        }
        yield return new WaitForSeconds(0.3f);
        Effect.SetActive(false);
        coolTime = 0.5f;
        ps.SetAnimator(PlayerState.StateAni.Idle);
        ps.isSkilling = false;
        ps.StopHook = false;
    }
    void Update()
    {
        if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
        }
        else if (coolTime != 0)
        {
            coolTime = 0;
        }
    }
}
