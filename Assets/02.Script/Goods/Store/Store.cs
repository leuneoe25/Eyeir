using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    private bool stay = false;
    private bool isOnStore = true;
    [SerializeField] private GameObject StoreUI;
    [SerializeField] private GameObject StoreUICheck;
    [SerializeField] private Button StoreUICheckYes;
    [SerializeField] private Button StoreUICheckNo;
    private GameObject Player;
    [Header("Slot")]
    [SerializeField] private GameObject[] Slots;
    private List<int> purchased;
    public bool isSkillStore = false;

    private int Select = -1;
    public List<ItemManager.Item> Product;
    //public List<>
    void Start()
    {
        //������ ����ȣ��
        stay = false;
        purchased = new List<int>();
        if (!isSkillStore)
        {
            Product = new List<ItemManager.Item>();
            
            //��ǰ 3��
            for (int i=0;i<3;i++)
            {
                int I = Random.RandomRange(0, 7);
                
                Product.Add(ItemManager.Instance.GetItem((ItemManager.ItemName)I));
                purchased.Add(0);
            }
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && stay&&!StoreUI.activeSelf)
        {

            Debug.Log("z");
            //z�� �����ÿ� ǥ��
            OnStore();
            return;
            //���� �������

        }
        if(!stay || Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.Z))
        {
            if(StoreUI.activeSelf)
                OffStore();
        }
    }
    private void OnStore()
    {
        isOnStore = true;
        Player.transform.GetChild(1).GetComponent<Rope>().DeletRope();
        Player.GetComponent<PlayerState>().StopHook = true;
        StoreUI.SetActive(true);

        //��ǰ ����
        for(int i = 0;i<3;i++)
        {
            GameObject g = Slots[i];
            g.SetActive(true);
            EventTrigger eventTrigger = g.AddComponent<EventTrigger>();
            //����
            g.transform.GetChild(0).GetComponent<Image>().sprite = ItemManager.Instance.GetItemSprite(Product[i].GetName());
            if (purchased[i] != 0)
            {
                //g.transform.GetChild(4).gameObject.SetActive(true);
                //eventTrigger.triggers.Clear();
                g.SetActive(false);
                continue;
            }
            g.transform.GetChild(1).GetComponent<Text>().text = Product[i].GetName().ToString();
            g.transform.GetChild(2).GetComponent<Text>().text = "����";
            g.transform.GetChild(3).gameObject.SetActive(false);

            //�̺�Ʈ Ʈ���� ����

            
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
    private void OnPointerEnter(GameObject g)
    {
        g.transform.GetChild(3).gameObject.SetActive(true);
        
    }
    private void OnPointerExit(GameObject g)
    {
        g.transform.GetChild(3).gameObject.SetActive(false);
    }
    //��ǰ Ŭ��
    private void OnPointerClick(GameObject g)
    {
        //ȿ���� + ũ�� ����
        StoreUICheck.SetActive(true);
        StoreUICheckYes.onClick.RemoveAllListeners();
        StoreUICheckYes.onClick.AddListener(StoreUICheckYesFunc);
        StoreUICheckNo.onClick.RemoveAllListeners();
        StoreUICheckNo.onClick.AddListener(StoreUICheckNoFunc);

        //����
        Select = int.Parse(g.name);
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
        //��ȭ �Ҹ�
        GoodsSystem.Instance.AddCoin(-ItemManager.Instance.Getprice( Product[Select - 1].GetName()));
        Debug.Log("Buy " + Select);
        purchased[Select-1] = 1;
        ItemManager.Instance.AddItem(Product[Select-1].GetName());
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
