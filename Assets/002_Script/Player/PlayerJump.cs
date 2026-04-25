using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] PlayerInputReader PlayerInputReader;
    [SerializeField] Rigidbody2D MyRigidbody;
    [SerializeField] PlayerHp MyPlayerHp;
    [SerializeField] PlayerClimbing MyPlayerClimbing;
    [SerializeField] AudioClip JumpSound;
    [SerializeField] private Collider2D GroundCheckCollider;
    

    [Header("Settings")]
    [Tooltip("Script Default : 5.0f")][SerializeField] private float JumpPower = 5.0f;
    [SerializeField] private LayerMask TargetLayer;
    [SerializeField] private bool IsGround;
    
    public bool GroundTrue => IsGround;
    public event Action ClimbingJump;
    public event Action<float> IsJump;
    public event Action<bool> Ground;
    private bool WasGround;
    private float StartGroundTime;
    private bool jumpRequested;
    
    private void Awake()
    {
        if (PlayerInputReader == null)
        {
            PlayerInputReader = GetComponent<PlayerInputReader>();
        }
        if (MyRigidbody == null)
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
        JumpSound = Resources.Load<AudioClip>("Sound/JumpSound");
    }

    private void Update()
    {
        GroundCheck();
        if (IsGround && !WasGround)
        {
            IsJump?.Invoke(0);
            if (MyPlayerHp.NotMove)
            {
                StartGroundTime = Time.time;
            }            
        }
        if(!IsGround || (StartGroundTime >= 0f && !MyPlayerHp.NotMove))
        {
            StartGroundTime = -999f;
        }
        if (IsGround && StartGroundTime >= 0f && MyPlayerHp.NotMove)
        {
            if(Time.time - StartGroundTime >= MyPlayerHp.Delay)
            {
                MyPlayerHp.ChangeState(PlayerState.Idle);
            }
        }
        if (MyPlayerClimbing == null)
        {
            if (PlayerInputReader.JumpPressedThisFrame && !MyPlayerHp.NotMove && IsGround)
            {
                jumpRequested = true;
            }
        }
        else
        {
            if (PlayerInputReader.JumpPressedThisFrame && !MyPlayerHp.NotMove
            && (IsGround || MyPlayerClimbing.IsWall) && !MyPlayerClimbing.IsWallJump)
            {
                jumpRequested = true;
            }
        }


        WasGround = IsGround;
    }
    private void FixedUpdate()
    {
        if (jumpRequested)
        {
            Jump();
            jumpRequested = false;
        }

    }

    private void Jump()
    {
        if(MyRigidbody == null)
        {
            Debug.Log("MyRigidbody == null");
            return;
        }
        //Debug.Log("Jump");
        MyRigidbody.linearVelocity = new Vector2(MyRigidbody.linearVelocityX, JumpPower);
        IsJump?.Invoke(JumpPower);
        if (MyPlayerClimbing != null &&MyPlayerClimbing.IsWall) ClimbingJump?.Invoke();
        if(JumpSound != null && SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySfxOneShot(JumpSound);
        }

    }

    private void GroundCheck()
    {
        if (GroundCheckCollider == null)
        {
            IsGround = false;
            Ground?.Invoke(IsGround);
            return;
        }

        IsGround = GroundCheckCollider.IsTouchingLayers(TargetLayer);
        Ground?.Invoke(IsGround);
    }
}
