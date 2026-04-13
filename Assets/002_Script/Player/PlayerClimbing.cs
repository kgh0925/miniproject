using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClimbing : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private PlayerHp MyPlayerHp;
    [SerializeField] private Rigidbody2D MyRigidBody2D;
    [SerializeField] private PlayerJump MyPlayerJump;
    [SerializeField] private LayerMask TargetLayer;
    [Header("Settings")]
    [SerializeField] private float ClimbingGravityScale;
    [SerializeField] private float RayCastDistance;

    public event Action<bool,bool> Climbing;
    private float GravityScale;
    private bool IsLeftWall;
    private bool IsRightWall;
    private bool WasWall;
    private bool WallJump;

    public bool IsWallJump => WallJump;
    public bool LeftWall => IsLeftWall;
    public bool RightWall => IsRightWall;
    public bool IsWall => IsRightWall || IsLeftWall;
    
    private void Awake()
    {
        if(MyRigidBody2D == null)
        {
            MyRigidBody2D = GetComponent<Rigidbody2D>();
        }
        if(MyRigidBody2D != null)
        {
            GravityScale = MyRigidBody2D.gravityScale;
        }
        if(MyPlayerHp == null)
        {
            MyPlayerHp = GetComponent<PlayerHp>();
        }
        if (MyPlayerJump == null)
        {
            MyPlayerJump = GetComponent<PlayerJump>();
        }
    }
    private void OnEnable()
    {
        MyPlayerJump.ClimbingJump += WallJumped;
    }
    private void OnDisable()
    {
        MyPlayerJump.ClimbingJump -= WallJumped;
    }


    /*    private void OnCollisionEnter2D(Collision2D collision)
        {
            if(((1 << collision.gameObject.layer) & TargetLayer) == 0 || MyPlayerHp.NotMove)
            {
                return;
            }
            if(MyRigidBody2D != null)
            {
                MyPlayerHp.ChangeState(PlayerState.Climbing);
                MyRigidBody2D.gravityScale = ClimbingGravityScale;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & TargetLayer) == 0)
            {
                return;
            }
            if (MyRigidBody2D != null)
            {
                MyRigidBody2D.gravityScale = GravityScale;
                if(!MyPlayerHp.NotMove) MyPlayerHp.ChangeState(PlayerState.Idle);
            }
        }*/
    private void Update()
    {
        WallCheck();
        if(IsWall && MyPlayerHp.State == PlayerState.Idle)
        {
            MyPlayerHp.ChangeState(PlayerState.Climbing);
            Climbing?.Invoke(LeftWall, RightWall);
            MyRigidBody2D.gravityScale = ClimbingGravityScale;
            if(!WasWall)
            {
                MyRigidBody2D.linearVelocityY = 0f;
            }
        }
        else if(!IsWall && MyPlayerHp.Climbing)
        {
            MyPlayerHp.ChangeState(PlayerState.Idle);
            Climbing?.Invoke(LeftWall,RightWall);
            if (MyRigidBody2D.gravityScale != GravityScale)
            {
                MyRigidBody2D.gravityScale = GravityScale;
            }
            WallJump = false;
        }
        WasWall = IsWall;
    }
    private void WallCheck()
    {
        RaycastHit2D m_raycastHit2DLeft = Physics2D.Raycast(transform.position, Vector2.left, RayCastDistance, TargetLayer);
        RaycastHit2D m_raycastHit2DRight = Physics2D.Raycast(transform.position, Vector2.right, RayCastDistance, TargetLayer);
        IsLeftWall = m_raycastHit2DLeft.collider != null;
        IsRightWall = m_raycastHit2DRight.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Vector3 m_LineLeft = transform.position;
        m_LineLeft.x -= RayCastDistance;
        Vector3 m_LineRight = transform.position;
        m_LineRight.x += RayCastDistance;

        Gizmos.DrawLine(transform.position, m_LineLeft);
        Gizmos.DrawLine(transform.position, m_LineRight);
    }

    private void WallJumped()
    {
        WallJump = true;
    }
}
