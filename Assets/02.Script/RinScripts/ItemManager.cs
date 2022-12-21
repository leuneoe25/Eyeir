using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemTable
{
    public static int GetGoogleSheetGID() { return 1945592105; }

    public int ID;
    public string Name;
    public string explanation;
    public string box;
    public string store;
    public string price;
    // 초기화를 원하는 모든 변수를 스트링으로 받는 생성자 필요 ( TableWWW의 GetInstance() 에서 사용 )
    public ItemTable(string _0, string _1, string _2, string _3, string _4, string _5)
    {
        ID = int.Parse(_0);
        Name = _1;
        explanation = _2;
        box = _3;
        store = _4;
        price = _5;
        // bool b    = (int.Parse( 인자 ) == 0) ? false : true;
        // int n    = int.Parse( 인자 );
        // float f    = float.Parse(인자);
        // string s    = 인자;
        // 정의한 enum eTest
        // eTest eAt = (eTest)System.Enum.Parse(typeof(eTest), name);
    }
}
public class ItemManager : MonoBehaviour
{
    //Item 관리
    #region Singleton
    private static ItemManager instance = null;
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
    public static ItemManager Instance
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

    Dictionary<int, ItemTable> m_mapTb = new Dictionary<int, ItemTable>();

    #endregion
    public enum ItemName
    {
        BrokenMirror,
        Match,
        Icicle,
        Snowball,
        Eyecrystal,
        Wand,
        Holly

    }
    public List<Item> Inventory = new List<Item>();
    public List<Sprite> ItemImage = new List<Sprite>();
    private ItemSystem ItemSystem = new ItemSystem();

    public int Coin = 0;
    [Header("UI")]
    [SerializeField] private Image[] images_4;
    [SerializeField] private GameObject exp;
    [SerializeField] private Text t_Coin;

