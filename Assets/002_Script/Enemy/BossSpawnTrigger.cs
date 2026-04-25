using Unity.VisualScripting;
using UnityEngine;

public class BossSpawnTrigger : MonoBehaviour, IResettable
{
    [Header("Ref")]
    [SerializeField] GameObject BossObject;
    [SerializeField] LayerMask TargetLayer;
    [SerializeField] private bool BossSpawned;
    [SerializeField] GameObject HpUI;
    [SerializeField] private BossMonster Boss;
    public void ResetToSavePoint()
    {
        BossSpawned = false;
        Debug.Log("외부 동작");
        if (HpUI != null)
        {
            HpUI.SetActive(false);
            Debug.Log("내부 동작");
        }
    }
    private void Awake()
    {
        if(Boss == null) Boss = BossObject.GetComponent<BossMonster>();
    }
    
    private void OnEnable()
    {
        if (SavePointManager.Instance == null) return;
        SavePointManager.Instance.Register(this);
        if(Boss != null)
        {
            Boss.BossDead += ResetToSavePoint;
        }
    }
    private void OnDisable()
    {
        if (SavePointManager.Instance == null) return;
        SavePointManager.Instance.Unregister(this);
        if (Boss != null)
        {
            Boss.BossDead -= ResetToSavePoint;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & TargetLayer) == 0) return;
        Debug.Log("레이어통과");
        if(!BossSpawned && BossObject != null)
        {
            Debug.Log("소환동작");
            BossObject.SetActive(true);
            BossSpawned = true;
            if(HpUI != null)
            {
                HpUI.SetActive(true);
            }
         }
    }
}
