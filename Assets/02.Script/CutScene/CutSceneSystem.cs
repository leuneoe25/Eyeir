using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneTable
{
    public static int GetGoogleSheetGID() { return 1862234714; }
    public string[] script;
    // 초기화를 원하는 모든 변수를 스트링으로 받는 생성자 필요 ( TableWWW의 GetInstance() 에서 사용 )
    public CutSceneTable(string _0, string _1, string _2, string _3, string _4, string _5, string _6, string _7, string _8, string _9, string _10, string _11)
    {
        script = new string[12];
        script[0] = (_0);
        script[1] = (_1);
        script[2] = (_2);
        script[3] = (_3);
        script[4] = (_4);
        script[5] = (_5);
        script[6] = (_6);
        script[7] = (_7);
        script[8] = (_8);
        script[9] = (_9);
        script[10] = (_10);
        script[11] = (_11);
        
        // bool b    = (int.Parse( 인자 ) == 0) ? false : true;
        // int n    = int.Parse( 인자 );
        // float f    = float.Parse(인자);
        // string s    = 인자;
        // 정의한 enum eTest
        // eTest eAt = (eTest)System.Enum.Parse(typeof(eTest), name);
    }
}
public class CutSceneSystem : MonoBehaviour
{
    #region INSPECTOR

    public TableSheet val = null;
    //public Text m_txtLoading = null;

    Dictionary<int, CutSceneTable> m_mapTb = new Dictionary<int, CutSceneTable>();

    #endregion
    public enum CutScene_
    {
        start,
        end,
    }
    public CutScene_ nowScene;
    public List<Sprite> image;
    [Header("BookImage")]
    [SerializeField] private GameObject StartBook;
    [SerializeField] private GameObject OpenBook;
    [SerializeField] private Image Pade;
    void Start()
    {
        val.Req<CutSceneTable>(CutSceneTable.GetGoogleSheetGID(), m_mapTb,
            (a_bSuccess) => // 네트워크 결과 콜백
            {
                if (a_bSuccess == true)
                {
                    Debug.Log("download finish");
                    StartCoroutine(CutScene());
                }
                else
                {
                    Debug.Log("download error");
                }
            }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CutScene()
    {
        if(nowScene == CutScene_.start)
        {
            StartBook.SetActive(true);
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(PadeIn());
        yield return new WaitForSeconds(2f);

    }
    IEnumerator PadeIn()
    {
        Pade.gameObject.SetActive(true);
        Pade.color = new Color(0, 0, 0, 0);

        while (Pade.color.a < 1)
        {
            Pade.color += new Color(0, 0, 0, 0.1f); ;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(PadeOut());
    }
    IEnumerator PadeOut()
    {
        Pade.color = new Color(0, 0, 0, 1);

        while (Pade.color.a > 0)
        {
            Pade.color -= new Color(0, 0, 0, 0.1f); ;
            yield return new WaitForSeconds(0.05f);
        }
        Pade.gameObject.SetActive(false);
    }

}
