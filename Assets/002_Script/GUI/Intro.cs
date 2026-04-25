using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private ButtonSceneLoad MyButtonSceneLoad;
    [SerializeField] private TMP_Text Txt_Main;
    [SerializeField] private Image UI_Image;
    [SerializeField] private TMP_Text Txt_Time;
    [SerializeField] private Slider MySlider;

    [Header("Setting")]
    [SerializeField] private string StageName;
    private float Sec = 60f;
    private void Start()
    {
        if(PlayerData.Instance != null)
        {
            PlayerData.Instance.IntroTrue();
        }
    }
    private void Update()
    {
        Sec -= Time.deltaTime;
        MySlider.value = (1 - (Sec / 60f));
        Txt_Time.text = "남은 시간 : " + ((int)Sec).ToString() + "초";
        if(Sec >= 57f)
        {
            Txt_Main.text = "메시지 작성 중...";
        }
        else if(Sec >= 54f)
        {
            Txt_Main.text = "메시지 전송 중...";
        }
        else if(Sec >= 51f)
        {
            Txt_Main.text = "이모티콘 다운로드 중...";
            UI_Image.enabled = true;
        }
        else if(Sec >= 48f)
        {
            Txt_Main.text = "이모티콘 파일 손상...!";
            UI_Image.color = Color.red;
        }
        else if(Sec >= 45f)
        {
            Txt_Main.text = "바이러스 다운로드 중...!";
        }
        else if(Sec >= 43f)
        {
            UI_Image.color = Color.white;
        }
        else if(Sec <= 41f)
        {
            Vector3 pos = UI_Image.rectTransform.position;
            pos.y -= 50f * Time.deltaTime;
            UI_Image.rectTransform.position = pos;
            if(Sec <= 37f && MyButtonSceneLoad != null)
            {
                MyButtonSceneLoad.Screen(PlayerData.Instance.SceneName);
            }
        }
        
    }
}
