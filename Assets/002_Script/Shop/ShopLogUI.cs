using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopLogUI : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] Image LogUI;
    [SerializeField] TMP_Text LogText;

    public void ResultLog(bool tf)
    {
        StartCoroutine(LogUIDelay(tf));
    }
    IEnumerator LogUIDelay(bool tf)
    {
        if (tf)
        {
            LogUI.color = Color.green;
            LogText.text = "구매가 완료되었습니다 :)";
        }
        else
        {
            LogUI.color = Color.red;
            LogText.text = "잔액이 부족해 구매할 수 없습니다";
        }
        LogUI.gameObject.SetActive(true);
        LogText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LogUI.gameObject.SetActive(false);
        LogText.gameObject.SetActive(false);

    }
}
