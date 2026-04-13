using System;
using UnityEngine;

public class VaccineCode : MonoBehaviour, IInteractable
{
    public event Action<VaccineCode> DestroyVCode;
    public void Interact(PlayerInteract Player)
    {
        if(PlayerData.Instance != null)
        {
            DestroyVCode?.Invoke(this);
            PlayerData.Instance.CodePickUp();
            Destroy(this.gameObject);
        }
    }
}
