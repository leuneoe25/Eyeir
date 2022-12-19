using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorList/Behavior/BehaviorMove")]
public class BehaviorMove : BehaviorNode
{
    public override bool OnUapdate(PlayerState ps, Rigidbody2D rigidbody, GameObject Character)
    {
        //if (ps.isGround)
        //    return false;
        if (ps.isWallWalk)
            return false;

        RaycastHit2D hit;

        hit = Physics2D.Raycast(Character.transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            ps.isGround = true;
        }
        else
        {
            ps.isGround = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ps.isJumping = true;
        }

        //move
        float x = Input.GetAxisRaw("Horizontal");
        TransformX(x, Character);

        if (ps.Rope)
            return false;

        if (ps.isGround)
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

        rigidbody.velocity = new Vector2(x * ps.Speed, rigidbody.velocity.y);
            
        return true;
    }
    private void TransformX(float x, GameObject Character)
    {
        
        if (x >= 1)
        {
            Character.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x <= -1)
        {
            Character.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
