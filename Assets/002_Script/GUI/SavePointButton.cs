using UnityEngine;

public class SavePointButton : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private GameObject Player;

    public void SaveButtonClick()
    {
        if(Player != null && SavePointManager.Instance != null)
        {
            SavePointManager.Instance.ReturnToSavePoint(Player);
        }
    }
}
