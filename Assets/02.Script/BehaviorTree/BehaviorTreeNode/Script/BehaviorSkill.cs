using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorList/Behavior/BehaviorSkill")]
public class BehaviorSkill : BehaviorNode
{
    public override bool OnUapdate(PlayerState ps, Rigidbody2D rigidbody, GameObject Character)
    {
        return false;
    }
}
