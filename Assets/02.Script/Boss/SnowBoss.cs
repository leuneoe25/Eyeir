using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowBoss : Boss
{
    [SerializeField] private BossStat bs;
    [SerializeField] private Image Hpbar;
    private int Stat = -1;
    private bool isPattern = false;
    private bool StartPattern = true;
    [Header("Pattern 1")]
    [SerializeField] private GameObject[] AttackArea_1;
    [SerializeField] private GameObject[] Attack_1;
    

    Coroutine nowPattern = null;

    [Header("Pattern 2")]
    [SerializeField] private GameObject AttackArea_2;
    //초기 중심 : 회전 되는 방향
    [Range(0, 360), Tooltip("퍼지기 전 회전을 줄 수 있음")]
    public float rot = 0f;

    [Range(3, 7), Tooltip("퍼지는 모양이 몇각형으로 퍼질지 정하는 것")] //->삼~칠각형이 그나마 이쁨 그 이상으로 가면 원으로 보임..
    public int Vertex = 3;

    [Range(1, 5), Tooltip("이 값을 조정하여 둥근 느낌, 납작한 느낌으로 표현 됨")]
    public float sup = 3;
    //스피드
    public float Speed = 3;//speed
    //기타 데이터
    int m;
    float a;
    float phi;
    List<float> v = new List<float>();
    List<float> xx = new List<float>();

    public GameObject bullet;
    [Header("Pattern 3")]
    //총알을 생성후 Target에게 날아갈 변수
    public Transform target;

    //발사될 총알 오브젝트
    public GameObject bullet_3;
    private void Awake()
    {
        //모양 데이터를 초기화 한다.
        ShapeInit();
    }

    void Update()
    {
        //if (!isPattern)
        //    StartCoroutine(Pattern_2());
        if(bs.BossPattern)
        {
            if (isPattern)
                return;
            if (StartPattern)
            {
                StartCoroutine(PtternStart());
                return;
            }
            RandomPattern();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            shot();
        }
    }
    private void RandomPattern()
    {
        
        int s = Random.Range(0, 4);
        while(Stat == s)
        {
            s = Random.Range(0, 4);
        }
        switch(s)
        {
            case 0:
                nowPattern = StartCoroutine(Pattern_1());
                break;
            case 1:
                nowPattern = StartCoroutine(Pattern_2());
                break;
            case 2:
                nowPattern = StartCoroutine(Pattern_3());
                break;
        }
        Debug.Log("Stat : " +s);
    }
    IEnumerator PtternStart()
    {
        bs.Player.GetComponent<PlayerState>().isStop = true;
        bs.Player.GetComponent<PlayerState>().StopHook = true;
        Debug.Log("Start");
        isPattern = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        StartCoroutine(HpbarAni());
        yield return new WaitForSeconds(1f);
        StartCoroutine(PadeBoss());
    }
    #region Start
    IEnumerator HpbarAni()
    {
        Hpbar.fillAmount = 0;
        while (Hpbar.fillAmount < 1)
        {
            Hpbar.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator PadeBoss()
    {
        while (gameObject.GetComponent<SpriteRenderer>().color.a < 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        bs.Player.GetComponent<PlayerState>().isStop = false;
        bs.Player.GetComponent<PlayerState>().StopHook = false;
        StartPattern = false;
        isPattern = false;
        yield return new WaitForSeconds(3f);
    }
    #endregion
    #region 탄막
    void ShapeInit()
    {
        //요소들이 들어 있을 수 있으니 초기화 하기전에 Clear한다.
        v.Clear();
        xx.Clear();

        //데이터 초기화
        m = (int)Mathf.Floor(sup / 2);
        a = 2 * Mathf.Sin(Mathf.PI / Vertex);
        phi = ((Mathf.PI / 2f) * (Vertex - 2f)) / Vertex;
        v.Add(0);
        xx.Add(0);

        for (int i = 1; i <= m; i++)
        {
            //list.Insert(위치,요소) -> 해당 위치에 값을 집어넣습니다.
            v.Add(Mathf.Sqrt(sup * sup - 2 * a * Mathf.Cos(phi) * i * sup + a * a * i * i));
        }

        for (int i = 1; i <= m; i++)
        {
            xx.Add(Mathf.Rad2Deg * (Mathf.Asin(a * Mathf.Sin(phi) * i / v[i])));
        }
    }
    void shot()
    {
        
        //rot값에 영향을 주지 않도록 별도로 dir값을 선언하였다.
        var dir = rot;

        //꼭짓점 수 만큼 실행
        for (int r = 0; r < Vertex; r++)
        {
            for (int i = 1; i <= m; i++)
            {
                #region //1차 생성
                //총알 생성
                GameObject idx1 = Instantiate(bullet);

                //2초후 삭제
                //Destroy(idx1, 5f);

                //총알 생성 위치를 (0,0) 좌표로 한다.
                idx1.transform.position = gameObject.transform.position;

                //정밀한 회전 처리로 모양을 만들어 낸다.
                idx1.transform.rotation = Quaternion.Euler(0, 0, dir + xx[i]);

                //정밀한 속도 처리로 모양을 만들어 낸다.
                idx1.GetComponent<Bullet_Move>().speed = v[i] * Speed / sup;
                #endregion

                #region //2차 생성
                //총알 생성
                GameObject idx2 = Instantiate(bullet);

                //2초후 삭제
                //Destroy(idx2, 5f);

                //총알 생성 위치를 (0,0) 좌표로 한다.
                idx2.transform.position = gameObject.transform.position;

                //정밀한 회전 처리로 모양을 만들어 낸다.
                idx2.transform.rotation = Quaternion.Euler(0, 0, dir - xx[i]);

                //정밀한 속도 처리로 모양을 만들어 낸다.
                idx2.GetComponent<Bullet_Move>().speed = v[i] * Speed / sup;
                #endregion

                #region //3차 생성
                //총알 생성
                GameObject idx3 = Instantiate(bullet);

                //2초후 삭제
                //Destroy(idx3, 5f);

                //총알 생성 위치를 (0,0) 좌표로 한다.
                idx3.transform.position = gameObject.transform.position;

                //정밀한 회전 처리로 모양을 만들어 낸다.
                idx3.transform.rotation = Quaternion.Euler(0, 0, dir);

                //정밀한 속도 처리로 모양을 만들어 낸다.
                idx3.GetComponent<Bullet_Move>().speed = Speed;
                #endregion

                //모양을 완성한다.
                dir += 360 / Vertex;
            }
        }
    }
    #endregion
    IEnumerator Pattern_1()
    {
        isPattern = true;
        for(int i=0;i<4;i++)
        {
            AttackArea_1[i].SetActive(true);
            yield return new WaitForSeconds(2f);
            AttackArea_1[i].SetActive(false);
            Attack_1[i].SetActive(true);
            yield return new WaitForSeconds(1f);
            Attack_1[i].SetActive(false);
            //공격 후 
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(5f);
        isPattern = false;
    }
    IEnumerator Pattern_2()
    {
        isPattern = true;
        AttackArea_2.SetActive(true);
        yield return new WaitForSeconds(1f);
        AttackArea_2.SetActive(false);
        
        rot = 210;
        shot();
        rot = 145;
        shot();

        yield return new WaitForSeconds(10f);
        isPattern = false;
    }
    IEnumerator Pattern_3()
    {
        isPattern = true;
        Targetshot();
        yield return new WaitForSeconds(3f);
        isPattern = false;
    }
    void Targetshot()
    {
        //Target방향으로 발사될 오브젝트 수록
        var bl = new List<Transform>();

        for (int i = 0; i < 360; i += 13)
        {
            //총알 생성
            var temp = Instantiate(bullet_3);

            //2초후 삭제
            Destroy(temp, 2f);

            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = gameObject.transform.position;

            //?초후에 Target에게 날아갈 오브젝트 수록
            bl.Add(temp.transform);

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }
        //총알을 Target 방향으로 이동시킨다.
        StartCoroutine(BulletToTarget(bl));
    }
    IEnumerator BulletToTarget(List<Transform> bl)
    {
        //0.5초 후에 시작
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < bl.Count; i++)
        {
            if (bl[i] == null)
                continue;
            //현재 총알의 위치에서 플레이의 위치의 벡터값을 뻴셈하여 방향을 구함
            var target_dir = target.transform.position - bl[i].position;

            //x,y의 값을 조합하여 Z방향 값으로 변형함. -> ~도 단위로 변형
            var angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;

            //Target 방향으로 이동
            bl[i].rotation = Quaternion.Euler(0, 0, angle);
        }

        //데이터 해제
        bl.Clear();
    }

    public override void Clear()
    {
        StopCoroutine(nowPattern);

        for(int i= 0;i<4;i++)
        {
            AttackArea_1[i].SetActive(false);
        }
        AttackArea_2.SetActive(false);
    }
}
