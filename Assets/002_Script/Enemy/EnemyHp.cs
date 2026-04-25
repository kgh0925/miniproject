using System;
using UnityEngine;

public class EnemyHp : MonoBehaviour, IDamageable
{
    [SerializeField] private float MaxHp;
    [SerializeField] private float CurrentHp;
    public event Action Dead;
    private void Awake()
    {
        CurrentHp = MaxHp;
    }
    public bool IsDead => CurrentHp <= 0;
    public void TakeDamage(int Damage)
    {
        CurrentHp = Mathf.Clamp(CurrentHp - Damage, 0, MaxHp);
        if (IsDead) Dead?.Invoke();
    }
    public void Init()
    {
        CurrentHp = MaxHp;
    }
}
