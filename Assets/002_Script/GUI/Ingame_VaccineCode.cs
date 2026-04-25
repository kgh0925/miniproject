using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ingame_VaccineCode : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Slider Vaccine_Slider;
    [SerializeField] private TMP_Text Vaccine_Percent;
    [SerializeField] VaccineCodeCounter VaccineCodeCounterRef;

    private void Start()
    {
        if(Vaccine_Slider == null || Vaccine_Percent == null || VaccineCodeCounterRef == null)
        {
            Debug.Log("Vaccine_Slider == null || Vaccine_Percent == null || VaccineCodeCounterRef == null");
            return;
        }
        UpdateUI();
    }
    private void OnEnable()
    {
        PlayerData.Instance.Code += UpdateUI;
    }
    private void OnDisable()
    {
        PlayerData.Instance.Code -= UpdateUI;
    }

    private void UpdateUI()
    {
        Vaccine_Slider.value = 1 - ((float)VaccineCodeCounterRef.CurrentCount / VaccineCodeCounterRef.TotalCount);
        Vaccine_Percent.text = ((int)((1 - ((float)VaccineCodeCounterRef.CurrentCount / VaccineCodeCounterRef.TotalCount)) * 100)).ToString() + "%";
    }

}
