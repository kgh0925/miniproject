using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask TargetLayer;
    [Tooltip("Default : 1")][SerializeField] private int Damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & TargetLayer) == 0)
        {
            return;
        }
        IDamageable Target = collision.gameObject.GetComponent<IDamageable>();
        if (Target == null) return;
        Target.TakeDamage(Damage);
        //PlayerInputReader playerInput = collision.GetComponent<PlayerInputReader>();
        //if(playerInput != null )
        //{
        //
        //}



    }
}
