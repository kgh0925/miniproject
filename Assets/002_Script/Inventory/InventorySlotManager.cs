using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotManager : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private ShopItemCatalog Catalog;
    [SerializeField] private InventorySlot SlotPrefeb;
    [SerializeField] private Transform SlotContainer;


    private List<InventorySlot> Slots = new List<InventorySlot>();

    private void Start()
    {
        BuildSlotViews();
        DrawAllSlots(ItemType.Skin);
    }
    public void DrawAllSlots(ItemType Type)
    {
        if (PlayerData.Instance == null) return;
        IReadOnlyList<string> Inven = PlayerData.Instance.Inventory;
        IReadOnlyList<ShopCatalogEntry> ShopItem = Catalog.ReadShopList;
        int LastIndex = Slots.Count - 1;
        int firstIndex = 0;
        for (int i = 0; i < Slots.Count; i++)
        {
            ShopCatalogEntry entry = i < ShopItem.Count && ShopItem[i].Category  == Type 
                && Inven.Contains(ShopItem[i].Id) ? ShopItem[i] : new ShopCatalogEntry { Id = string.Empty, BuyCode = -1 };

            if (string.IsNullOrEmpty(entry.Id))
            {
                Slots[LastIndex].Bind(entry.Id, Catalog);
                LastIndex--;
            }
            else
            {
                Slots[firstIndex].Bind(entry.Id, Catalog);
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
            InventorySlot slotInstance = Instantiate(SlotPrefeb, SlotContainer);
            slotInstance.gameObject.name = $"InvenSlot_{slotindex:D2}";
            Slots.Add(slotInstance);
        }
    }

/*    public void DrawPlayerSkin()
    {
        DrawAllSlots(ItemType.Skin);
    }
    public void DrawMap()
    {
        DrawAllSlots(ItemType.Map);
    }*/
}
