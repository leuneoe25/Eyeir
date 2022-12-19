using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3 : Skill
{
    [SerializeField] private float Damage;
    [SerializeField] private int level;
    private float coolTime;
    void Start()
    {
        
    }
    public override void ExcutSkill()
    {
        if (coolTime > 0)
            return;
        Debug.Log("Skill 3");
        coolTime = 20f;
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



    // Update is called once per frame
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
