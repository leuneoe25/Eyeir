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
    //�ʱ� �߽� : ȸ�� �Ǵ� ����
    [Range(0, 360), Tooltip("������ �� ȸ���� �� �� ����")]
    public float rot = 0f;

    [Range(3, 7), Tooltip("������ ����� ������� ������ ���ϴ� ��")] //->��~ĥ������ �׳��� �̻� �� �̻����� ���� ������ ����..
    public int Vertex = 3;

    [Range(1, 5), Tooltip("�� ���� �����Ͽ� �ձ� ����, ������ �������� ǥ�� ��")]
    public float sup = 3;
    //���ǵ�
    public float Speed = 3;//speed
    //��Ÿ ������
    int m;
    float a;
    float phi;
    List<float> v = new List<float>();
    List<float> xx = new List<float>();

    public GameObject bullet;
    [Header("Pattern 3")]
    //�Ѿ��� ������ Target���� ���ư� ����
    public Transform target;

    //�߻�� �Ѿ� ������Ʈ
    public GameObject bullet_3;
    private void Awake()
    {
        //��� �����͸� �ʱ�ȭ �Ѵ�.
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
    #region ź��
    void ShapeInit()
    {
        //��ҵ��� ��� ���� �� ������ �ʱ�ȭ �ϱ����� Clear�Ѵ�.
        v.Clear();
        xx.Clear();

        //������ �ʱ�ȭ
        m = (int)Mathf.Floor(sup / 2);
        a = 2 * Mathf.Sin(Mathf.PI / Vertex);
        phi = ((Mathf.PI / 2f) * (Vertex - 2f)) / Vertex;
        v.Add(0);
        xx.Add(0);

        for (int i = 1; i <= m; i++)
        {
            //list.Insert(��ġ,���) -> �ش� ��ġ�� ���� ����ֽ��ϴ�.
            v.Add(Mathf.Sqrt(sup * sup - 2 * a * Mathf.Cos(phi) * i * sup + a * a * i * i));
        }

        for (int i = 1; i <= m; i++)
        {
            xx.Add(Mathf.Rad2Deg * (Mathf.Asin(a * Mathf.Sin(phi) * i / v[i])));
        }
    }
    void shot()
    {
        
        //rot���� ������ ���� �ʵ��� ������ dir���� �����Ͽ���.
        var dir = rot;

        //������ �� ��ŭ ����
        for (int r = 0; r < Vertex; r++)
        {
            for (int i = 1; i <= m; i++)
            {
                #region //1�� ����
                //�Ѿ� ����
                GameObject idx1 = Instantiate(bullet);

                //2���� ����
                //Destroy(idx1, 5f);

                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                idx1.transform.position = gameObject.transform.position;

                //������ ȸ�� ó���� ����� ����� ����.
                idx1.transform.rotation = Quaternion.Euler(0, 0, dir + xx[i]);

                //������ �ӵ� ó���� ����� ����� ����.
                idx1.GetComponent<Bullet_Move>().speed = v[i] * Speed / sup;
                #endregion

                #region //2�� ����
                //�Ѿ� ����
                GameObject idx2 = Instantiate(bullet);

                //2���� ����
                //Destroy(idx2, 5f);

                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                idx2.transform.position = gameObject.transform.position;

                //������ ȸ�� ó���� ����� ����� ����.
                idx2.transform.rotation = Quaternion.Euler(0, 0, dir - xx[i]);

                //������ �ӵ� ó���� ����� ����� ����.
                idx2.GetComponent<Bullet_Move>().speed = v[i] * Speed / sup;
                #endregion

                #region //3�� ����
                //�Ѿ� ����
                GameObject idx3 = Instantiate(bullet);

                //2���� ����
                //Destroy(idx3, 5f);

                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                idx3.transform.position = gameObject.transform.position;

                //������ ȸ�� ó���� ����� ����� ����.
                idx3.transform.rotation = Quaternion.Euler(0, 0, dir);

                //������ �ӵ� ó���� ����� ����� ����.
                idx3.GetComponent<Bullet_Move>().speed = Speed;
                #endregion

                //����� �ϼ��Ѵ�.
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
            //���� �� 
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

        yield return new WaitForSeconds(5f);
        isPattern = false;
    }
    IEnumerator Pattern_3()
    {
        isPattern = true;
        //��� �����
        Targetshot();
        yield return new WaitForSeconds(3f);
        isPattern = false;
    }
    void Targetshot()
    {
        //Target�������� �߻�� ������Ʈ ����
        var bl = new List<Transform>();

        for (int i = 0; i < 360; i += 13)
        {
            //�Ѿ� ����
            var temp = Instantiate(bullet_3);

            //2���� ����
            Destroy(temp, 2f);

            //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
            temp.transform.position = gameObject.transform.position;

            //?���Ŀ� Target���� ���ư� ������Ʈ ����
            bl.Add(temp.transform);

            //Z�� ���� ���ؾ� ȸ���� �̷�����Ƿ�, Z�� i�� �����Ѵ�.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }
        //�Ѿ��� Target �������� �̵���Ų��.
        StartCoroutine(BulletToTarget(bl));
    }
    IEnumerator BulletToTarget(List<Transform> bl)
    {
        //0.5�� �Ŀ� ����
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < bl.Count; i++)
        {
            if (bl[i] == null)
                continue;
            //���� �Ѿ��� ��ġ���� �÷����� ��ġ�� ���Ͱ��� �y���Ͽ� ������ ����
            var target_dir = target.transform.position - bl[i].position;

            //x,y�� ���� �����Ͽ� Z���� ������ ������. -> ~�� ������ ����
            var angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;

            //Target �������� �̵�
            bl[i].rotation = Quaternion.Euler(0, 0, angle);
        }

        //������ ����
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
