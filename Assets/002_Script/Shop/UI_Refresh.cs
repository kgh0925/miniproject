using TMPro;
using UnityEngine;

public class UI_Refresh : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private RectTransform ShopUI;
    [SerializeField] private RectTransform InvenUI;
    [SerializeField] private TMP_Text Txt_Btn;
    [SerializeField] InventorySlotManager InvenManager;
    [SerializeField] ShopSlotManager ShopManager;
    private bool IsInvenOpen;

    public void ChangeUI()
    {
        if (ShopUI == null || InvenUI == null || Txt_Btn == null) return;
        if(IsInvenOpen)
        {
            InvenUI.gameObject.SetActive(false);
            ShopUI.gameObject.SetActive(true);
        }
        else if(!IsInvenOpen)
        {
            InvenUI.gameObject.SetActive(true);
            ShopUI.gameObject.SetActive(false);
        }
        IsInvenOpen = !IsInvenOpen;
        Txt_Btn.text = IsInvenOpen ? "상점" : "인벤토리";
    }

    public void DrawPlayerSkin()
    {
        if (InvenManager == null || ShopManager == null) return;
        if(IsInvenOpen)
        {
            InvenManager.DrawAllSlots(ItemType.Skin);
        }
        else
        {
            ShopManager.DrawAllSlots(ItemType.Skin);
        }
    }
    public void DrawMap()
    {
        if (InvenManager == null || ShopManager == null) return;
        if (IsInvenOpen)
        {
            InvenManager.DrawAllSlots(ItemType.Map);
        }
        else
        {
            ShopManager.DrawAllSlots(ItemType.Map);
        }
    }
}
