using Unity.VisualScripting;
using UnityEngine;

public class SoundPass : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private AudioClip MyClip;

    private void Start()
    {
        if(SoundManager.Instance != null && MyClip != null && !SoundManager.Instance.IsSamePlayerBGM(MyClip))
        {
            SoundManager.Instance.PlayBgm(MyClip);
        }
    }
}
