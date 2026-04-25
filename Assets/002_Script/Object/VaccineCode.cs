using System;
using UnityEngine;

public class VaccineCode : MonoBehaviour, IInteractable
{
    [Header("Ref")]
    [SerializeField] private AudioClip MySound;
    public event Action<VaccineCode> DestroyVCode;
    
    public void Interact(PlayerInteract Player)
    {
        if(PlayerData.Instance != null)
        {
            DestroyVCode?.Invoke(this);
            PlayerData.Instance.CodePickUp();
            if(MySound != null && SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySfxOneShot(MySound);
            }
            Destroy(this.gameObject);
        }
    }
}
