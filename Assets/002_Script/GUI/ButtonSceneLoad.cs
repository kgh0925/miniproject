using UnityEngine;

public class ButtonSceneLoad : MonoBehaviour
{
    [Header("Load Scene Name")]
    [SerializeField] private string SceneName;

    public void ButtonClick()
    {
        if(GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.LoadSceneByName(SceneName);
        }
    }
}
