using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Image UI_Image;
    
    private string ItemId;
    public void Bind(string Id, ShopItemCatalog Catalog)
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            UI_Image.sprite = null;
            UI_Image.enabled = false;
            return;
        }
        if (Catalog.VaildId(Id.Trim()))
        {
            UI_Image.enabled = true;
            UI_Image.sprite = Catalog.GetIcon(Id.Trim());
            ItemId = Id;
        }

    }
}
