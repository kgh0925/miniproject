using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask TargetLayer;
    [Tooltip("Default : 1")][SerializeField] private int Damage = 1;
    private float AttackDelay = 3f;
    private float LastAttackTime = -999f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & TargetLayer) == 0)
        {
            return;
        }
        IDamageable Target = collision.gameObject.GetComponent<IDamageable>();
        if (Target == null) return;
        if (LastAttackTime + AttackDelay > Time.time) return;
        Target.TakeDamage(Damage);
        LastAttackTime = Time.time;
        //PlayerInputReader playerInput = collision.GetComponent<PlayerInputReader>();
        //if(playerInput != null )
        //{
        //
        //}



    }
}
