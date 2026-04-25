using UnityEngine;

public class ErrorGuard : MonoBehaviour
{
    [SerializeField] BoxCollider2D MyBox;
    [SerializeField] UITopDown MyUITopDown;
    private float LastenabledTime = -999f;
    private float GuardTime = 5f;
    private void Update()
    {
        if (MyBox == null && MyUITopDown == null) return;
        if(MyBox.enabled)
        {
            LastenabledTime = Time.time;
        }
        else if(!MyBox.enabled && LastenabledTime + GuardTime <= Time.time)
        {
            MyBox.enabled = true;
            MyUITopDown.SavePointEnable();
        }
    }
}
