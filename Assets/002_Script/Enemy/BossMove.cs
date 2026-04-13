using System;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Transform TargetObject;
    [SerializeField] private float MoveSpeed;


    [Header("Inspector View")]
    [SerializeField] private Direction CurrentDirection;
    private Direction WasDirection;
    public event Action<Direction> DirectionChanged;
    

    private void Update()
    {
        if (TargetObject == null)
        {
            GameObject Target = GameObject.FindGameObjectWithTag("Player");
            if (Target == null) return;
            TargetObject = Target.transform;
        }
        HandleMove();
    }

    private void HandleMove()
    {
        Vector2 NewPosition = (TargetObject.position - this.transform.position).normalized;
        transform.position = (Vector2)transform.position + NewPosition * MoveSpeed * Time.deltaTime;
        UpdateDirection(NewPosition);
    }

    private void UpdateDirection(Vector2 Position)
    {
        if (Position.sqrMagnitude < 0.01f) return;

        float x = Position.x;
        float y = Position.y;

        if (x > 0.1f)
        {
            CurrentDirection = Direction.Right;
        }
        else if (x < -0.1f)
        {
            CurrentDirection = Direction.Left;
        }
        if(WasDirection != CurrentDirection)
        {
            DirectionChanged?.Invoke(CurrentDirection);
        }
        WasDirection = CurrentDirection;
    }
}
