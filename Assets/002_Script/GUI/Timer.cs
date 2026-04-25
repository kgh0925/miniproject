using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] TMP_Text Txt_Timer;

    private float Sec;
    private float Min;

    public float GetSec => Sec;
    public float GetMin => Min;
    private void Awake()
    {
        Sec = 0;
        Min = 0;
    }
    private void Update()
    {
        Sec += Time.deltaTime;
        if (Sec >= 60)
        {
            Min++;
            Sec = 0;
        }
        Txt_Timer.text = ((int)Min).ToString() +" : "+ ((int)Sec).ToString();

    }
}
