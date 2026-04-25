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
    [SerializeField] private Collider2D LeftClimbingCollider2D;
    [SerializeField] private Collider2D RightClimbingCollider2D;
    [Header("Settings")]
    [SerializeField] private float ClimbingGravityScale;

    public event Action<bool,bool> Climbing;
    private float GravityScale;
    [SerializeField]private bool IsLeftWall;
    [SerializeField]private bool IsRightWall;
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
        else if(!IsWall && MyPlayerHp.Climbing) //벽이 아니였는데 플레이어가 벽 타는경우
        {
            MyPlayerHp.ChangeState(PlayerState.Idle);
            Climbing?.Invoke(LeftWall,RightWall);
            if (MyRigidBody2D.gravityScale != GravityScale)
            {
                MyRigidBody2D.gravityScale = GravityScale;
            }
            WallJump = false;
        }
        else if(MyPlayerHp.NotMove && WallJump)
        {
            WallJump = false;
        }
            WasWall = IsWall;
    }
    private void WallCheck()
    {
        if (LeftClimbingCollider2D == null || RightClimbingCollider2D == null)
        {
            return;
        }
        IsLeftWall = LeftClimbingCollider2D.IsTouchingLayers(TargetLayer);
        IsRightWall = RightClimbingCollider2D.IsTouchingLayers(TargetLayer);
    }

    private void WallJumped()
    {
        WallJump = true;
    }
}
