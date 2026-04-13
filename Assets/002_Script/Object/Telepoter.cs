using UnityEngine;

public class Telepoter : MonoBehaviour, IInteractable
{
    [Header("Ref")]
    [SerializeField] Telepoter TargetObject;

    [Header("Settings")]
    [SerializeField] private float CoolDown;
    private float LastTeloportTime = -999f;


    private void Awake()
    {
        if(TargetObject == null)
        {
            Debug.LogWarning("TargetObject == null");
            return;
        }
    }

    public void Interact(PlayerInteract Player)
    {
        if (Time.time > LastTeloportTime + CoolDown)
        {
            Player.gameObject.transform.position = TargetObject.transform.position;
            LastTeloportTime = Time.time;
            TargetObject.SyncTime(LastTeloportTime);
        }
        
    }

    public void SyncTime(float time)
    {
        LastTeloportTime = time;
    }


}
