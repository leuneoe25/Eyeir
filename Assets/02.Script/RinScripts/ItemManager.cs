using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //Item 包府
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
    private ItemSystem ItemSystem = new ItemSystem();
    void Start()
    {

    }
    
    public void AddItem(ItemName item)
    {
        int index = include(item);
        //Debug.Log(item.ToString());
        if (index == -1)
        {
            Inventory.Add(ItemSystem.NewItemAdd(item));
            for(int i= 0;i<Inventory.Count;i++)
            {
                Debug.Log( i + " : "+Inventory[i].GetName().ToString());
            }
        }
        else
        {
            Inventory[index].AddCount();
        }
    }
    public void UseItem(int index)
    {
        //荤侩 规过 : UseItem(include(BrokenMirror));
        if (index == -1)
        {
            Debug.Log("Index -1");
            return;
        }
        else
        {

        }
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

    public interface Item
    {
        public ItemName GetName();
        public int GetCount();
        public void AddCount();
        public void Ues();
    }
    

}

