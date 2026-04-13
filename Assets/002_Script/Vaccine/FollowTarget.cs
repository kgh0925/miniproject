using System;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Transform Target;
    [Header("Settings")]
    [SerializeField] private float Distance;
    [SerializeField] private float Speed;
    [SerializeField] private Direction CurrentDirection;

    public event Action<Direction> ChangeDirection;

    private void FixedUpdate()
    {
        if(Target != null && !DistanceCheck())
        {
            Tracking();
        }
    }

    private bool DistanceCheck()
    {
        if ((this.transform.position.y - Target.transform.position.y <= Distance) &&
            (this.transform.position.y - Target.transform.position.y >= (Distance / 2)))
        {
            //내 위치가 타겟의 머리 위에서 Distance안에 있어야함 
            //Debug.Log("1조건 만족");
            {
                if (Mathf.Abs(this.transform.position.x - Target.transform.position.x) <= Distance) return true;
                //Debug.Log("2조건 미만족");
            }
        }
        return false;
    }

    private void Tracking()
    {
        //Debug.Log("Tracking");
        Vector3 TargetPosition = Target.position;
        TargetPosition.y += Distance;
        Vector2 NewPosition = (TargetPosition - this.transform.position).normalized;
        if(NewPosition.x <= 0.1f)
        {
            if(CurrentDirection == Direction.Right)
            {
                ChangeDirection?.Invoke(Direction.Left);
            }
            CurrentDirection = Direction.Left;
            
        }
        else{
            if (CurrentDirection == Direction.Left)
            {
                ChangeDirection?.Invoke(Direction.Right);
            }
            CurrentDirection = Direction.Right;
        }
        
        transform.position = (Vector2)transform.position + NewPosition * Speed * Time.fixedDeltaTime;
        //Vector2.Lerp()
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * Distance);
    }
}
