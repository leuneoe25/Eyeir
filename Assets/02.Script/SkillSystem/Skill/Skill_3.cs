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
        throw new System.NotImplementedException();
    }

    public override int GetSkillLevel()
    {
        throw new System.NotImplementedException();
    }

    public override int SkillLevelUp()
    {
        throw new System.NotImplementedException();
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
