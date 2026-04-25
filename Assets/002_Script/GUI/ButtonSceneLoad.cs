using System.Collections;
using UnityEngine;

/*public class ButtonSceneLoad : MonoBehaviour
{
    //[Header("Ref")]
    //[SerializeField] ScreenTopDown MyScreenTopDown;
    [Header("Load Scene Name")]
    [SerializeField] private string SceneName;
    [Tooltip("Write only to stage buttons")][SerializeField] private string IntroSceneName;
    [SerializeField] private bool FirstStageButton = false;
    public void ButtonClick()
    {

        if(GameSceneManager.Instance != null)
        {
            if(FirstStageButton && PlayerData.Instance != null)
            {
                
                PlayerData.Instance.PlayerCurrentStageScene(SceneName);

                if (!PlayerData.Instance.Intro)
                {
                    GameSceneManager.Instance.LoadSceneByName(IntroSceneName);
                    //Screen(IntroSceneName);
                    return;
                }
                
            }
            GameSceneManager.Instance.LoadSceneByName(SceneName);
            //Screen(SceneName);
        }
    }

    //public void Screen(string Name)
    //{
    //    if (MyScreenTopDown == null) return;
    //    StartCoroutine(SceneRoad(Name));
    //}
    //private IEnumerator SceneRoad(string Name)
    //{
    //    yield return MyScreenTopDown.Close();
    //    yield return new WaitForSecondsRealtime(MyScreenTopDown.Duration);
    //    GameSceneManager.Instance.LoadSceneByName(Name);
    //
    //}
   
}*/

public class ButtonSceneLoad : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private ScreenTopDown MyScreenTopDown;
    [SerializeField] private CanvasGroup MyCanvasGroup;
    [SerializeField] private PlayerInputReader MyPlayerInputReader;
    [Header("Load Scene Name")]
    [Tooltip("Write only to stage buttons")][SerializeField] private string IntroSceneName;
    [SerializeField] private string FirstStageName;
    public void ButtonClick(string SceneName)
    {

        if (GameSceneManager.Instance != null)
        {
            if (PlayerData.Instance != null)
            {

                PlayerData.Instance.PlayerCurrentStageScene(SceneName);

                if (!PlayerData.Instance.Intro && SceneName == FirstStageName)
                {
                    Screen(IntroSceneName);
                    return;
                }

            }
            Screen(SceneName);
        }
    }

    public void Screen(string Name)
    {
        if (MyScreenTopDown == null) return;
        StartCoroutine(SceneRoad(Name));
    }
    private IEnumerator SceneRoad(string Name)
    {
        if(MyPlayerInputReader != null)
        {
            MyPlayerInputReader.enabled = false;
        }
        if(MyCanvasGroup != null)
        {
            MyCanvasGroup.blocksRaycasts = false; // 클릭 막기
            MyCanvasGroup.interactable = false;   // 버튼 비활성
        }
        yield return MyScreenTopDown.Close();
        yield return new WaitForSecondsRealtime(MyScreenTopDown.Duration); 
        if (MyPlayerInputReader != null)
        {
            MyPlayerInputReader.enabled = true;
        }
        if (MyCanvasGroup != null)
        {
            MyCanvasGroup.blocksRaycasts = true; // 클릭 막기
            MyCanvasGroup.interactable = true;   // 버튼 비활성
        }
        GameSceneManager.Instance.LoadSceneByName(Name);
    
    }

}
