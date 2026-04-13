using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TrackingMove : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Transform TargetObject;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float TrackingRange;
    [SerializeField] private Collider2D MyCollider2D;
    [SerializeField] private SpriteRenderer MySpriteRenderer;

    [Header("Inspector View")]
    [SerializeField] private EnemyState MyState;
    private Vector2 DefaultTransform;

    private void Awake()
    {
        DefaultTransform = this.transform.position;
    }
    private void InIt()
    {
        MyCollider2D.enabled = true;
        MySpriteRenderer.enabled = true;
        this.transform.position = DefaultTransform;
    }

    private void ChangeState(EnemyState State)
    {
        if (MyState == State) return;
        MyState = State;
    }

    private void Update()
    {
        if(TargetObject == null)
        {
            GameObject Target = GameObject.FindGameObjectWithTag("Player");
            if (Target == null) return;
            TargetObject = Target.transform;
        }
        MyState_Action();
        if(MyState == EnemyState.Chase)
        {
            HandleMove();
        }
    }

    private void HandleMove()
    {
        Vector2 NewPosition = (TargetObject.position - this.transform.position).normalized;
        transform.position = (Vector2)transform.position + NewPosition * MoveSpeed * Time.deltaTime;
    }

    private void MyState_Action()
    {
        float Distance = Vector2.Distance(TargetObject.position, this.transform.position);
        switch(MyState)
        {
            case EnemyState.Idle:
                if(Distance <= TrackingRange)
                {
                    ChangeState(EnemyState.Chase);
                }
                break;
            case EnemyState.Chase:
                if(Distance > TrackingRange * 1.4f)
                {
                    ChangeState(EnemyState.Idle);
                }
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, TrackingRange);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, TrackingRange * 1.4f);
    }
}
