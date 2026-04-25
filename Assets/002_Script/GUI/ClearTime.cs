using TMPro;
using UnityEngine;

public class ClearTime : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private TMP_Text Txt_ClearTime;
    [SerializeField] private string StageName;

    private void Start()
    {
        int Min = PlayerPrefs.GetInt(StageName + "M", 99);
        int Sec = PlayerPrefs.GetInt(StageName + "S", 59);
        Txt_ClearTime.text = Min.ToString() + "Ка " + Sec.ToString() +"УЪ";
    }
}
