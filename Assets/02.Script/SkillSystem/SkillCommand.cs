using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand : MonoBehaviour
{
    [SerializeField] private Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
    [Header("Skill")]
    [SerializeField] private Skill s_1;
    [SerializeField] private Skill s_2;
    [SerializeField] private Skill s_3;
    private void Start()
    {
        skills.Add(0, s_1);
        skills.Add(1, s_2);
        skills.Add(2, s_3);
    }

    public void ExcutSkill(int index)
    {
        skills[index].ExcutSkill();
    }
    public void GetSkillCoolTime(int index)
    {
        skills[index].GetCoolTime();
    }
}
