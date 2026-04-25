using System;
using System.Collections;
using UnityEngine;


public class PlayerMove2D : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] PlayerInputReader PlayerInputReader;
    [SerializeField] Rigidbody2D MyRigidbody;
    [SerializeField] PlayerHp MyPlayerHp;
    [SerializeField] PlayerClimbing MyPlayerClimbing;
    [SerializeField] private Collider2D LeftCollider2D;
    [SerializeField] private Collider2D RightCollider2D;
    [SerializeField] private LayerMask TargetLayer;


    [Header("Setting")]
    [Tooltip("Script Default : 40.0f [MaxSpeed]")] [SerializeField] private float MoveSpeed = 40.0f;
    [Tooltip("Script Default : 5.0f")][SerializeField] private float Acceleration = 5.0f;
    [Tooltip("Script Default : 10.0f")][SerializeField] private float RunAddSpeed = 10.0f;
    [SerializeField] private bool RunActive;

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
    //private void OnEnable()
    //{
    //    if (SavePointManager.Instance == null) return;
    //    SavePointManager.Instance.Register(this);
    //}
    //private void OnDisable()
    //{
    //    if (SavePointManager.Instance == null) return;
    //    SavePointManager.Instance.Unregister(this);
    //}
    private void Update()
    {
        InputDirection = PlayerInputReader != null  ? PlayerInputReader.MoveVector.normalized : Vector2.zero;
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
        if(RunActive)
        {
            CurrentSpeed = PlayerInputReader.RunIsPressed ?
            (Mathf.Clamp(CurrentSpeed, 20f + RunAddSpeed, MoveSpeed + RunAddSpeed))
            : Mathf.Clamp(CurrentSpeed, 20f, MoveSpeed);

            if (PlayerInputReader.RunIsPressed)
            {
                IsRun?.Invoke(true);
            }
            else
            {
                IsRun?.Invoke(false);
            }
        }
        else
        {
            CurrentSpeed = Mathf.Clamp(CurrentSpeed, 20f, MoveSpeed);
        }


        float NewPosition;
        if (MyPlayerClimbing != null)
        {
            bool LeftWall = MyPlayerClimbing.LeftWall && InputDirection.x < 0;
            bool RightWall = MyPlayerClimbing.RightWall && InputDirection.x > 0;
            bool IsWall = LeftWall || RightWall;
            NewPosition = IsWall ? 0f : InputDirection.x * CurrentSpeed;
        }
        else if(LeftCollider2D != null && RightCollider2D != null)
        {
            bool LeftGround = LeftCollider2D.IsTouchingLayers(TargetLayer) && InputDirection.x < 0;
            bool RightGround = RightCollider2D.IsTouchingLayers(TargetLayer) && InputDirection.x > 0;
            bool SideGround = LeftGround || RightGround;
            NewPosition = SideGround ? 0f : InputDirection.x * CurrentSpeed;
        }
        else
        {
            NewPosition = InputDirection.x * CurrentSpeed;
        }
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

    //public void ResetToSavePoint()
    //{
    //    StartCoroutine(RespawnCorutine());
    //}
    //IEnumerator RespawnCorutine()
    //{
    //    Respawn = true;
    //    yield return new WaitForSecondsRealtime(RespawnTime);
    //    Respawn = false;
    //}
}
