using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTable
{
    public static int GetGoogleSheetGID() { return 522400766; }

    public int ID;
    public string Name;
    public string explanation;
    public float CoolTime;
    public int Damage;
    public int UpgradeDamage;
    public int Buy;
    public int ButPrice;
    public int Start;

    // 초기화를 원하는 모든 변수를 스트링으로 받는 생성자 필요 ( TableWWW의 GetInstance() 에서 사용 )
    public SkillTable(string _1, string _2, string _3, string _4, string _5, string _6, string _7, string _8, string _9)
    {
        ID = int.Parse(_1);
        Name = _2;
        explanation = _3;
        CoolTime = float.Parse(_4);
        Damage = int.Parse(_5);
        UpgradeDamage = int.Parse(_6);
        Buy = int.Parse(_7);
        ButPrice = int.Parse(_8);
        Start = int.Parse(_9);

        // bool b    = (int.Parse( 인자 ) == 0) ? false : true;
        // int n    = int.Parse( 인자 );
        // float f    = float.Parse(인자);
        // string s    = 인자;
        // 정의한 enum eTest
        // eTest eAt = (eTest)System.Enum.Parse(typeof(eTest), name);
    }
}
public class SkillCommand : MonoBehaviour
{
    [SerializeField] private Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
    [Header("Skill")]
    [SerializeField] private Skill s_1;
    [SerializeField] private Skill s_2;
    [SerializeField] private Skill s_3;
    #region Singleton
    private static SkillCommand instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static SkillCommand Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    #region INSPECTOR

    public TableSheet val = null;
    public Text m_txtLoading = null;
    public List<Sprite> SkillIcon = new List<Sprite>();

    Dictionary<int, SkillTable> m_mapTb = new Dictionary<int, SkillTable>();

    #endregion
    [SerializeField] private GameObject StabEffect;
    [SerializeField] private GameObject CutEffect;
    [SerializeField] private GameObject ForstEffect;

    [SerializeField] private Image[] SkillImageFrame;
    private int[] Skillindex = new int[3] { -1,-1,-1};
    private int SkillCount = 0;
    private void Start()
    {
        AddSkill(1);
        AddSkill(0);
        AddSkill(2);


        //m_txtLoading.text = "loading";

        val.Req<SkillTable>(SkillTable.GetGoogleSheetGID(), m_mapTb,
            (a_bSuccess) => // 네트워크 결과 콜백
            {
                if (a_bSuccess == true)
                {
                    Debug.Log("download finish");
                }
                else
                {
                    Debug.Log("download error");
                }
            }
        );

    }
    private void Update()
    {
        //if (m_mapTb[1] != null)
        //    Debug.Log(m_mapTb[1].ID + " , " + m_mapTb[1].NPC + " , " + m_mapTb[1].explanation);
        SkillUIImage();
    }
    public void SkillUIImage()
    {
        for(int i= 0;i<3;i++)
        {
            if (SkillCount < i+1)
            {
                SkillImageFrame[i].gameObject.SetActive(false);
                continue;
            }
            
            SkillImageFrame[i].gameObject.SetActive(true);
            SkillImageFrame[i].transform.GetChild(1).GetComponent<Image>().fillAmount = (1f-skills[Skillindex[i]].GetCoolTime());
        }
    }
    public void AddSkill(int index)
    {
        switch(index)
        {
            case 0:
                skills.Add(0, s_1);
                break;
            case 1:
                skills.Add(1, s_2);
                break;
            case 2:
                skills.Add(2, s_3);
                break;
        }
        //SkillImageFrame[SkillCount].transform.GetChild(1).GetComponent<Image>().sprite = SkillIcon[index];
        //SkillImageFrame[SkillCount].transform.GetChild(0).GetComponent<Image>().sprite = SkillIcon[index];
        SkillImageFrame[SkillCount].transform.GetChild(1).GetComponent<Image>().sprite = SkillIcon[0];
        SkillImageFrame[SkillCount].transform.GetChild(0).GetComponent<Image>().sprite = SkillIcon[0];
        Skillindex[SkillCount] = index;
        SkillCount++;
    }
    public void ExcutSkill(int index, PlayerState ps, GameObject Character, GameObject Effect = null)
    {
        if(!skills.ContainsKey(index))
        {
            Debug.Log("Not Contain Key(Skill)");
            return;
        }
        if (index == 0)
            Effect = StabEffect;
        if (index == 1)
            Effect = CutEffect;
        if (index == 2)
            Effect = ForstEffect;
        skills[index].ExcutSkill(ps, Character, Effect);
    }
    public string GetName(int index)
    {
        return m_mapTb[index + 1].Name;
    }
    public string GetExplanation(int index)
    {
        return m_mapTb[index + 1].explanation;
    }
    public void GetSkillCoolTime(int index)
    {
        skills[index].GetCoolTime();
    }
    public int GetSkillLevel(int index)
    {
        return skills[index].GetSkillLevel();
    }
    public void SkillLevelUp(int index)
    {
        skills[index].SkillLevelUp();
    }
    public Sprite GetSkillIcon(int index)
    {
        return SkillIcon[index];
    }
    public int Damage(int index)
    {
        if (GetSkillLevel(index) > 1)
            return m_mapTb[index+1].UpgradeDamage;
        return m_mapTb[index+1].Damage;
    }
}
