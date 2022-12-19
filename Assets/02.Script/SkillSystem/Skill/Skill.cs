using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    private float Damage;
    private int level;
    public abstract int ExcutSkill();
    public abstract int SkillLevelUp();
    
}
