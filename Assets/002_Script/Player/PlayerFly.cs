using UnityEngine;
using UnityEngine.UI;

public class PlayerFly : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] Rigidbody2D MyRigidbody2D;
    [SerializeField] PlayerMove2D MyPlayerMove2D;
    [SerializeField] PlayerInputReader MyInputReader;
    [SerializeField] PlayerHp MyPlayerHp;
    [SerializeField] Image FlyTimeUI;

    [Header("Setting")]
    [SerializeField] private float FlyEnergeDelayTime = 2f;
    [SerializeField] private float TotalFlyTime = 3f;
    [SerializeField] private float FlySpeed = 5f;
    [SerializeField] private float GravityScale;
    [SerializeField] private float CurrentFlyTime;
    private bool Flying;
    private float LastFlyTime = -999f;
    private bool MaxEnerge => CurrentFlyTime == TotalFlyTime;
    
    private void Awake()
    {
        if (MyRigidbody2D == null)
        {
            MyRigidbody2D = GetComponent<Rigidbody2D>();
        }
        if (MyInputReader == null)
        {
            MyInputReader = GetComponent<PlayerInputReader>();
        }
        if (MyPlayerHp == null)
        {
            MyPlayerHp = GetComponent<PlayerHp>();
        }
        if (MyPlayerMove2D == null)
        {
            MyPlayerMove2D = GetComponent<PlayerMove2D>();
        }
        CurrentFlyTime = TotalFlyTime;
        if(MyRigidbody2D != null)
        {
            GravityScale = MyRigidbody2D.gravityScale;
        }
        
    }

    private void Update()
    {
        if(CurrentFlyTime <= TotalFlyTime && !MaxEnerge) // TODO : 최대일때도 1번은 갱신해야 UI Value가 1이 될 수 있음
        {
            UpdateFillAmount();
            if (Time.time >= LastFlyTime + FlyEnergeDelayTime)
            {
                FlyTime();
            }
        }
        if(MaxEnerge && FlyTimeUI.enabled)
        {
            FlyTimeUI.enabled = false;
        }


    }
    private void FixedUpdate()
    {
        if (MyInputReader != null && MyRigidbody2D != null && MyPlayerMove2D != null && 
            MyInputReader.FlyIsPressed && CurrentFlyTime > 0 && !MyPlayerHp.NotMove)
        {
             Fly();
             Flying = true;
        }
        if((Flying && !MyInputReader.FlyIsPressed) || CurrentFlyTime <= 0)
        {
            Flying = false;
            if(!MyPlayerHp.NotMove)
            {
                MyRigidbody2D.gravityScale = GravityScale;
            }
            
        }
    }

    private void Fly()
    {
        MyRigidbody2D.gravityScale = 0;
        MyRigidbody2D.linearVelocityY = 0;
        MyRigidbody2D.linearVelocity = new Vector2(Direction8.ToVector2(MyPlayerMove2D.DIRECTION).x * FlySpeed, MyRigidbody2D.linearVelocityY);
        LastFlyTime = Time.time;
        CurrentFlyTime = Mathf.Clamp(CurrentFlyTime - Time.deltaTime, 0, TotalFlyTime);
    }
    private void UpdateFillAmount()
    {
        if(!FlyTimeUI.enabled)
        {
            FlyTimeUI.enabled = true;
        }
        FlyTimeUI.fillAmount = CurrentFlyTime / TotalFlyTime;
    }
    private void FlyTime()
    {
        CurrentFlyTime = Mathf.Clamp(CurrentFlyTime+Time.deltaTime,0, TotalFlyTime);
    }

    public void TimeAdd()
    {
        CurrentFlyTime = TotalFlyTime;
    }
}
