using System;
using UnityEngine;

public class PlayerHp : MonoBehaviour, IDamageable
{

    [SerializeField]private PlayerState MyState;
    [SerializeField] private bool IsGround;
    [SerializeField] private float StunDelay;
    [SerializeField] private float MaxHp;
    [SerializeField] private float CurrentHp;
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
        MaxHp = 1;
        CurrentHp = 1;
    }
    public void ChangeState(PlayerState state)
    {

        if (MyState == state) return;
        MyState = state;
        StateChange?.Invoke(MyState);


    }

    public void TakeDamage(int Damage)
    {
        CurrentHp = Mathf.Clamp(CurrentHp - Damage, 0, MaxHp);
        if(IsDead)
        {
            ChangeState(PlayerState.Dead);
        }
        
    }
}
