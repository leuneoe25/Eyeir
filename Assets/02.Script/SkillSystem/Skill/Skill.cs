using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    private float Damage;
    private int level;
    private float coolTime;
    public abstract int ExcutSkill();
    public abstract int SkillLevelUp();

    
}
