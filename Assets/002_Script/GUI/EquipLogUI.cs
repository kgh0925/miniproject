using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipLogUI : MonoBehaviour
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
            LogText.text = "성공적으로 스킨을 장착하였습니다 :)";
        }
        else
        {
            LogUI.color = Color.red;
            LogText.text = "오류로 인하여 장착을 실패했습니다.";
        }
        LogUI.gameObject.SetActive(true);
        LogText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LogUI.gameObject.SetActive(false);
        LogText.gameObject.SetActive(false);

    }
}
