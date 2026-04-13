using UnityEngine;

public class VirusAttack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask TargetLayer;
    [SerializeField] private float AttackPowerX;
    [SerializeField] private float AttackPowerY;
    //[SerializeField] private float AttackDelay = 0.1f;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & TargetLayer) == 0)
        {
            return;
        }
        //PlayerInputReader playerInput = collision.GetComponent<PlayerInputReader>();
        //if(playerInput != null )
        //{
        //
        //}
        PlayerHp TargetHp = collision.GetComponent<PlayerHp>();
        Rigidbody2D TargetRb = collision.GetComponent<Rigidbody2D>();
        if (TargetRb != null && TargetHp != null)
        {
            TargetRb.linearVelocityX = (-1) * AttackPowerX;
            TargetRb.linearVelocityY = AttackPowerY;
            TargetHp.ChangeState(PlayerState.Stun);

        }

    }
}
