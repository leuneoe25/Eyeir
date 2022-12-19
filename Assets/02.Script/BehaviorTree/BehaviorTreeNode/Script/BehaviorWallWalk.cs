using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorList/Behavior/BehaviorWallWalk")]
public class BehaviorWallWalk : BehaviorNode
{
    public override bool OnUapdate(PlayerState ps, Rigidbody2D rigidbody, GameObject Character)
    {
        //if (ps.isGround)
        //    return false;
        //닿아 있나


        Debug.Log(ps.Rope);
        if (ps.Rope)
            return false;
        
        
        if (!ps.isWallWalk)
            return false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            StopWallWalk(ps, rigidbody, Character);
            return false;
        }

        //isWallWalk = true;
        //rigidbody.gravityScale = 0;
        float y = Input.GetAxisRaw("Vertical");
        rigidbody.gravityScale = 0;
        RaycastHit2D[] hitEndWall;
        rigidbody.velocity = new Vector2(0, 0);
        if (y >= 1)
        {
            hitEndWall = Physics2D.RaycastAll(new Vector2(Character.transform.position.x, Character.transform.position.y + 0.5f), new Vector2(ps.WallWalkPosX, 0), 5f, LayerMask.GetMask("Ground", "Wall"));
            //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1.5f), new Vector2(WallWalkPosX, 0) * 5, Color.black);
            if (hitEndWall.Length == 0)
            {
                //대각선
                //Debug.Log(null);

                StopWallWalk(ps, rigidbody, Character);
                return false;
            }
            Character.transform.position += Vector3.up * 10 * Time.deltaTime;
        }
        else if (y <= -1)
        {
            hitEndWall = Physics2D.RaycastAll(new Vector2(Character.transform.position.x, Character.transform.position.y - 0.5f), new Vector2(ps.WallWalkPosX, 0), 5f, LayerMask.GetMask("Ground", "Wall"));
            //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(ps.WallWalkPosX, 0) * 5, Color.black);
            if (hitEndWall.Length == 0)
            {
                //대각선
                StopWallWalk(ps,rigidbody,Character);
                return false;
            }
            Character.transform.position += Vector3.down * 10 * Time.deltaTime;
        }


        return true;
    }

    //public void StartWallWalk(float PosX)
    //{
    //    isWallWalk = true;
    //    rigidbody.gravityScale = 0;
    //    WallWalkPosX = PosX;
    //}
    public void StopWallWalk(PlayerState ps, Rigidbody2D rigidbody, GameObject Character)
    {
        rigidbody.AddForce(Vector2.up * (ps.JumpPower ), ForceMode2D.Impulse);
        rigidbody.gravityScale = 5;
        ps.isWallWalk = false;
    }
}
