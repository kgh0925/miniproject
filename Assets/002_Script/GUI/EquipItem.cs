using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipItem : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Image ItemImage;
    [SerializeField] private TMP_Text ItemText;
    [SerializeField] private EquipLogUI LogUI;

    private string ItemId;
    private ItemType MyType;

    public void EquipUI(string Id, ShopItemCatalog Catalog)
    {
        if(ItemImage == null || ItemText == null || string.IsNullOrWhiteSpace(Id) || Catalog == null || !Catalog.VaildId(Id.Trim()))
        {
            return;
        }
        MyType = Catalog.GetItemType(Id.Trim());
        ItemId = Id;
        ItemImage.sprite = Catalog.GetIcon(ItemId);
        ItemText.text = Id;
    }

    public void Equip()
    {
        if (PlayerData.Instance == null) return;
        switch (MyType)
        {
            case ItemType.Map:
                PlayerData.Instance.MapEquip(ItemId);
                break;
            case ItemType.Skin:
                PlayerData.Instance.SkinEquip(ItemId); 
                break;
            default:
                break;
        } 
        LogUI.ResultLog(true);
        this.gameObject.SetActive(false);
    }
}
