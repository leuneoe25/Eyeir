using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Rope : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject Player;
    public CinemachineImpulseSource impulseSource;

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen = 0.35f;
    private int segmentLength = 35;
    private float lineWidth = 0.1f;

    private RaycastHit2D hit;


    public float circleR; //반지름
    public float deg = 180; //각도
    public float objSpeed; //원운동 속도



    float angle;//플레이어와의 타겟과 각도
    bool PMotion = false;//pendulum motion 진자운동
    float StartPMotionAngle = 0;//pendulum motion 진자운동의 기준 각
    float PMotionAngle = 0;//pendulum motion 진자운동의 기준 각

    private Coroutine cutDown;
    private bool isCutDown = false;

    private bool isBooster = false;
    //속도
    private Vector3 oldPosition;
    private Vector3 currentPosition;
    private Vector3 velocityDir;
    private float velocity;


    private bool isRope = false;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Player.GetComponent<PlayerState>().isWallWalk)
            return;
        
        Vector3 ropeStartPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (!Player.GetComponent<PlayerState>().isWallWalk)
        {
            float wallx = 1;
            RaycastHit2D hitPlayerLookAt = Physics2D.Raycast(Player.transform.position, Vector2.right, 0.2f, LayerMask.GetMask("Ground", "Wall"));
            if (PMotion)
                Debug.DrawRay(Player.transform.position, Vector2.right * 0.2f, Color.red);
            if (!PMotion)
            {
                hitPlayerLookAt = Physics2D.Raycast(Player.transform.position, Vector2.left, 0.2f, LayerMask.GetMask("Ground", "Wall"));
                Debug.DrawRay(Player.transform.position, Vector2.left * 0.2f, Color.red);
                wallx = -1;
            }
            if (hitPlayerLookAt.collider != null)
            {
                Debug.Log(wallx);
                Player.GetComponent<PlayerState>().StartWallWalk(wallx);
                if (lineRenderer.positionCount > 0)
                    DeletRope();

                return;
            }
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            isCutDown = false;
            isBooster = false;
            isRope = true;
            //위치 설정
            if (hit.collider == null)
            {
                return;
            }
            else
            {
                //각
                Vector2 dir = new Vector2(Player.transform.position.x - hit.point.x, Player.transform.position.y - hit.point.y);
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                angle = Mathf.Abs(angle);
                //Debug.Log(angle + ", " + (90 + angle));
                deg = 90 + angle;
                if ((angle + 90) > 180)
                {
                    PMotion = true;
                    PMotionAngle = 45;
                    StartPMotionAngle = 180 - PMotionAngle;
                }
                else if ((angle + 90) < 180)
                {
                    PMotion = false;
                    PMotionAngle = 45;
                    StartPMotionAngle = 180 - PMotionAngle;
                }



                ropeSegments.Add(new RopeSegment(hit.point));
                ropeSegments.Add(new RopeSegment(Player.transform.position));
                Player.GetComponent<Rigidbody2D>().gravityScale = 0;
                impulseSource.GenerateImpulse(0.5f);
                //현재 위치 확인(속도)
                oldPosition = Player.transform.position;
                Player.GetComponent<PlayerState>().Rope = true;
                circleR = Vector2.Distance(Player.transform.position, hit.point);
                //180도로만 하니 벽뚫이 생김=>양쪽으로 +-45를 하여 체크
                RaycastHit2D[] hit2D;
                string[] s = new string[2] { "Ground", "Wall" };
                hit2D = Physics2D.RaycastAll(hit.point, Vector2.down, (circleR+1), LayerMask.GetMask(s));
                Debug.DrawRay(hit.point, Vector2.down * (circleR + 1), Color.red, 0.5f);
                if (hit2D.Length > 0)
                {
                    for(int i= 0;i<hit2D.Length;i++)
                    {
                        if(hit2D[i].collider != hit.collider)
                        {
                            //Debug.Log("Coollllllll name : " + hit2D[i].transform.name);

                            //반지름 줄이기
                            if(Player.GetComponent<PlayerState>().isGround)
                            {
                                if(hit2D.Length>=2)
                                {
                                    cutDown = GroundCircleRCutDown(Vector2.Distance(hit.point, hit2D[1].point) - 2f, hit2D[1].point);
                                    return;
                                }
                                cutDown = GroundCircleRCutDown(Vector2.Distance(hit.point, hit2D[i].point) - 2f, hit2D[i].point);
                                Debug.Log(hit2D.Length);
                                return;
                            }
                            else
                            {
                                //+45
                                //RayCast
                                //if(hit!=null)
                                //hit로 이동한 뒤 hit에서 원 운동
                                //-45




                                //부딪히지않음(공중에서)
                                cutDown = SkyCircleRCutDown(Vector2.Distance(hit.point, hit2D[i].point) - 2f, hit2D[i].point);
                                //circleR = Vector2.Distance(hit.point, hit2D[i].point) - 2f;
                            }
                                
                            //circleR = Vector2.Distance(hit.point, hit2D[i].point) - 0.5f;
                        }
                    }
                    
                }
                
                //가속
                objSpeed += velocity*2;
            }
        }
        else if(Input.GetMouseButton(0) && isRope)
        {   
            if (hit.collider == null)
                return;
            if (Player.transform.position.y > hit.point.y)//앵커가 내 아래에 있을 시 작동 x
            {
                //임시로 플레이어의 위치에 마지막지점 고정
                //선 그리기
                if(ropeSegments.Count>=2)
                    ropeSegments[1] = new RopeSegment(Player.transform.position);
                DrawRope();
                return;
            }
            if(!isCutDown)
            {
                
                


                Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (PMotion)//생략화 가능
                {
                    deg -= Time.deltaTime * objSpeed;
                }
                else if (!PMotion)
                {
                    deg += Time.deltaTime * objSpeed;
                }
                if (deg < 360)
                {
                    var rad = Mathf.Deg2Rad * (deg);
                    var x = circleR * Mathf.Sin(rad);
                    var y = circleR * Mathf.Cos(rad);
                    Player.transform.position = hit.point + new Vector2(x, y);
                    if(!isBooster)
                    {
                        if ((angle + 90) > 180) // 왼쪽일때
                        {
                            if (deg > ((PMotionAngle * 2) + StartPMotionAngle))
                            {
                                PMotion = true;
                            }
                            else if (deg < StartPMotionAngle)
                            {
                                PMotion = false;
                                //Debug.Log(StartPMotionAngle);
                            }

                        }
                        else if ((angle + 90) < 180) // 오른쪽인때
                        {
                            if (deg > ((PMotionAngle * 2) + StartPMotionAngle))
                            {
                                PMotion = true;
                            }
                            else if (deg < StartPMotionAngle)
                            {
                                PMotion = false;
                            }
                        }
                    }
                    

                }
                else // 없어도 ㄱㅊ?
                {
                    deg = 0;
                }


                //angle 값 조정 + 반대로 움직일시 구현
                if (Input.GetKey(KeyCode.A))
                {
                    deg += 30 * Time.deltaTime;
                    PMotionAngle += 10 * Time.deltaTime;
                    StartPMotionAngle -= 10 * Time.deltaTime;
                    objSpeed += 20 * Time.deltaTime;
                    //if ((angle + 90) < 180)
                    //{
                    //    PMotionAngle += 10 * Time.deltaTime;
                    //    StartPMotionAngle -= 10 * Time.deltaTime;
                    //    objSpeed += 10 * Time.deltaTime;

                    //}

                    if (PMotionAngle > 80)
                        PMotionAngle = 80;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    deg -= 30 * Time.deltaTime;
                    PMotionAngle += 10 * Time.deltaTime;
                    StartPMotionAngle -= 10 * Time.deltaTime;
                    objSpeed += 20 * Time.deltaTime;
                    //if ((angle + 90) < 180)
                    //{
                    //    StartPMotionAngle -= 10 * Time.deltaTime;
                    //    PMotionAngle += 10 * Time.deltaTime;
                    //    objSpeed += 10 * Time.deltaTime;
                    //}

                    if (PMotionAngle > 80)
                        PMotionAngle = 80;
                    if (StartPMotionAngle < 100)
                        StartPMotionAngle = 100;
                }
                RaycastHit2D hitUP = Physics2D.Raycast(Player.transform.position, Vector2.up, 2, LayerMask.GetMask("Ground", "Wall"));
                if (hitUP.collider == null)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {


                        isBooster = true;
                        Player.transform.GetChild(0).gameObject.SetActive(true);
                        if (PMotion)//생략화 가능
                        {
                            deg -= Time.deltaTime * 150;
                        }
                        else if (!PMotion)
                        {
                            deg += Time.deltaTime * 150;
                        }
                    }
                    else
                    {
                        isBooster = false;
                        Player.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                else
                {
                    isBooster = false;
                    Player.transform.GetChild(0).gameObject.SetActive(false);
                }


                if (objSpeed>150)
                {
                    objSpeed = 150;
                }
            }


            //선 그리기
            if (ropeSegments.Count >= 2)
                ropeSegments[1] = new RopeSegment(Player.transform.position);
            DrawRope();

            

        }
        else
        {
            if(lineRenderer.positionCount > 0)
                DeletRope();

            string[] s = new string[2] { "Ground", "Wall" };
            hit = Physics2D.Raycast(Player.transform.position, ((Vector2)ropeStartPoint - (Vector2)Player.transform.position).normalized, 20f,LayerMask.GetMask(s));
            Debug.DrawRay(Player.transform.position, ((Vector2)ropeStartPoint - (Vector2)Player.transform.position).normalized * 20, Color.green, 0.1f);
            if (hit.collider != null)
            {

            }
        }
        //속도
        GetPlayerVelocity();
    }
    #region 생략
    //private void FixedUpdate()
    //{
    //    Simulate();
    //}
    private void Simulate()
    {
        Vector2 forceGravity = new Vector2(0, -1);
        for(int i= 0;i<segmentLength;i++)
        {
            RopeSegment firstSegment = ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.deltaTime;
            ropeSegments[i] = firstSegment;

        }

        for(int i = 0;i<50;i++)
        {
            ApplyConstraint();
        }
    }
    private void ApplyConstraint()
    {
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        ropeSegments[0] = firstSegment;

        for(int i = 0;i <segmentLength-1;i++)
        {
            RopeSegment firstSeg = ropeSegments[i];
            RopeSegment secondSeg = ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = dist - ropeSegLen;
            Vector2 changeDir = Vector2.zero;

            if(dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if(dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }
            Vector2 changeAmount = changeDir * error;
            if (i!=0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                ropeSegments[i + 1] = secondSeg;
            }
        }
    }
    #endregion
    private void DrawRope()
    {

        //라인 두께
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[ropeSegments.Count];
        for(int i = 0;i< ropeSegments.Count; i++)
        {
            ropePositions[i] = ropeSegments[i].posNow;
        }
        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }
    private void DeletRope()
    {
        Debug.Log("Delet");
        //부스터 끝내기
        isBooster = false;
        Player.transform.GetChild(0).gameObject.SetActive(false);
        isRope = false;

        if (velocityDir.normalized.y > 0)
        {
            if (velocity > 20)
                velocity = 20;
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector3(velocityDir.normalized.x, velocityDir.normalized.y) * (velocity * 1.3f), ForceMode2D.Impulse);
        }
        //else
        //    Debug.Log("---");
        Debug.DrawRay(Player.transform.position, velocityDir.normalized*3,Color.yellow,1f);
        //Debug.Log(velocity);
        //Debug.Log(new Vector3(velocityDir.normalized.x * 30, Mathf.Abs(velocityDir.normalized.y) * 30));
        if(cutDown!=null)
        {
            StopCoroutine(cutDown);
        }
        
        objSpeed = 90;
        lineRenderer.positionCount = 0;
        ropeSegments.Clear();
        Player.GetComponent<Rigidbody2D>().gravityScale = 5;
        Player.GetComponent<PlayerState>().Rope = false;
    }
    private void GetPlayerVelocity()
    {
        currentPosition = Player.transform.position;
        var dis = (currentPosition - oldPosition);
        var distance = Mathf.Sqrt(Mathf.Pow(dis.x, 2) + Mathf.Pow(dis.y, 2) + Mathf.Pow(dis.z, 2));
        velocity = distance / Time.deltaTime;
        velocityDir = currentPosition - oldPosition;
        //velocityDir = new Vector3(0.5f,1f,0);
        oldPosition = currentPosition;
        //Debug.Log(velocity);
    }
    private Coroutine GroundCircleRCutDown(float r,Vector3 pos)
    {
        
        return StartCoroutine(GroundCircleCutDown(r, pos));
    }
    private Coroutine SkyCircleRCutDown(float r, Vector3 pos)
    {
        return StartCoroutine(SkyCircleCutDown(r, pos));
    }
    IEnumerator GroundCircleCutDown(float r,Vector3 pos)
    {
        isCutDown = true;
        float Speed = 30;
        float x = 0;
        if (pos.x > Player.transform.position.x)
            x = 1;
        else
        {
            x = -1;
        }
        yield return new WaitForSeconds(0.1f);
        while (Mathf.Abs(Player.transform.position.x - pos.x) > 0.5f)
        {
            Player.transform.position += new Vector3(x,0,0) * Speed * Time.deltaTime;
            //if(Mathf.Abs(Player.transform.position.x) > Mathf.Abs(pos.x))
            //    Player.transform.position -= Vector3.right * Speed * Time.deltaTime;
            //else
            //    Player.transform.position += Vector3.right * Speed * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        cutDown = null;
        isCutDown = false;
        circleR = r;
        deg = 180;
        //Player.transform.position += Vector3.up;
    }
    IEnumerator SkyCircleCutDown(float r, Vector3 pos)
    {
        isCutDown = true;
        yield return new WaitForSeconds(0.1f);


        //Debug.Log(v);
        var rad = Mathf.Deg2Rad * (180);
        var x = r * Mathf.Sin(rad);
        var y = r * Mathf.Cos(rad);
        Vector3 v = hit.point + new Vector2(x, y);
        Vector3 dir = v - Player.transform.position;

        float Speed = 20;
        
        Debug.DrawRay(Player.transform.position, v, Color.black, 2f);
        while (Vector3.Distance( Player.transform.position, v) > 0.5f)
        {
            if (PMotion)//생략화 가능
            {
                deg -= Time.deltaTime * objSpeed;
            }
            else if (!PMotion)
            {
                deg += Time.deltaTime * objSpeed;
            }
            rad = Mathf.Deg2Rad * (deg);
            x = r * Mathf.Sin(rad);
            y = r * Mathf.Cos(rad);
            v = hit.point + new Vector2(x, y);
            dir = v - Player.transform.position;
            //Debug.Log(Vector3.Distance(Player.transform.position, v));
            //if (Mathf.Abs(Player.transform.position.x) > Mathf.Abs(pos.x))
            //    Player.transform.position -= Vector3.right * Speed * Time.deltaTime;
            //else
            dir = v - Player.transform.position;
            Player.transform.position += dir.normalized * Speed * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        cutDown = null;
        isCutDown = false;
        circleR = r;
        //deg = 180;
        //Player.transform.position += Vector3.up;
    }
    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;
        public RopeSegment(Vector2 pos)
        {
            posNow = pos;
            posOld = pos;
        }
    }
}
