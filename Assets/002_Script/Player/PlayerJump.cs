using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] PlayerInputReader PlayerInputReader;
    [SerializeField] Rigidbody2D MyRigidbody;
    [SerializeField] PlayerHp MyPlayerHp;
    [SerializeField] PlayerClimbing MyPlayerClimbing;

    [Header("Settings")]
    [Tooltip("Script Default : 5.0f")][SerializeField] private float JumpPower = 5.0f;
    [Tooltip("Script Default : 5.0f")][SerializeField] private float RayCastDistance = 5.0f;
    [SerializeField] private LayerMask TargetLayer;
    [SerializeField] private bool IsGround;

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
        if (PlayerInputReader.JumpPressedThisFrame && !MyPlayerHp.NotMove 
            && (IsGround || MyPlayerClimbing.IsWall) && !MyPlayerClimbing.IsWallJump)
        {
            jumpRequested = true;
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
        if (MyPlayerClimbing.IsWall) ClimbingJump?.Invoke();

    }

    private void GroundCheck()
    {
        RaycastHit2D m_raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, RayCastDistance, TargetLayer);

        IsGround = m_raycastHit2D.collider != null;
        Ground?.Invoke(IsGround);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 m_Line = transform.position;
        m_Line.y -= RayCastDistance;

        Gizmos.DrawLine(transform.position , m_Line);
    }

}
