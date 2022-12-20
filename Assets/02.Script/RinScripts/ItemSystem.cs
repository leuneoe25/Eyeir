using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem
{
    public ItemManager.Item NewItemAdd(ItemManager.ItemName name)
    {
        ItemManager.Item newitem;
        switch (name)
        {
            case ItemManager.ItemName.BrokenMirror:
                return newitem = new BrokenMirror(name);
                break;
            case ItemManager.ItemName.Match:
                return newitem = new Match(name);
                break;
            case ItemManager.ItemName.Icicle:
                return newitem = new Icicle(name);
                break;
            case ItemManager.ItemName.Snowball:
                return newitem = new Snowball(name);
                break;
            case ItemManager.ItemName.Eyecrystal:
                return newitem = new Eyecrystal(name);
                break;
            case ItemManager.ItemName.Wand:
                return newitem = new Wand(name);
                break;
            case ItemManager.ItemName.Holly:
                return newitem = new Holly(name);
                break;
        }
        return null;
    }
    #region Items
    public class BrokenMirror : ItemManager.Item
    {
        private ItemManager.ItemName name;
        private int Count;
        private int price;
        public BrokenMirror(ItemManager.ItemName name)
        {
            this.name = name;
        }
        public void AddCount()
        {
            Count++;
        }
        public int GetCount()
        {
            return Count;
        }

        public ItemManager.ItemName GetName()
        {
            return name;
        }

        public void Ues()
        {
            throw new System.NotImplementedException();
        }
    }
    public class Match : ItemManager.Item
    {
        private ItemManager.ItemName name;
        private int Count;
        public Match(ItemManager.ItemName name)
        {
            this.name = name;
        }
        public void AddCount()
        {
            Count++;
        }
        public int GetCount()
        {
            return Count;
        }

        public ItemManager.ItemName GetName()
        {
            return name;
        }

        public void Ues()
        {
            throw new System.NotImplementedException();
        }
    }
    public class Icicle : ItemManager.Item
    {
        private ItemManager.ItemName name;
        private int Count;
        public Icicle(ItemManager.ItemName name)
        {
            this.name = name;
        }
        public void AddCount()
        {
            Count++;
        }
        public int GetCount()
        {
            return Count;
        }

        public ItemManager.ItemName GetName()
        {
            return name;
        }

        public void Ues()
        {
            throw new System.NotImplementedException();
        }
    }
    public class Snowball : ItemManager.Item
    {
        private ItemManager.ItemName name;
        private int Count;
        public Snowball(ItemManager.ItemName name)
        {
            this.name = name;
        }
        public void AddCount()
        {
            Count++;
        }
        public int GetCount()
        {
            return Count;
        }

        public ItemManager.ItemName GetName()
        {
            return name;
        }

        public void Ues()
        {
            throw new System.NotImplementedException();
        }
    }
    public class Eyecrystal : ItemManager.Item
    {
        private ItemManager.ItemName name;
        private int Count;
        public Eyecrystal(ItemManager.ItemName name)
        {
            this.name = name;
        }
        public void AddCount()
        {
            Count++;
        }
        public int GetCount()
        {
            return Count;
        }

        public ItemManager.ItemName GetName()
        {
            return name;
        }

        public void Ues()
        {
            throw new System.NotImplementedException();
        }
    }
    public class Wand : ItemManager.Item
    {
        private ItemManager.ItemName name;
        private int Count;
        public Wand(ItemManager.ItemName name)
        {
            this.name = name;
        }
        public void AddCount()
        {
            Count++;
        }
        public int GetCount()
        {
            return Count;
        }

        public ItemManager.ItemName GetName()
        {
            return name;
        }

        public void Ues()
        {
            throw new System.NotImplementedException();
        }
    }
    public class Holly : ItemManager.Item
    {
        private ItemManager.ItemName name;
        private int Count;
        public Holly(ItemManager.ItemName name)
        {
            this.name = name;
        }
        public void AddCount()
        {
            Count++;
        }
        public int GetCount()
        {
            return Count;
        }

        public ItemManager.ItemName GetName()
        {
            return name;
        }

        public void Ues()
        {
            throw new System.NotImplementedException();
        }
    }
    #endregion
}
