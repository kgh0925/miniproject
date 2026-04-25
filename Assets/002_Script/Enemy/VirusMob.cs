using System.Collections;
using UnityEngine;

public class VirusMob : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] EnemyHp VirusHp;
    [SerializeField] SpriteRenderer MySpriteRenderer;
    [SerializeField] Collider2D MyCollider2D;
    [SerializeField] float RespawnTime= 10f;
    [SerializeField] RoundMove2D Round;
    [SerializeField] TrackingMove Tracking;

    Vector2 DefaultPosition;
    private void Awake()
    {
        if(VirusHp == null)
        {
            VirusHp = GetComponent<EnemyHp>();
        }
        if(MySpriteRenderer == null)
        {
            MySpriteRenderer = GetComponent<SpriteRenderer>();
        }
        if(MyCollider2D == null)
        {
            MyCollider2D = GetComponent<Collider2D>();
        }
        if(Round == null)
        {
            Round = GetComponent<RoundMove2D>();
        }
        if(Tracking == null)
        {
            Tracking = GetComponent<TrackingMove>();
        }
        DefaultPosition = gameObject.transform.position;
    }
    private void OnEnable()
    {
        VirusHp.Dead += Init;
    }
    private void OnDisable()
    {
        VirusHp.Dead -= Init;
    }
    private void Init()
    {
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        if (VirusHp != null) VirusHp.Init();
        this.gameObject.transform.position = DefaultPosition;
        if (MySpriteRenderer != null) MySpriteRenderer.enabled = false;
        if(MyCollider2D != null) MyCollider2D.enabled = false;
        if (Round != null) Round.enabled = false;
        if(Tracking != null) Tracking.enabled = false;
        yield return new WaitForSeconds(RespawnTime);
        if (Round != null) Round.enabled = true;
        if (Tracking != null) Tracking.enabled = true;
        if (MySpriteRenderer != null) MySpriteRenderer.enabled = true;
        if (MyCollider2D != null) MyCollider2D.enabled = true;

    }
}
