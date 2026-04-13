using UnityEngine;

public class PlayerAniController : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Animator MyAnimator;
    [Tooltip("Stun")] [SerializeField] private PlayerHp StunState;
    [Tooltip("Move & Run")][SerializeField] private PlayerMove2D MoveState;
    [Tooltip("Jump & Ground")][SerializeField] private PlayerJump JumpState;
    [Tooltip("Climbing")][SerializeField] private PlayerClimbing ClimbingState;

    [Header("Settings")]
    [SerializeField] private string JumpParameter;
    [SerializeField] private string WalkParameter;
    [SerializeField] private string RunParameter;
    [SerializeField] private string GroundParameter;
    [SerializeField] private string StunParameter;
    [SerializeField] private string LeftClimbingParameter;
    [SerializeField] private string RightClimbingParameter;
    [SerializeField] private string IsMoveParameter;

    private int JumpHash;
    private int WalkHash;
    private int RunHash;
    private int LeftClimbingHash;
    private int RightClimbingHash;
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
        ClimbingState.Climbing += ClimbingAni;
    }
    private void OnDisable()
    {
        StunState.StateChange -= StateAni;
        JumpState.Ground -= this.Ground;
        JumpState.IsJump -= this.Jump;
        MoveState.MoveX -= MoveAni;
        MoveState.IsRun -= RunAni;
        ClimbingState.Climbing -= ClimbingAni;
    }

    private void HashSetUp()
    {
        JumpHash = Animator.StringToHash(JumpParameter);
        WalkHash = Animator.StringToHash(WalkParameter);
        RunHash = Animator.StringToHash(RunParameter);
        LeftClimbingHash = Animator.StringToHash(LeftClimbingParameter);
        RightClimbingHash = Animator.StringToHash(RightClimbingParameter);
        StunHash = Animator.StringToHash(StunParameter);
        GroundHash = Animator.StringToHash(GroundParameter);
        IsMoveHash = Animator.StringToHash(IsMoveParameter);
    }
    private void MoveAni(float MoveX)
    {
        MyAnimator.SetFloat(WalkHash, MoveX);
        if(MoveX == 0)
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
        if(state == PlayerState.Stun && MyAnimator != null)
        {
            MyAnimator.SetBool(StunHash, true);
            MyAnimator.SetBool(LeftClimbingHash, false);
            MyAnimator.SetBool(RightClimbingHash, false);

        }
        else if(state != PlayerState.Stun && MyAnimator != null)
        {
            MyAnimator.SetBool(StunHash, false);
        }
    }
    private void ClimbingAni(bool Left, bool Right)
    {
        MyAnimator.SetBool(LeftClimbingHash, Left);
        MyAnimator.SetBool(RightClimbingHash, Right);
    }
    
}
