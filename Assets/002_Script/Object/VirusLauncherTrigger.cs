using UnityEngine;

public class VirusLauncherTrigger : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private VirusProjectile Prefab;
    [Header("Setting")]
    [SerializeField] private float AttackDelay;
    [SerializeField] private LayerMask TargetLayer;
    [SerializeField] private float VirusSpeed = 15f;
    [SerializeField] float LifeTime = 4f;

    private float LastAttackTime = -999f;

    public LayerMask GetLayer => TargetLayer;
    void TryFire(GameObject Target)
    {
        if (Prefab == null)
        {
            return;
        }
        if (Time.time < LastAttackTime + AttackDelay) return;

        LastAttackTime = Time.time;
        Vector2 FireDirection = (Target.transform.position - this.transform.position).normalized;
        Vector3 SpawnPosition = transform.position;
        VirusProjectile SpawnedProjectile = Instantiate(Prefab, SpawnPosition, Quaternion.identity);
        SpawnedProjectile.Initialize(FireDirection, TargetLayer, VirusSpeed, LifeTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & TargetLayer) != 0))
        {
            if (Time.time >= LastAttackTime + AttackDelay) TryFire(collision.gameObject);
        }
    }
}
