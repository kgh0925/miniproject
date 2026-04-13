using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class VaccinePorjetile : MonoBehaviour
{
    
    [Header("Settings")]
    [SerializeField] float Speed;
    [SerializeField] float LifeTime;
    [SerializeField] LayerMask TargetLayer;
    [SerializeField] int Damage = 1;

    private Vector2 MoveDirection = Vector2.right;
    private void OnEnable()
    {
        Destroy(gameObject, LifeTime);
    }

    public void Initialize(Vector2 Direction, LayerMask Layer)
    {
        if (Direction == Vector2.zero)
        {
            Direction = Vector2.right;
        }
        MoveDirection = Direction.normalized;
        TargetLayer = Layer;

        float zRotation = Mathf.Atan2(MoveDirection.y, MoveDirection.x) *
            Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
    }

    private void Update()
    {
        Vector2 NewPosition = (Vector2)transform.position + Speed * MoveDirection * Time.deltaTime;
        transform.position = NewPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (((1 << collision.gameObject.layer) & TargetLayer) == 0) return;

        IDamageable Target = collision.GetComponent<IDamageable>();

        if (Target != null)
        {
            Target.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
