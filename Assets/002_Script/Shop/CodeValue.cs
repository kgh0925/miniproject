using TMPro;
using UnityEngine;

public class CodeValue : MonoBehaviour
{
    [SerializeField] private TMP_Text VaccineCode;
    private void Awake()
    {
        if(PlayerData.Instance != null && VaccineCode != null)
        {
            VaccineCode.text = PlayerData.Instance.GetCode.ToString();
        }
    }
    private void OnEnable()
    {
        PlayerData.Instance.Shopping += VaccineCode_Refresh;
    }

    private void OnDisable()
    {
        PlayerData.Instance.Shopping -= VaccineCode_Refresh;
    }
    private void VaccineCode_Refresh()
    {
        if (PlayerData.Instance != null && VaccineCode != null)
        {
            VaccineCode.text = PlayerData.Instance.GetCode.ToString();
        }
    }
}
