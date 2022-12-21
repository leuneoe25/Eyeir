using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCTable
{
    public static int GetGoogleSheetGID() { return 832042991; }

    public int ID;
    public string Name;
    public string[] script;
    public string script_1;
    public string script_2;
    public string script_3;
    public string script_4;
    public string script_5;
    public string script_6;
    public string script_7;
    public string script_8;
    public string script_9;
    public string script_10;
    // 초기화를 원하는 모든 변수를 스트링으로 받는 생성자 필요 ( TableWWW의 GetInstance() 에서 사용 )
    public NPCTable(string _0,string _1, string _2, string _3, string _4, string _5, string _6, string _7, string _8, string _9, string _10, string _11)
    {
        ID = int.Parse(_0);
        Name = _1;
        script = new string[10];
        script[0] = (_2);
        script[1] = (_3);
        script[2] = (_4);
        script[3] = (_5);
        script[4] = (_6);
        script[5] = (_7);
        script[6] = (_8);
        script[7] = (_9);
        script[8] = (_10);
        script[9] = (_11);


        // bool b    = (int.Parse( 인자 ) == 0) ? false : true;
        // int n    = int.Parse( 인자 );
        // float f    = float.Parse(인자);
        // string s    = 인자;
        // 정의한 enum eTest
        // eTest eAt = (eTest)System.Enum.Parse(typeof(eTest), name);
    }
}
public class NPCSystem : MonoBehaviour
{
    #region Singleton
    private static NPCSystem instance = null;
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
    public static NPCSystem Instance
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
    //public Text m_txtLoading = null;

    Dictionary<int, NPCTable> m_mapTb = new Dictionary<int, NPCTable>();

    #endregion
    public enum NPCName
    {
        Kai,
        Gerda,
        Jack,
        Mika,
        Liam,
        Dmitry,
        Ivan,
        Alyosha
    }
    void Start()
    {
        val.Req<NPCTable>(NPCTable.GetGoogleSheetGID(), m_mapTb,
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

    public string GetScript(NPCName name, int index)
    {
        return m_mapTb[((int)name)+1].script[index];
    }
    public string GetName(NPCName name)
    {
        return m_mapTb[((int)name)+1].Name;
    }
}
