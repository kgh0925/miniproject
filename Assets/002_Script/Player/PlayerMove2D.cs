using System;
using UnityEngine;


public class PlayerMove2D : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] PlayerInputReader PlayerInputReader;
    [SerializeField] Rigidbody2D MyRigidbody;
    [SerializeField] PlayerHp MyPlayerHp;
    [SerializeField] PlayerClimbing MyPlayerClimbing;
    

    [Header("Setting")]
    [Tooltip("Script Default : 40.0f [MaxSpeed]")] [SerializeField] private float MoveSpeed = 40.0f;
    [Tooltip("Script Default : 5.0f")][SerializeField] private float Acceleration = 5.0f;
    [Tooltip("Script Default : 10.0f")][SerializeField] private float RunAddSpeed = 10.0f;

    [Header("Inspector View")]
    [SerializeField] Direction CurrentDirection = Direction.Right;
    [SerializeField] private float CurrentSpeed;

    public event Action<float> MoveX;
    public event Action<bool> IsRun;
    public Direction DIRECTION => CurrentDirection;
    Vector2 InputDirection;

    private void Awake()
    {
        if(PlayerInputReader == null)
        {
            PlayerInputReader = GetComponent<PlayerInputReader>();
        }
        if(MyRigidbody == null)
        {
            MyRigidbody = GetComponent<Rigidbody2D>();
        }
        if (MyPlayerHp == null)
        {
            MyPlayerHp = GetComponent<PlayerHp>();
        }
        if (MyPlayerClimbing == null)
        {
            MyPlayerClimbing = GetComponent<PlayerClimbing>();
        }
    }
    private void Update()
    {
        InputDirection = PlayerInputReader != null ? PlayerInputReader.MoveVector.normalized : Vector2.zero;
    }

    private void FixedUpdate()
    {
        if(MyPlayerHp.NotMove)
        {
            NuckBack();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        //Vector2 NewPosition = MyRigidbody.position + InpuerDirection * MoveSpeed * Time.fixedDeltaTime;
        //MyRigidbody.MovePosition(NewPosition);
        if (InputDirection == Vector2.zero) CurrentSpeed = 0;
        else
        {
            CurrentSpeed += Acceleration * Time.fixedDeltaTime;
        }
        CurrentSpeed = PlayerInputReader.RunIsPressed ?
            (Mathf.Clamp(CurrentSpeed, 10f + RunAddSpeed, MoveSpeed + RunAddSpeed))
            : Mathf.Clamp(CurrentSpeed, 10f, MoveSpeed);
        if(PlayerInputReader.RunIsPressed)
        {
            IsRun?.Invoke(true);
        }
        else
        {
            IsRun?.Invoke(false);
        }
            bool LeftWall = MyPlayerClimbing.LeftWall && InputDirection.x < 0;
        bool RightWall = MyPlayerClimbing.RightWall && InputDirection.x > 0;
        bool IsWall = LeftWall || RightWall;
        float NewPosition = IsWall ? 0f : InputDirection.x * CurrentSpeed;
        MyRigidbody.linearVelocity = new Vector2(NewPosition, MyRigidbody.linearVelocityY);
        UpdateDirection();
        MoveX?.Invoke(NewPosition);
    }
    private void NuckBack()
    {
        float NewPosition = Mathf.Lerp(MyRigidbody.linearVelocityX, 0, 0.01f);
        MyRigidbody.linearVelocity = new Vector2(NewPosition, MyRigidbody.linearVelocityY);
    }

    private void UpdateDirection()
    {
        if (InputDirection.sqrMagnitude < 0.01f) return;

        float x = InputDirection.x;
        float y = InputDirection.y;

        if (x > 0.1f)
        {
            if (y > 0.1f) CurrentDirection = Direction.RightUp;
            else if (y < -0.1f) CurrentDirection = Direction.RightDown;
            else CurrentDirection = Direction.Right;
        }
        else if (x < -0.1f)
        {
            if (y > 0.1f) CurrentDirection = Direction.LeftUp;
            else if (y < -0.1f) CurrentDirection = Direction.LeftDown;
            else CurrentDirection = Direction.Left;
        }
        else
        {
            if (y > 0.1f) CurrentDirection = Direction.Up;
            else if (y < -0.1f) CurrentDirection = Direction.Down;
            else CurrentDirection = Direction.None;
        }
    }

}
