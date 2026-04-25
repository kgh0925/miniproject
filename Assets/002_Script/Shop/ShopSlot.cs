using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private BuyItem MyBuyItem;
    [SerializeField] private Button ItemButton;
    [SerializeField] private TMP_Text VaccineCode;
    [SerializeField] private Image UI_Image;
    private string ItemId;
    private ShopItemCatalog catalog;
    private void Awake()
    {
        //BuyItem = FindFirstObjectByType<BuyItem>(FindObjectsInactive.Include);
    }
    public void Init(BuyItem buyItem)
    {
        MyBuyItem = buyItem;
    }
    public void Bind(string Id, ShopItemCatalog Catalog, bool ButtonActive)
    {
        if (Catalog == null) return;
        catalog = Catalog;
        if (string.IsNullOrWhiteSpace(Id))
        {
            UI_Image.sprite = null;
            UI_Image.enabled = false;
            VaccineCode.text = "상품 준비중";
            return;
        }
        if(Catalog.VaildId(Id.Trim()))
        {
            //Debug.Log("바인드동작");
            UI_Image.enabled = true;
            UI_Image.sprite = Catalog.GetIcon(Id.Trim());
            if (ButtonActive)
            {
                VaccineCode.text = Catalog.GetCode(Id.Trim()).ToString();
                ItemButton.enabled = true;
                UI_Image.color = Color.white;
            }
            else
            {
                VaccineCode.text = "보유중";
                ItemButton.enabled = false;
                UI_Image.color = Color.gray3;
            }
            ItemId = Id;
        }

    }

    public void InfoUIOpen()
    {
        if(MyBuyItem != null)
        {
            MyBuyItem.Info(ItemId, catalog);
            MyBuyItem.gameObject.SetActive(true);
        }
    }
}
