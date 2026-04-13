using UnityEngine;

public class BossAni : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Animator MyAnimator;
    [SerializeField] private BossMove MyBossMove;

    [Header("Settings")]
    [SerializeField] private string ChangeParameterName;

    private int ChangeHashSet;
    private void Awake()
    {
        if(MyAnimator == null)
        {
            MyAnimator = GetComponent<Animator>();
        }
        if(MyBossMove == null)
        {
            MyBossMove = GetComponent<BossMove>();
        }
    }

    private void OnEnable()
    {
        MyBossMove.DirectionChanged += ChangeAni;
        ChangeHashSet = Animator.StringToHash(ChangeParameterName);
    }
    private void OnDisable()
    {
        MyBossMove.DirectionChanged -= ChangeAni;
    }

    private void ChangeAni(Direction direction)
    {
        if(direction == Direction.Left)
        {
            MyAnimator.SetBool(ChangeHashSet, true);
        }
        else if (direction == Direction.Right)
        {
            MyAnimator.SetBool(ChangeHashSet, false);
        }
    }
}
