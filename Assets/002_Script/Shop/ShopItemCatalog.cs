using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class ShopItemCatalog : MonoBehaviour
{
    [SerializeField] private List<ShopCatalogEntry> ShopList = new List<ShopCatalogEntry>();

    public IReadOnlyList<ShopCatalogEntry> ReadShopList => ShopList;
    public readonly Dictionary<string,ShopCatalogEntry> CatalogById = new Dictionary<string,ShopCatalogEntry>();
    private void Awake()
    {
        if(ShopList.Count <= 0)
        {
            Debug.LogWarning("ShopList.Count <= 0");
            return;
        }
        CatalogSetUp();
    }

    private void CatalogSetUp()
    {
        foreach(ShopCatalogEntry entry in ShopList)
        {
            if(!CatalogById.ContainsKey(entry.Id.Trim()))
            {
                CatalogById.Add(entry.Id.Trim(), entry);
                //Debug.Log(entry.Id.Trim() + CatalogById.Count);
                
                //CatalogById.Add(entry.Id.Trim(), new ShopCatalogEntry 
                //{ Id = entry.Id , BuyCode = entry.BuyCode , Category = entry.Category , DisplayName = entry.DisplayName, Icon = entry.Icon});
            }
        }
    }
    public int MaxSlotCount()
    {
        return ShopList.Count;
    }
    public bool VaildId(string Id)
    {
        //Debug.Log(CatalogById.ContainsKey(Id.Trim()) + Id.Trim());
        //Debug.Log(CatalogById.Count);
        return CatalogById.ContainsKey(Id.Trim());
    }

    public Sprite GetIcon(string Id)
    {
        if (!VaildId(Id)) return null;
        return CatalogById[Id].Icon;
    }
    public int GetCode(string Id)
    {
        if(!VaildId(Id)) return -1;
        return CatalogById[Id].BuyCode;
    }

    public ItemType GetItemType(string Id)
    {
        return CatalogById[Id].Category;
    }
}
