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

        //if (ps.Rope)
        //    return false;
        
        
        if (!ps.isWallWalk)
            return false;

        //벽에 붙어 있는지 체크
        RaycastHit2D UpHit = Physics2D.Raycast(new Vector2(Character.transform.position.x, Character.transform.position.y + 0.5f), new Vector2(-Character.transform.localScale.x, 0), 0.55f, LayerMask.GetMask("Ground", "Wall"));
        RaycastHit2D DwonHit = Physics2D.Raycast(new Vector2(Character.transform.position.x, Character.transform.position.y - 0.5f), new Vector2(-Character.transform.localScale.x, 0), 0.55f, LayerMask.GetMask("Ground", "Wall"));
        if (UpHit.collider == null && DwonHit.collider == null)
        {
            //Debug.Log(UpHit.transform.name + " " + DwonHit.transform.name);
            ps.isWallWalk = false;
            rigidbody.gravityScale = 5;
            return false;

        }
        Character.transform.GetChild(1).GetComponent<Rope>().DeletRope();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            StopWallWalk(ps, rigidbody, Character);
            return false;
        }

        //isWallWalk = true;
        //rigidbody.gravityScale = 0;
        float y = Input.GetAxisRaw("Vertical");
        rigidbody.gravityScale = 0;
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
        RaycastHit2D[] hitEndWall;
        rigidbody.velocity = new Vector2(0, 0);
        if (y >= 1)
        {
            
            hitEndWall = Physics2D.RaycastAll(new Vector2(Character.transform.position.x, Character.transform.position.y + 0.5f), new Vector2(-Character.transform.localScale.x, 0), 5f, LayerMask.GetMask("Ground", "Wall"));
            RaycastHit2D hitup = Physics2D.Raycast(new Vector2(Character.transform.position.x, Character.transform.position.y + 0.5f), Vector2.up, 2f, LayerMask.GetMask("Ground", "Wall"));
            if(hitup.collider != null)
            {
                rigidbody.AddForce(new Vector2(Character.transform.localScale.x * 2, -0.2f) * (ps.JumpPower), ForceMode2D.Impulse);
                rigidbody.gravityScale = 5;
                ps.isWallWalk = false;
                return false;
            }
            //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1.5f), new Vector2(WallWalkPosX, 0) * 5, Color.black);
            if (hitEndWall.Length == 0  )
            {
                //대각선
                Debug.Log(null);

                StopWallWalk(ps, rigidbody, Character);
                return false;
            }
            Character.transform.position += Vector3.up * 10 * Time.deltaTime;
        }
        else if (y <= -1)
        {
            hitEndWall = Physics2D.RaycastAll(new Vector2(Character.transform.position.x, Character.transform.position.y - 0.5f), new Vector2(-Character.transform.localScale.x, 0), 5f, LayerMask.GetMask("Ground", "Wall"));
            Debug.DrawRay(new Vector2(Character.transform.position.x, Character.transform.position.y - 0.5f), new Vector2(-Character.transform.localScale.x, 0) * 5, Color.white);
            if(ps.isGround)
            {
                rigidbody.gravityScale = 5;
                ps.isWallWalk = false;
                return false;
            }
            if (hitEndWall.Length == 0)
            {
                //대각선
                //Debug.Log(Character.transform.localScale.x);
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
        if(Character.transform.localScale.x > 0)
            rigidbody.AddForce(new Vector2(Character.transform.localScale.x+2,1) * (ps.JumpPower ), ForceMode2D.Impulse);
        else
        {
            rigidbody.AddForce(new Vector2(Character.transform.localScale.x - 2, 1) * (ps.JumpPower), ForceMode2D.Impulse);

        }
        rigidbody.gravityScale = 5;
        ps.isWallWalk = false;
    }
}
