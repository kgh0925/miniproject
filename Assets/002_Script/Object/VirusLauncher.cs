using UnityEngine;

public class VirusLauncher : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private VirusProjectile Prefab;
    [Header("Setting")]
    [SerializeField] private float AttackDelay;
    [SerializeField] private LayerMask TargetLayer;
    [SerializeField] private Direction LauncherDirection;
    [SerializeField] private float VirusSpeed = 15f;
    [SerializeField] float LifeTime = 4f;

    private float LastAttackTime = -999f;

    public LayerMask GetLayer => TargetLayer;
    void TryFire(Direction direction)
    {
        if (Prefab == null)
        {
            return;
        }
        if (Time.time < LastAttackTime + AttackDelay) return;

        LastAttackTime = Time.time;
        Vector2 FireDirection = Direction8.ToVector2(direction).normalized;
        Vector3 SpawnPosition = transform.position + (Vector3)FireDirection * 5;
        VirusProjectile SpawnedProjectile = Instantiate(Prefab, SpawnPosition, Quaternion.identity);
        SpawnedProjectile.Initialize(FireDirection, TargetLayer, VirusSpeed, LifeTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & TargetLayer) != 0))
        {
            if (Time.time >= LastAttackTime + AttackDelay) TryFire(LauncherDirection);
        }
    }
}
