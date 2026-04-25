using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private PlayerInputReader MyInputReader;
    [SerializeField] private Transform GameMenuUI;
    //[SerializeField] private ScreenTopDown MyScreenTopDown;
    private bool IsOpened;
    private void Awake()
    {
        if(MyInputReader == null)
        {
            MyInputReader = FindFirstObjectByType<PlayerInputReader>();
        }
    }

    private void Update()
    {
        if(MyInputReader.GameMenuPressedThisFrame)
        {
            ActiveUI();
        }
    }
    public void ActiveUI()
    {
        IsOpened = !IsOpened;
        GameMenuUI.gameObject.SetActive(IsOpened); // 열려있으면 닫기, 닫아져있으면 열기 
        Time.timeScale = IsOpened ? 0 : 1;
    }
/*    public void SceneLoadUI()
    {
        if (MyScreenTopDown == null) return;
        IsOpened = !IsOpened;
        Time.timeScale = IsOpened ? 0 : 1;
    }*/
}
