using System.Collections;
using UnityEngine;

public class VirusAttack : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] AudioClip HitSound;
    [Header("Settings")]
    [SerializeField] private LayerMask TargetLayer;
    [SerializeField] private float AttackPowerX;
    [SerializeField] private float AttackPowerY;
    private float Delay = 0.1f;
    private Collider2D targetCollider;
    //int wallLayer;
    //int PlayerLayer;
    private void Start()
    {
        HitSound = Resources.Load<AudioClip>("Sound/HitSound");
       // wallLayer = LayerMask.NameToLayer("Wall");
       // PlayerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnDisable()
    {
        if(targetCollider != null)
        {
            targetCollider.enabled = true;
        }
        
    }

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
        Collider2D TargetCollider = collision.GetComponent<Collider2D>();
        if (TargetRb != null && TargetHp != null && TargetCollider != null)
        {
            //TargetRigidbody = TargetRb;
            TargetRb.linearVelocityX = (-1) * AttackPowerX;
            TargetRb.linearVelocityY = AttackPowerY;
            if(SoundManager.Instance != null && HitSound != null)
            {
               SoundManager.Instance.PlaySfxOneShot(HitSound);
            }
            if (targetCollider == null) targetCollider = TargetCollider;
            TargetHp.ChangeState(PlayerState.Stun);
            StartCoroutine(ColliderSet(TargetCollider));

        }

    }
    /*    private void OnTriggerStay2D(Collider2D collision)
        {
            if(TargetRigidbody != null && Time.time >= LastAttackTime + Delay)
            {
                TargetRigidbody.position += (Vector2.right * KnockDistance);
                TargetRigidbody.linearVelocityX = (-1) * AttackPowerX;
                TargetRigidbody.linearVelocityY = AttackPowerY;
                if(SoundManager.Instance != null && HitSound != null)
                {
                   SoundManager.Instance.PlaySfxOneShot(HitSound);
                }
                LastAttackTime = Time.time;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            TargetRigidbody = null;
        }*/
    IEnumerator ColliderSet(Collider2D collider)
    {
        collider.enabled = false;
        yield return new WaitForSecondsRealtime(Delay);
        collider.enabled = true;
    }

    //IEnumerator ColliderSet()
    //{
    //    Physics2D.IgnoreLayerCollision(PlayerLayer, wallLayer, true);
    //    yield return new WaitForSecondsRealtime(Delay);
    //    Physics2D.IgnoreLayerCollision(PlayerLayer, wallLayer, false);
    //
    //}
}

