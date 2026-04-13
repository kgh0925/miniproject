using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Transform Target;
    [Header("Settings")]
    [Tooltip("Z Position")] [SerializeField] private float CameraRange;
    [SerializeField] private float Smoothing = 0.02f;

    private void FixedUpdate()
    {
        if(Mathf.Abs(this.transform.position.y - Target.position.y) <= 20)
        {
            Vector3 TargetPos = new Vector3(Target.position.x, this.transform.position.y, CameraRange);
            this.transform.position = Vector3.Lerp(transform.position, TargetPos, Smoothing);
        }
        else
        {
            Vector3 TargetPos = new Vector3(Target.position.x, Target.position.y, CameraRange);
            this.transform.position = Vector3.Lerp(transform.position, TargetPos, Smoothing);
        }
    }
}
