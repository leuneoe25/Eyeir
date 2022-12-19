using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorNode : ScriptableObject
{
    public abstract bool OnUapdate(PlayerState ps, Rigidbody2D rigidbody, GameObject Character);
}