    [SerializeField] private PlayerState ps;
    [SerializeField] private Button moreButton;
    [SerializeField] private GameObject moreObject;
    [SerializeField] private Image[] images_16;
    public bool DTrap = false;
    private float Traptime = 0;
    public bool moreCoin = false;
    private float Cointime = 0;
    void Start()
    {
        val.Req<ItemTable>(ItemTable.GetGoogleSheetGID(), m_mapTb,
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

        AddItem(ItemName.BrokenMirror);
        AddItem(ItemName.Eyecrystal);
        AddItem(ItemName.BrokenMirror);
        AddItem(ItemName.BrokenMirror);
        AddItem(ItemName.BrokenMirror);
        
        if(Inventory.Count > 0)
        {
            UISet();
        }
        moreButton.onClick.AddListener(moreFunc);

    }
    public void Update()
    {
        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    GetAttackDamage();
        //}
        if(Traptime>0)
        {
            DTrap = true;
            Debug.Log(Traptime);
            Traptime -= Time.deltaTime;
        }
        else if(DTrap)
        {
            DTrap = false;
            Inventory.RemoveAt(include(ItemName.Eyecrystal));
            UISet();
        }
        if(Cointime > 0)
        {
            moreCoin = true;
            Cointime -= Time.deltaTime;
        }
        else if(moreCoin)
        {
            moreCoin = false;
            Inventory.RemoveAt(include(ItemName.Holly));
            UISet();
        }
    }
    public void UISet()
    {
        t_Coin.text = Coin.ToString();
        for (int i= 0;i<4;i++)
        {
            if(Inventory.Count <= i)
            {
                images_4[i].gameObject.SetActive(false);
                continue;
            }
            images_4[i].gameObject.SetActive(true);
            //images_4[i].sprite = ItemImage[(int)Inventory[i].GetName()];
            EventTrigger eventTrigger = images_4[i].gameObject.AddComponent<EventTrigger>();
            //설명
            //g.transform.GetChild(0).GetComponent<Image>().sprite = ItemManager.Instance.GetItemSprite(Product[i].GetName());


            //이벤트 트리거 설정
            int index = i;

            EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
            entry_PointerDown.eventID = EventTriggerType.PointerEnter;
            entry_PointerDown.callback.AddListener((data) => { OnPointerEnter((int)Inventory[index].GetName()); });
            eventTrigger.triggers.Add(entry_PointerDown);


            EventTrigger.Entry entry_Drag = new EventTrigger.Entry();
            entry_Drag.eventID = EventTriggerType.PointerExit;
            entry_Drag.callback.AddListener((data) => { OnPointerExit((int)Inventory[index].GetName()); });
            eventTrigger.triggers.Add(entry_Drag);
        }
    }
    private void OnPointerEnter(int index)
    {
        Vector3 mos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 v = Camera.main.WorldToScreenPoint(new Vector3(mos.x + 5, mos.y + 5));
        exp.transform.position = new Vector3(mos.x-9, mos.y + 5);
        exp.SetActive(true);
        exp.transform.GetChild(0).GetComponent<Text>().text = m_mapTb[index + 1].Name;
        exp.transform.GetChild(1).GetComponent<Text>().text = m_mapTb[index + 1].explanation;
        
    }

    private void OnPointerExit(int index)
    {
        exp.SetActive(false);
    }
    private void moreFunc()
    {
        if(moreObject.activeSelf)
        {
            moreObject.SetActive(false);
            return;
        }    
        moreObject.SetActive(true);
        int index = 4;
        for(int i=0;i<16;i++)
        {
            if(index >= Inventory.Count)
            {
                images_16[i].gameObject.SetActive(false);
                continue;
            }
            images_16[i].gameObject.SetActive(true);
            EventTrigger eventTrigger = images_16[i].gameObject.AddComponent<EventTrigger>();
            int _in = index;

            EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
            entry_PointerDown.eventID = EventTriggerType.PointerEnter;
            entry_PointerDown.callback.AddListener((data) => { OnPointerEnter((int)Inventory[_in].GetName()); });
            eventTrigger.triggers.Add(entry_PointerDown);


            EventTrigger.Entry entry_Drag = new EventTrigger.Entry();
            entry_Drag.eventID = EventTriggerType.PointerExit;
            entry_Drag.callback.AddListener((data) => { OnPointerExit((int)Inventory[_in].GetName()); });
            eventTrigger.triggers.Add(entry_Drag);
            index++;
        }
    }
    public int GetAttackDamage()
    {
        return itemCount(ItemName.BrokenMirror) * 10;
    }
    public int GetSpeed()
    {
        int index = include(ItemName.Icicle);
        if (index == -1)
            return 0;
        else
            return 5;
    }
    public bool Usedefense()
    {
        int index = include(ItemName.Wand);
        if (index == -1)
        {
            return false;
        }
        else
        {
            Inventory.RemoveAt(index);
            UISet();
            return true;
        }
    }
    public void AddItem(ItemName item)
    {
        int index = include(item);
        //Debug.Log(item.ToString());
        //if (index == -1)//아이템이 인벤에 없을때
        //{
            switch (item)
            {
                case ItemName.BrokenMirror:
                    break;
                case ItemName.Match:
                    ps.AddHp();
                    return;
                    break;
                case ItemName.Icicle:
                    break;
                case ItemName.Snowball:
                    ps.Heal();
                    return;
                    break;
                case ItemName.Eyecrystal:
                if (index != -1)
                    Inventory.RemoveAt(index);
                    Traptime = 10 * 3;
                    break;
                case ItemName.Wand:
                    break;
                case ItemName.Holly:
                if (index != -1)
                    Inventory.RemoveAt(index);
                Cointime = 60 * 5;
                    break;
            }
            Inventory.Add(ItemSystem.NewItemAdd(item));
            for(int i= 0;i<Inventory.Count;i++)
            {
                Debug.Log( i + " : "+Inventory[i].GetName().ToString());
            }
        //}
        //else
        //{
        //    Inventory[index].AddCount();
        //}
    }
    public Item GetItem(ItemName item)
    {
        return ItemSystem.NewItemAdd(item);
    }
    public void UseItem(int index)
    {
        //사용 방법 : UseItem(include(BrokenMirror));
        if (index == -1)
        {
            Debug.Log("Index -1");
            return;
        }
        else
        {

        }
    }
    public Sprite GetItemSprite(ItemName item)
    {
        return ItemImage[(int)item];
    }
    public int include(ItemName item)
    {
        for(int i=0;i<Inventory.Count;i++)
        {
            if(Inventory[i].GetName() == item)
            {
                return i;
            }
        }
        return -1;
    }
    public int itemCount(ItemName item)
    {
        int count = 0;
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].GetName() == item)
            {
                count++;
            }
        }
        return count;
    }

public interface Item
    {
        public ItemName GetName();
        public int GetCount();
        public void AddCount();
        public void Ues();
    }


}

