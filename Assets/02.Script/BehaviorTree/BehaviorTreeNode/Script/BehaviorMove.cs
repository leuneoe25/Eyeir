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


        if (!ps.isGround)
        {

            if (!ps.isWallWalk)
            {

                RaycastHit2D UpHit = Physics2D.Raycast(new Vector2(Character.transform.position.x, Character.transform.position.y + 0.5f), new Vector2(-Character.transform.localScale.x, 0), 0.55f, LayerMask.GetMask("Ground", "Wall"));
                Debug.DrawRay(new Vector2(Character.transform.position.x, Character.transform.position.y + 0.5f), new Vector2(ps.WallWalkPosX, 0) * 0.6f, Color.blue, 2);
                RaycastHit2D DwonHit = Physics2D.Raycast(new Vector2(Character.transform.position.x, Character.transform.position.y - 0.5f), new Vector2(-Character.transform.localScale.x, 0), 0.55f, LayerMask.GetMask("Ground", "Wall"));
                Debug.DrawRay(new Vector2(Character.transform.position.x, Character.transform.position.y - 0.5f), new Vector2(ps.WallWalkPosX, 0) * 0.6f, Color.blue, 2);
                if (UpHit.collider != null && DwonHit.collider != null)
                {
                    //Debug.Log(UpHit.transform.name + " " + DwonHit.transform.name);
                    ps.isWallWalk = true;

                }
            }

        }


        RaycastHit2D hit;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ps.isJumping = true;
            return false;
        }
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
