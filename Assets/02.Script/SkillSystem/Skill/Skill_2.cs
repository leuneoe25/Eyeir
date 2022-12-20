using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : Skill
{
    [SerializeField] private float Damage;
    [SerializeField] private int level;
    private float coolTime;
    void Start()
    {
        
    }
    public override void ExcutSkill(PlayerState ps, GameObject Character)
    {
        if (ps.isSkilling)
        {
            Debug.Log("IS Skilling");
            return;
        }
        if (coolTime > 0)
            return;
        StartCoroutine(Excut(ps, Character));
    }
    public override float GetCoolTime()
    {
        return coolTime;
    }

    public override int GetSkillLevel()
    {
        return level;
    }

    public override void SkillLevelUp()
    {
        if (level == 1)
        {
            level++;
        }
    }
    private IEnumerator Excut(PlayerState ps, GameObject Character)
    {
        ps.isSkilling = true;
        ps.StopHook = true;
        Debug.Log("Skill 2");
        //ĳ���� �̹����� �����ִ� ������ �ݴ���
        //Character.transform.localScale = new Vector3(-Character.transform.localScale.x, 1, 1);
        ps.SetAnimator(PlayerState.StateAni.Stab);
        //Debug.Log()

        yield return new WaitForSeconds(0.2f);
        //ĳ���� �̹����� �����ִ� ������ �ݴ���
        //Character.transform.localScale = new Vector3(-Character.transform.localScale.x, 1, 1);
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
