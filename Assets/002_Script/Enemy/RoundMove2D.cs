
using UnityEngine;

public class RoundMove2D : MonoBehaviour
{
    [Header("Ref")]
    [Tooltip("Rount Point [ Left ]")][SerializeField] Transform LeftPoint;
    [Tooltip("Rount Point [ Right ]")][SerializeField] Transform RightPoint;
    [Tooltip("Script Default : 10.0f")][SerializeField] float MoveSpeed = 10.0f;
    [SerializeField] SpriteRenderer MyImage;

    private float LeftPoint_X;
    private float RightPoint_X;
    private int Direction;
    #region Gizmo
    private Vector2 Gizmos_LeftPoint;
    private Vector2 Gizmos_RightPoint;
    #endregion

    private void Awake()
    {
        LeftPoint_X = LeftPoint.position.x;
        RightPoint_X = RightPoint.position.x;
        Gizmos_LeftPoint = new Vector2(LeftPoint.position.x, LeftPoint.position.y);
        Gizmos_RightPoint = new Vector2(RightPoint.position.x, RightPoint.position.y);
        Direction = 1;
    }
    private void Update()
    {
        transform.position += Vector3.right * MoveSpeed * Direction * Time.deltaTime;

        if(transform.position.x  >= RightPoint_X)
        {
            Direction = -1;
            MyImage.flipX = true;
        }
        else if(transform.position.x <= LeftPoint_X)
        {
            Direction = 1;
            MyImage.flipX = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position,Gizmos_LeftPoint);

        Gizmos.DrawLine(transform.position, Gizmos_RightPoint);
    }
}
