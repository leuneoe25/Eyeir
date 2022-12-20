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

    // 초기화를 원하는 모든 변수를 스트링으로 받는 생성자 필요 ( TableWWW의 GetInstance() 에서 사용 )
    public SkillTable(string _1, string _2, string _3)
    {
        ID = int.Parse(_1);
        Name = _2;
        explanation = _3;

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

    public TableSkill val = null;
    public Text m_txtLoading = null;
    public List<Sprite> SkillIcon = new List<Sprite>();

    Dictionary<int, SkillTable> m_mapTb = new Dictionary<int, SkillTable>();

    #endregion
    private void Start()
    {
        skills.Add(0, s_1);
        skills.Add(1, s_2);
        skills.Add(2, s_3);


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
    }
    public void ExcutSkill(int index, PlayerState ps, GameObject Character)
    {
        skills[index].ExcutSkill(ps, Character);
    }
    public string GetName(int index)
    {
        return m_mapTb[index + 1].Name;
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
}
