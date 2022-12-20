using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorList/Behavior/BehaviorSkill")]
public class BehaviorSkill : BehaviorNode
{
    public override bool OnUapdate(PlayerState ps, Rigidbody2D rigidbody, GameObject Character)
    {
        if (ps.Rope)
            return false;
        if (ps.isWallWalk)
            return false;
        if (ps.isJumping)
            return false;
        if (!ps.isGround)
            return false;
        if (ps.nowSkill == -1)
            return false;

        SkillCommand.Instance.ExcutSkill(ps.nowSkill);
        ps.nowSkill = -1;

        return false;

    }
}
