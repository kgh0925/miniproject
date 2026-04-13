using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text ItemName;
    [SerializeField] TMP_Text Code;
    [SerializeField] ShopLogUI LogUI;

    private string ItemId;
    private int Amount;

    private Queue<PurchaseRequest> BuyItemQueue = new Queue<PurchaseRequest>();

    private void Awake()
    {
        BuyItemQueue.Clear();
    }
    public void Info(string Id, ShopItemCatalog Catalog)
    {
        if(ItemImage == null || ItemName == null || Code == null) return;
        if(string.IsNullOrWhiteSpace(Id) || !Catalog.VaildId(Id)) return;

        ItemImage.sprite = Catalog.GetIcon(Id);
        ItemName.text = Id;
        ItemId = Id;
        Amount = Catalog.GetCode(Id);
        Code.text = Amount.ToString();
    }

    public void Buy()
    {
        PurchaseRequest purchaseRequest = new PurchaseRequest(ItemId, Amount);
        BuyItemQueue.Enqueue(purchaseRequest);
        while(BuyItemQueue.Count > 0)
        {
            ProgressQueue();
        }
        this.gameObject.SetActive(false);
    }

    private void ProgressQueue()
    {
        if (BuyItemQueue.Count <= 0) return;
        if(PlayerData.Instance != null)
        {
            PurchaseRequest purchaseRequest = BuyItemQueue.Dequeue();
            if(PlayerData.Instance.PurchaseItem(purchaseRequest.Id, purchaseRequest.BuyCode))
            {
                //TODO : 구매완료 UI
                if (LogUI == null) return;
                LogUI.ResultLog(true);
            }
            else
            {
                //TODO : 구매실패 UI
                if (LogUI == null) return;
                LogUI.ResultLog(false);
            }

        }

    }


}
