using TMPro;
using UnityEngine;

public class AchiveText : MonoBehaviour, IInteractable
{
    [Header("Ref")]
    [SerializeField] private RectTransform UI_Object;
    [SerializeField] private TMP_Text Txt_Title;
    [SerializeField] private TMP_Text Txt_Description;

    [Header("Settings")]
    [SerializeField] private string Title;
    [SerializeField] private string Description;

    public void Interact(PlayerInteract Player)
    {
        if (Txt_Title == null || Txt_Description == null || UI_Object == null) return;
        UI_Object.gameObject.SetActive(true);
        Txt_Title.text = Title;
        Txt_Description.text = Description;
    }
}
