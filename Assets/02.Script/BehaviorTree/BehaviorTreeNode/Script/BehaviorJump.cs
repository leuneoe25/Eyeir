using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorList/Behavior/BehaviorJump")]
public class BehaviorJump : BehaviorNode
{
    public override bool OnUapdate(PlayerState ps, Rigidbody2D rigidbody, GameObject Character)
    {
        //if (ps.isGround)
        //    return false;
        if (ps.isWallWalk)
            return false;

        //move
        float x = Input.GetAxisRaw("Horizontal");
        TransformX(x, Character);

        if (ps.Rope)
            return false;

        if (!ps.isGround)
        {

            if (!ps.isWallWalk)
            {
                
                RaycastHit2D UpHit = Physics2D.Raycast(new Vector2(Character.transform.position.x, Character.transform.position.y + 0.5f), new Vector2(ps.WallWalkPosX, 0), 0.8f, LayerMask.GetMask("Ground", "Wall"));
                Debug.DrawRay(new Vector2(Character.transform.position.x, Character.transform.position.y + 0.5f), new Vector2(ps.WallWalkPosX, 0) * 0.6f, Color.blue, 2);
                RaycastHit2D DwonHit = Physics2D.Raycast(new Vector2(Character.transform.position.x, Character.transform.position.y - 0.5f), new Vector2(ps.WallWalkPosX, 0), 0.8f, LayerMask.GetMask("Ground", "Wall"));
                Debug.DrawRay(new Vector2(Character.transform.position.x, Character.transform.position.y - 0.5f), new Vector2(ps.WallWalkPosX, 0) * 0.6f, Color.blue, 2);
                Debug.Log("C");
                if (UpHit.collider != null && DwonHit.collider != null)
                {
                    Debug.Log(UpHit.transform.name + " " + DwonHit.transform.name);
                    ps.isWallWalk = true;
                    
                }
            }

        }



        if (ps.isGround)
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

        rigidbody.velocity = new Vector2(x * ps.Speed, rigidbody.velocity.y);

        //Jump
        float JumpPower = 20;
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
            rigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }

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
