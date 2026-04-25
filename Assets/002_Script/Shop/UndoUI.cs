using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UndoUI : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] Image MyImage;
    [SerializeField] TMP_Text Txt_Id;
    [SerializeField] TMP_Text Txt_VaccineCode;
    [SerializeField] Image Img_LogUI;
    [SerializeField] TMP_Text Txt_LogUI;
    [SerializeField] RectTransform UndoSystemUI;
    [SerializeField] ShopItemCatalog Catalog;

    public void ButtonClick()
    {
        if (PlayerData.Instance == null || Catalog == null) return;
        if(!PlayerData.Instance.StackPeek(out UndoData data))
        {
            StartCoroutine(LogUIDelay(false));
            return;
        }
        else
        {
            MyImage.sprite = Catalog.GetIcon(data.ItemId);
            Txt_Id.text = data.ItemId;
            Txt_VaccineCode.text = "총 백신 코드 : " +(data.ItemValue + PlayerData.Instance.GetCode).ToString();
            UndoSystemUI.gameObject.SetActive(true);


        }

    }
    public void UndoItem(bool tf)
    {
        StartCoroutine (LogUIDelay(tf));
    }
    IEnumerator LogUIDelay(bool tf)
    {
        if(tf)
        {
            Img_LogUI.color = Color.green;
            Txt_LogUI.text = "성공적으로 구매 기록을 되돌렸습니다.";
            Img_LogUI.gameObject.SetActive(true);
            Txt_LogUI.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            Img_LogUI.gameObject.SetActive(false);
            Txt_LogUI.gameObject.SetActive(false);
        }
        else
        {
            Img_LogUI.color = Color.red;
            Txt_LogUI.text = "되돌릴 상품이 없습니다.";
            Img_LogUI.gameObject.SetActive(true);
            Txt_LogUI.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            Img_LogUI.gameObject.SetActive(false);
            Txt_LogUI.gameObject.SetActive(false);
        }

    }
}
