using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private PlayerInputReader MyInputReader;
    [SerializeField] private Transform GameMenuUI;
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
            if(IsOpened)
            {
                GameMenuUI.gameObject.SetActive(false);
            }
            else
            {
                GameMenuUI.gameObject.SetActive(true);
            }
            IsOpened = !IsOpened;
        }
    }
}
