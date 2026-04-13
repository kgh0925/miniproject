using UnityEngine;

public class VaccineController : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] CircleCollider2D MyCollider;
    [SerializeField] VaccineAttack MyVaccine;

    private LayerMask TargetLayer;
    private void Awake()
    {
        if(MyCollider == null)
        {
            MyCollider = GetComponent<CircleCollider2D>();
        }
        if(MyVaccine == null)
        {
            MyVaccine = FindFirstObjectByType<VaccineAttack>();
        }
        if(MyVaccine != null)
        {
            TargetLayer = MyVaccine.GetLayer;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(((1 << collision.gameObject.layer) & TargetLayer) == 0)
        {
            return;
        }
        MyVaccine.AttackTarget(collision.gameObject);
    }
}
