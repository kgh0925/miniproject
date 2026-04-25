using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearData : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private VaccineCodeCounter VaccineCodes; // 수집률, 개수,
    [SerializeField] private Timer TimeData;
    [SerializeField] private PlayerHp CollisionData;
    [SerializeField] private LayerMask TargetLayer;
    [SerializeField] private ButtonSceneLoad SceneLoad;
    [SerializeField] private BossMonster Boss;

    [Header("Settings")]
    [SerializeField] private string SceneName;
    

    private StageClearData StageResultData;
    private void Awake()
    {
        StageResultData = new StageClearData();
        if(PlayerData.Instance != null)
        {
            PlayerData.Instance.StageClear(StageResultData);
        }
    }
    private void OnEnable()
    {
        if (Boss != null) Boss.BossDead += ClearStage;
    }
    private void OnDisable()
    {
        if (Boss != null) Boss.BossDead -= ClearStage;
    }
    public void ClearStage()
    {
        if (VaccineCodes == null || TimeData == null || CollisionData == null ||
            GameSceneManager.Instance == null || PlayerData.Instance == null) return;
        int CodePercent = ((int)((1 - ((float)VaccineCodes.CurrentCount / VaccineCodes.TotalCount)) * 100));
        StageResultData = new StageClearData(CodePercent,
            VaccineCodes.CurrentCount,
            (int)TimeData.GetMin,
            (int)TimeData.GetSec,
            CollisionData.CollsionCount,
            (int)(PlayerData.Instance.Code_Value * (VaccineCodes.TotalCount - VaccineCodes.CurrentCount)),
            CodePercent - (CollisionData.CollsionCount / 10)
            );

        // 최고기록 검사
        if (PlayerPrefs.GetInt(PlayerData.Instance.SceneName + "M", 99) > StageResultData.MinTime)
        {
            PlayerPrefs.SetInt(PlayerData.Instance.SceneName + "M", StageResultData.MinTime);
            PlayerPrefs.SetInt(PlayerData.Instance.SceneName + "S", StageResultData.SecTime);

        }
        else if(PlayerPrefs.GetInt(PlayerData.Instance.SceneName + "M", 99) == StageResultData.MinTime)
        {
            if (PlayerPrefs.GetInt(PlayerData.Instance.SceneName + "S", 59) >= StageResultData.SecTime)
            {
                PlayerPrefs.SetInt(PlayerData.Instance.SceneName + "M", StageResultData.MinTime);
                PlayerPrefs.SetInt(PlayerData.Instance.SceneName + "S", StageResultData.SecTime);
            }
        }
        // 업적 검사
        AchiveCheck.Achive(PlayerData.Instance.SceneName, StageResultData, out int[] isClear);
        for(int i = 0; i < isClear.Length; i++)
        {
            if (isClear[i] == 1)
            {
                switch(PlayerData.Instance.SceneName)
                {
                    case "Stage_One":
                        PlayerPrefs.SetInt("Achive" + i, 1); // 0 1
                        break;
                    case "Stage_Two":
                        PlayerPrefs.SetInt("Achive" + (i+2), 1); // 2 3
                        break;
                    case "Stage_Three":
                        PlayerPrefs.SetInt("Achive" + (i + 4), 1); // 4 5
                        break;
                    default: 
                        break;
                }
            }
        }
            PlayerPrefs.Save();
        PlayerData.Instance.StageClear(StageResultData);
        //GameSceneManager.Instance.LoadSceneByName(SceneName);
        if (SceneLoad != null) SceneLoad.Screen(SceneName);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & TargetLayer) == 0) return;
        if (Boss != null) Boss.ResetToSavePoint(); 
        ClearStage();
    }
}
