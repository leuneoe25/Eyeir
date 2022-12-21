using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Smithy : MonoBehaviour
{
    private bool stay = false;
    private bool isOnStore = true;
    [SerializeField] private GameObject StoreUI;
    [SerializeField] private GameObject StoreUICheck;
    [SerializeField] private Button StoreUICheckYes;
    [SerializeField] private Button StoreUICheckNo;
    [SerializeField] private Text CheckText;
    private GameObject Player;
    [Header("Slot")]
    [SerializeField] private GameObject[] Slots;
    private List<int> purchased;

    private int Select = -1;
    //public List<ItemManager.Item> Product;
    //public List<>
    void Start()
    {
        //아이템 랜덤호출
        stay = false;
        purchased = new List<int>();
        //Product = new List<ItemManager.Item>();

        ////제품 3개
        //for (int i = 0; i < 3; i++)
        //{
        //    int I = Random.RandomRange(0, 7);

        //    Product.Add(ItemManager.Instance.GetItem((ItemManager.ItemName)I));
        //    purchased.Add(0);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && stay && !StoreUI.activeSelf)
        {

            //z를 누르시오 표시
            OnStore();
            return;
            //상자 사라지기

        }
        if (!stay || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Z))
        {
            if (StoreUI.activeSelf)
                OffStore();
        }
    }
    private void OnStore()
    {
        isOnStore = true;
        Player.transform.GetChild(1).GetComponent<Rope>().DeletRope();
        Player.GetComponent<PlayerState>().StopHook = true;
        StoreUI.SetActive(true);

        //상품 세팅
        for (int i = 0; i < 3; i++)
        {
            GameObject g = Slots[i];
            g.SetActive(true);
            EventTrigger eventTrigger = Slots[i].AddComponent<EventTrigger>();
            //설명
            g.transform.GetChild(0).GetComponent<Image>().sprite = SkillCommand.Instance.GetSkillIcon(i);
            g.transform.GetChild(1).GetComponent<Text>().text = SkillCommand.Instance.GetName(i); ;
            g.transform.GetChild(2).GetComponent<Text>().text = SkillCommand.Instance.GetExplanation(i);
            g.transform.GetChild(3).gameObject.SetActive(false);
            if (SkillCommand.Instance.GetSkillLevel(i) == 2)
            {
                Slots[i].SetActive(false);
                //g.transform.GetChild(4).gameObject.SetActive(true);
                //eventTrigger.triggers.Clear();
                //eventTrigger = null;
                //eventTrigger.enabled = false;
                //Slots[i].AddComponent<EventTrigger>().enabled = false;

                continue;
            }
            else
            {
                //이벤트 트리거 설정
                Slots[i].AddComponent<EventTrigger>().enabled = true;

                EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
                entry_PointerDown.eventID = EventTriggerType.PointerEnter;
                entry_PointerDown.callback.AddListener((data) => { OnPointerEnter(g); });
                eventTrigger.triggers.Add(entry_PointerDown);


                EventTrigger.Entry entry_Drag = new EventTrigger.Entry();
                entry_Drag.eventID = EventTriggerType.PointerExit;
                entry_Drag.callback.AddListener((data) => { OnPointerExit(g); });
                eventTrigger.triggers.Add(entry_Drag);

                EventTrigger.Entry entry_EndDrag = new EventTrigger.Entry();
                entry_EndDrag.eventID = EventTriggerType.PointerClick;
                entry_EndDrag.callback.AddListener((data) => { OnPointerClick(g); });
                eventTrigger.triggers.Add(entry_EndDrag);
            }
            

           


        }






    }
    private void OnPointerEnter(GameObject g)
    {
        g.transform.GetChild(3).gameObject.SetActive(true);

    }
    private void OnPointerExit(GameObject g)
    {
        g.transform.GetChild(3).gameObject.SetActive(false);
    }
    //상품 클릭
    private void OnPointerClick(GameObject g)
    {
        //선택
        Select = int.Parse(g.name) - 1;
        Debug.Log(SkillCommand.Instance.GetSkillContains(Select));
        if (SkillCommand.Instance.GetSkillContains(Select))
        {
            CheckText.text = "스킬을 업그레이드 하시겠습니끼?";
        }
        else
        {
            
            CheckText.text = "스킬을 구매 하시겠습니끼?";
        }
        if (SkillCommand.Instance.Buy(Select) > GoodsSystem.Instance.GetCoin())
        {
            StoreUICheck.SetActive(true);
            CheckText.text = "재화가 부족합니다";
            StoreUICheckYes.gameObject.SetActive(false);
            StoreUICheckNo.onClick.RemoveAllListeners();
            StoreUICheckNo.onClick.AddListener(StoreUICheckNoFunc);
            return;
        }
        //효과음 + 크기 변경
        StoreUICheck.SetActive(true);
        StoreUICheckYes.gameObject.SetActive(true);
        StoreUICheckYes.onClick.RemoveAllListeners();
        StoreUICheckYes.onClick.AddListener(StoreUICheckYesFunc);
        StoreUICheckNo.onClick.RemoveAllListeners();
        StoreUICheckNo.onClick.AddListener(StoreUICheckNoFunc);

        
    }
    private void StoreUICheckYesFunc()
    {
        StoreUICheck.SetActive(false);
        BuyProduct();
    }
    private void StoreUICheckNoFunc()
    {
        StoreUICheck.SetActive(false);
    }
    private void BuyProduct()
    {
        GoodsSystem.Instance.AddCoin(-SkillCommand.Instance.Buy(Select));
        Debug.Log("Upgrade " + Select + "  " + SkillCommand.Instance.GetSkillLevel(Select));
        //재화 소모
        SkillCommand.Instance.SkillLevelUp(Select);


        OnStore();
    }
    private void OffStore()
    {
        if (Player != null)
            Player.GetComponent<PlayerState>().StopHook = false;
        isOnStore = false;

        StoreUI.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stay = true;
            Player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stay = false;
        }
    }
}
