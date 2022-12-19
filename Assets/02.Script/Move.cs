using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{
    [SerializeField] private Rope ropeSystem;
    private Rigidbody2D rigidbody;
    public float JumpPower = 10f;
    public float Speed = 5f;
    public bool Rope = false;
    public bool isGround = false;
    RaycastHit2D hit;
    RaycastHit2D[] hitEndWall;

    public bool isWallWalk = false;
    public float WallXPos = 0f;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (x >= 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x <= -1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        if (Rope)
        {
            return;
        }
        else
        {
            if(isWallWalk)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    StopWallWalk();

                    return;
                }
                isWallWalk = true;
                //rigidbody.gravityScale = 0;
                float y = Input.GetAxisRaw("Vertical");
                
                rigidbody.velocity = new Vector2(0, 0);
                if (y>=1)
                {
                    hitEndWall = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(WallXPos, 0), 5f, LayerMask.GetMask("Ground", "Wall"));
                    Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1.5f), new Vector2(WallXPos, 0) * 5, Color.black);
                    if(hitEndWall.Length == 0)
                    {
                        //대각선
                        //Debug.Log(null);
                        
                        StopWallWalk();
                    }
                    transform.position += Vector3.up * 10*Time.deltaTime;
                }
                else if(y<=-1)
                {
                    hitEndWall = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(WallXPos, 0), 5f, LayerMask.GetMask("Ground", "Wall"));
                    Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(WallXPos, 0) * 5, Color.black);
                    if (hitEndWall.Length == 0)
                    {
                        //대각선
                        StopWallWalk();
                    }
                    transform.position += Vector3.down * 10 * Time.deltaTime;
                }
                
                
                return;
            }
            if (isGround)
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

            rigidbody.velocity = new Vector2(x * Speed, rigidbody.velocity.y);

            hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                isGround = true;
            }
            else
            {
                isGround = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            }
        }
    }
    public void StartWallWalk(float PosX)
    {
        isWallWalk = true;
        rigidbody.gravityScale = 0;
        WallXPos = PosX;
    }
    public void StopWallWalk()
    {
        rigidbody.AddForce(Vector2.up * (JumpPower/2), ForceMode2D.Impulse);
        rigidbody.gravityScale = 5;
        isWallWalk = false;
    }
    private void isWallWalkFalse()
    {
        isWallWalk = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Ground"))
        {
            if (Rope)
            {

            }
                
        }
    }
}
