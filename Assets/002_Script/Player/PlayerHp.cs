using System;
using UnityEngine;

public class PlayerHp : MonoBehaviour, IDamageable, IResettable
{
    [Header("Ref")]
    [SerializeField] private UITopDown SaveAction;

    [SerializeField]private PlayerState MyState;
    [SerializeField] private bool IsGround;
    [SerializeField] private float StunDelay;
    [SerializeField] private float MaxHp;
    [SerializeField] private float CurrentHp;

    [SerializeField] private Rigidbody2D MyRigidbody2d;
    private float gravityScale;
    private int collisionCount;
    public int CollsionCount => collisionCount;
    //public bool IsStunFinished => MyState == PlayerState.Stun && Time.time - StunStartTime >= StunDelay;
    public event Action<PlayerState> StateChange;

    public bool IsDead => CurrentHp <= 0;
    public PlayerState State => MyState;
    public float Delay => StunDelay;
    public bool NotMove => MyState == PlayerState.Stun ;
    public bool Climbing => MyState == PlayerState.Climbing;
    
    private void Awake()
    {
        MyState = PlayerState.Idle;
        collisionCount = 0;
        MaxHp = 1;
        CurrentHp = 1;
        gravityScale = MyRigidbody2d.gravityScale;
    }

    private void OnEnable()
    {
        if (SavePointManager.Instance == null) return;
        SavePointManager.Instance.Register(this);
    }
    private void OnDisable()
    {
        if (SavePointManager.Instance == null) return;
        SavePointManager.Instance.Unregister(this);
    }

    public void ChangeState(PlayerState state)
    {
        if(state == PlayerState.Stun)
        {
            collisionCount++;
            MyRigidbody2d.gravityScale = gravityScale * 2;
        }
        else if(state == PlayerState.Climbing)
        {

        }
        else
        {
            MyRigidbody2d.gravityScale = gravityScale;
        }
        if (MyState == state) return;
        MyState = state;
        StateChange?.Invoke(MyState);
    }

    public void TakeDamage(int Damage)
    {
        if (IsDead) return;
        CurrentHp = Mathf.Clamp(CurrentHp - Damage, 0, MaxHp);
        if(IsDead)
        {
            ChangeState(PlayerState.Dead);
            if(SaveAction != null)
            {
                SaveAction.SavePointEnable();
            }
        }
        
    }

    public void ResetToSavePoint()
    {
        CurrentHp = MaxHp;
        ChangeState(PlayerState.Idle);
    }
}
