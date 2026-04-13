using UnityEngine;

public class VaccineAni : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private FollowTarget VaccineMove;
    [SerializeField] private Animator MyAnimator;

    [Header("Settings")]
    [SerializeField] private string ParameterName_Left;
    [SerializeField] private string ParameterName_Right;

    private int LeftHash;
    private int RightHash;
    private void Awake()
    {
        if(VaccineMove == null)
        {
            VaccineMove = GetComponent<FollowTarget>();
        }
    }
    private void Start()
    {
        HashSetUp();
    }

    private void OnEnable()
    {
        VaccineMove.ChangeDirection += AniChange;
    }
    private void OnDisable()
    {
        VaccineMove.ChangeDirection -= AniChange;
    }

    private void HashSetUp()
    {
        LeftHash = Animator.StringToHash(ParameterName_Left);
        RightHash = Animator.StringToHash(ParameterName_Right);
    }
    private void AniChange(Direction direction)
    {
        if (MyAnimator == null) return;
        if (direction == Direction.Left)
        {
            MyAnimator.SetBool(LeftHash, true);
            MyAnimator.SetBool(RightHash, false);
        }
        else if(direction == Direction.Right)
        {
            MyAnimator.SetBool(RightHash, true);
            MyAnimator.SetBool(LeftHash, false);
        }

    }
}
