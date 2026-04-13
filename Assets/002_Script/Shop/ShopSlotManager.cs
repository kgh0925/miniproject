using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopSlotManager : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private ShopItemCatalog Catalog;
    [SerializeField] private ShopSlot SlotPrefeb;
    [SerializeField] private Transform SlotContainer;



    private List<ShopSlot> Slots = new List<ShopSlot>();

    private void Start()
    {
        BuildSlotViews();
        DrawAllSlots(ItemType.Skin);
    }
    private void OnEnable()
    {
        PlayerData.Instance.TypeEvent += ShoppingDrawAllSlots;
    }
    private void OnDisable()
    {
        PlayerData.Instance.TypeEvent -= ShoppingDrawAllSlots;
    }
    private void ShoppingDrawAllSlots(string Id)
    {
        DrawAllSlots(Catalog.GetItemType(Id));
    }
    public void DrawAllSlots(ItemType Type)
    {
        IReadOnlyList<ShopCatalogEntry> ShopItem = Catalog.ReadShopList;
        IReadOnlyList<string> PlayerInven = PlayerData.Instance.Inventory;

        int LastIndex = Slots.Count - 1;
        int firstIndex = 0;
        for(int i = 0; i < Slots.Count; i++)
        {
            ShopCatalogEntry entry = i < ShopItem.Count && ShopItem[i].Category == Type ? ShopItem[i] : new ShopCatalogEntry { Id = string.Empty, BuyCode = -1 };   
            if(string.IsNullOrEmpty(entry.Id))
            {
                Slots[LastIndex].Bind(entry.Id, Catalog, false);
                LastIndex--;
            }
            else
            {
                if(PlayerInven.Contains(entry.Id.Trim()))
                {
                    Slots[firstIndex].Bind(entry.Id, Catalog, false);
                }
                else
                {
                    Slots[firstIndex].Bind(entry.Id, Catalog, true);
                }
                firstIndex++;
            }
                
            //Debug.Log(entry.Id);
        }
    }

    private void BuildSlotViews()
    {
        if (SlotContainer == null || SlotPrefeb == null)
        {
            Debug.Log("SlotContainer == null || SlotPrefeb == null");
            return;
        }
        if (Catalog == null)
        {
            Debug.Log("Catalog == null");
            return;
        }
        for (int childIndex = SlotContainer.childCount - 1; childIndex >= 0; childIndex--)
        {
            Destroy(SlotContainer.GetChild(childIndex).gameObject);
        }
        Slots.Clear();

        int capacity = Mathf.Max(0, Catalog.MaxSlotCount());
        for (int slotindex = 0; slotindex < capacity; slotindex++)
        {
            ShopSlot slotInstance = Instantiate(SlotPrefeb, SlotContainer);
            slotInstance.gameObject.name = $"ShopSlot_{slotindex:D2}";
            Slots.Add(slotInstance);
        }
    }
/*
    public void DrawPlayerSkin()
    {
        DrawAllSlots(ItemType.Skin);
    }
    public void DrawMap()
    {
        DrawAllSlots(ItemType.Map);
    }*/
}
