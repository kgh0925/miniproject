using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Image UI_Image;
    [SerializeField] private EquipItem UI_Equip;
    [SerializeField] private Button MyButton;
    [SerializeField] private Image Label;

    private string ItemId;
    private ShopItemCatalog catalog;

    private void Awake()
    {
        //UI_Equip = FindFirstObjectByType<EquipItem>(FindObjectsInactive.Include);
    }
    public void Init(EquipItem equip)
    {
        UI_Equip = equip;
    }
    public void Bind(string Id, ShopItemCatalog Catalog)
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            UI_Image.sprite = null;
            UI_Image.enabled = false;
            Label.gameObject.SetActive(false);
            return;
        }
        if (Catalog.VaildId(Id.Trim()))
        {
            UI_Image.enabled = true;
            UI_Image.sprite = Catalog.GetIcon(Id.Trim());
            ItemId = Id;
            catalog = Catalog;
            if (PlayerData.Instance != null && Label != null && (PlayerData.Instance.EnquipMap == Id || PlayerData.Instance.EnquipSkin == Id))
            {
                //UI_Image.color = Color.darkGreen;
                Label.gameObject.SetActive(true);
                MyButton.enabled = false;
            }
            else
            {
                //UI_Image.color = Color.white;
                Label.gameObject.SetActive(false);
                if (!MyButton.enabled)
                {
                    MyButton.enabled = true;
                }
            }
        }

    }

    public void EquipUI_Open()
    {
        if (UI_Equip == null || catalog == null) return;

        UI_Equip.EquipUI(ItemId, catalog);
        UI_Equip.gameObject.SetActive(true);
    }
}
