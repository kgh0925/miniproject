using UnityEngine;

public class VaccineAttack : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private VaccinePorjetile Prefab;
    [Header("Setting")]
    [SerializeField] private float AttackDelay;
    [SerializeField] private float AttackRange;
    [SerializeField] private LayerMask TargetLayer;
    
    private float LastAttackTime = -999f;

    public LayerMask GetLayer => TargetLayer;
    public void AttackTarget(GameObject Target)
    {
        TryFire(Target);
    }
    void TryFire(GameObject Target)
    {
        if(Prefab == null)
        {
            return;
        }
        if (Time.time < LastAttackTime + AttackDelay) return;
    
        LastAttackTime = Time.time;
        Vector2 FireDirection = (Target.transform.position - this.transform.position).normalized;
        Vector3 SpawnPosition = transform.position;
        VaccinePorjetile SpawnedProjectile = Instantiate(Prefab, SpawnPosition, Quaternion.identity);
        SpawnedProjectile.Initialize(FireDirection,TargetLayer);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamageable Target = collision.GetComponent<IDamageable>();
        if(Target != null && (((1 << collision.gameObject.layer) & TargetLayer) != 0)) 
        {
            if (Time.time >= LastAttackTime + AttackDelay) TryFire(collision.gameObject);
        }
    }
}
