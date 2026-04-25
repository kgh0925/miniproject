using UnityEngine;

public class PlayerAniCon : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Animator MyAnimator;
    [Tooltip("Stun")][SerializeField] private PlayerHp StunState;
    [Tooltip("Move & Run")][SerializeField] private PlayerMove2D MoveState;
    [Tooltip("Jump & Ground")][SerializeField] private PlayerJump JumpState;
    [Tooltip("Climbing")][SerializeField] private PlayerClimbing ClimbingState;
    [SerializeField] private SpriteRenderer MySpriteRenderer;

    [Header("Settings")]
    [SerializeField] private string JumpParameter;
    [SerializeField] private string WalkParameter;
    [SerializeField] private string RunParameter;
    [SerializeField] private string GroundParameter;
    [SerializeField] private string StunParameter;
    [SerializeField] private string ClimbingParameter;
    [SerializeField] private string IsMoveParameter;

    private int JumpHash;
    private int WalkHash;
    private int RunHash;
    private int ClimbingHash;
    private int StunHash;
    private int GroundHash;
    private int IsMoveHash;

    private void Start()
    {
        HashSetUp();
    }

    private void OnEnable()
    {
        StunState.StateChange += StateAni;
        JumpState.Ground += this.Ground;
        JumpState.IsJump += this.Jump;
        MoveState.MoveX += MoveAni;
        MoveState.IsRun += RunAni;
        if(ClimbingState != null) ClimbingState.Climbing += ClimbingAni;
    }
    private void OnDisable()
    {
        StunState.StateChange -= StateAni;
        JumpState.Ground -= this.Ground;
        JumpState.IsJump -= this.Jump;
        MoveState.MoveX -= MoveAni;
        MoveState.IsRun -= RunAni;
        if (ClimbingState != null) ClimbingState.Climbing -= ClimbingAni;
    }

    private void Update()
    {
        if(StunState.State == PlayerState.Stun) // 스턴 정방향
        {
            MySpriteRenderer.flipX = false;
        }
        else if(ClimbingState != null && ClimbingState.LeftWall) // 왼쪽벽만 유일하게 왼쪽방향을 봐도 플립 X ( 기본 스프라이트가 왼쪽 )
        {
            MySpriteRenderer.flipX = false;
        }
        else if(ClimbingState != null && ClimbingState.RightWall) // 오른쪽벽은 플립 해줘야함
        {
            MySpriteRenderer.flipX = true;
        }
        else if (MoveState.DIRECTION == Direction.Left) // 나머지는 전부 왼쪽 방향이면 뒤집어줘야함
        {
            MySpriteRenderer.flipX = true;
        }
        else // 왼쪽 아니면 전부 플립 X
        {
            MySpriteRenderer.flipX = false;
        }
    }

    private void HashSetUp()
    {
        JumpHash = Animator.StringToHash(JumpParameter);
        WalkHash = Animator.StringToHash(WalkParameter);
        RunHash = Animator.StringToHash(RunParameter);
        ClimbingHash = Animator.StringToHash(ClimbingParameter);
        StunHash = Animator.StringToHash(StunParameter);
        GroundHash = Animator.StringToHash(GroundParameter);
        IsMoveHash = Animator.StringToHash(IsMoveParameter);
    }
    private void MoveAni(float MoveX)
    {
        MyAnimator.SetFloat(WalkHash, MoveX);
        if (MoveX == 0)
        {
            MyAnimator.SetBool(IsMoveHash, false);
        }
        else
        {
            MyAnimator.SetBool(IsMoveHash, true);
        }
    }
    private void RunAni(bool IsRun)
    {
        MyAnimator.SetBool(RunHash, IsRun);
    }
    private void Jump(float VelocityY)
    {
        MyAnimator.SetBool(GroundHash, false);
        MyAnimator.SetFloat(JumpHash, VelocityY);
    }
    private void Ground(bool IsGround)
    {
        MyAnimator.SetBool(GroundHash, IsGround);
    }
    private void StateAni(PlayerState state)
    {
        if (state == PlayerState.Stun && MyAnimator != null)
        {
            MyAnimator.SetBool(StunHash, true);
            MyAnimator.SetBool(ClimbingHash, false);

        }
        else if (state != PlayerState.Stun && MyAnimator != null)
        {
            MyAnimator.SetBool(StunHash, false);
        }
    }
    private void ClimbingAni(bool left, bool right)
    {
        MyAnimator.SetBool(ClimbingHash, left || right);
    }
}
