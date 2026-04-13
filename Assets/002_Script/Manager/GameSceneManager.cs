using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneByName(string SceneName)
    {
        if(!IsValidSceneName(SceneName))
        {
            return;
        }
        if (!Application.CanStreamedLevelBeLoaded(SceneName))
        {
            Debug.LogWarning($"[GameSceneManager] LoadSceneByName 실패: Build Settings에 없는 씬입니다. sceneName={SceneName}");
            return;
        }

        SceneManager.LoadScene(SceneName);
    }

    public AsyncOperation LoadSceneAsyncByName(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        if (!IsValidSceneName(sceneName))
        {
            return null;
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogWarning($"[GameSceneManager] LoadSceneAsyncByName 실패: Build Settings에 없는 씬입니다. sceneName={sceneName}");
            return null;
        }

        return SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
    }

    /// <summary>
    /// sceneName이 비어 있지 않은지 검사한다.
    /// </summary>
    private bool IsValidSceneName(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogWarning("[GameSceneManager] sceneName이 비어 있습니다.");
            return false;
        }

        return true;
    }
}
