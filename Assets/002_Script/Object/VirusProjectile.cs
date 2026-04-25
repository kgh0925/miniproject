using UnityEngine;

public class VirusProjectile : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private VirusAttack MyVirusAttack;
    [Header("Settings")]
    [SerializeField] float Speed;
    [SerializeField] float LifeTime;
    [SerializeField] LayerMask TargetLayer;

    private Vector2 MoveDirection = Vector2.right;
    private void OnEnable()
    {
        Destroy(gameObject, LifeTime);
    }

    public void Initialize(Vector2 Direction, LayerMask Layer, float InputSpeed, float lifetime)
    {
        if (Direction == Vector2.zero)
        {
            Direction = Vector2.right;
        }
        MoveDirection = Direction.normalized;
        TargetLayer = Layer;
        Speed = InputSpeed;
        float zRotation = Mathf.Atan2(MoveDirection.y, MoveDirection.x) *
            Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);

    }

    private void Update()
    {
        Vector2 NewPosition = (Vector2)transform.position + Speed * MoveDirection * Time.deltaTime;
        transform.position = NewPosition;
    }
}
