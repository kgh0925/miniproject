using System;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : MonoBehaviour , IResettable, IDamageable
{
    [Header("Ref")]
    [SerializeField] Slider MyHpUI;

    [SerializeField] private float MaxHp = 100;
    [SerializeField] private float CurrentHp;
    
    private AudioClip MyAudioClip;
    private Vector3 MyPosition;
    public bool IsDead => CurrentHp <= 0;
    public bool IsStarted { get; private set; }
    public event Action BossDead;
    private void Awake()
    {
        CurrentHp = MaxHp;
        MyAudioClip = Resources.Load<AudioClip>("Sound/BossSound");
        //this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (SavePointManager.Instance == null) return;
        SavePointManager.Instance.Register(this);
        IsStarted = true;
        if(MyAudioClip != null && SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySfxOneShot(MyAudioClip);
        }
        if(MyHpUI != null)
        {
            MyHpUI.value = CurrentHp / MaxHp;
        }
    }
    private void OnDisable()
    {
        if (SavePointManager.Instance == null) return;
        SavePointManager.Instance.Unregister(this);
    }
    
    private void Start()
    {
        MyPosition = transform.position;
    }
    public void ResetToSavePoint()
    {
        if (!IsStarted) return;
        CurrentHp = MaxHp;
        transform.position = MyPosition;
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(int Damage)
    {
        CurrentHp = Mathf.Clamp(CurrentHp - Damage, 0, MaxHp);
        if (MyHpUI != null)
        {
            MyHpUI.value = CurrentHp / MaxHp;
        }
        if(IsDead)
        {
            BossDead?.Invoke();
            ResetToSavePoint();
        }
    }
}
