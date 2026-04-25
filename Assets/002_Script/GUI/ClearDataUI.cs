using System.Collections;
using TMPro;
using UnityEngine;

public class ClearDataUI : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] TMP_Text Title;
    [SerializeField] TMP_Text Percent;
    [SerializeField] TMP_Text MissCode;
    [SerializeField] TMP_Text PlayTime;
    [SerializeField] TMP_Text CollisionCount;
    [SerializeField] TMP_Text VaccineCodeGet;
    [SerializeField] TMP_Text Rating;

    [Header("Setting")]
    [Tooltip("Last Index : TitleMenu")][SerializeField] string[] StageName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Percent.gameObject.SetActive(false);
        MissCode.gameObject.SetActive(false);
        PlayTime.gameObject.SetActive(false);
        CollisionCount.gameObject.SetActive(false);
        VaccineCodeGet.gameObject.SetActive(false);
        Rating.gameObject.SetActive(false);
    }
    private void Start()
    {
        if (Percent == null || MissCode == null || PlayTime == null || CollisionCount == null ||
            VaccineCodeGet == null || Rating == null) return;
        if (PlayerData.Instance == null) return;
        StageClearData temp = PlayerData.Instance.StageResult;
        Percent.text += " " + temp.CodePercent.ToString();
        MissCode.text += " " + temp.MissCodeCount.ToString();
        PlayTime.text += " " + temp.MinTime.ToString() + "Ка" + " " + temp.SecTime.ToString() + "УЪ";
        CollisionCount.text += " " + temp.CollisionCount.ToString();
        VaccineCodeGet.text += " " + temp.AppendCodeCount.ToString();
        Rating.text += " " + temp.Rating.ToString();
        StartCoroutine(TextOrder());
    }

    IEnumerator TextOrder()
    {
        Percent.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        MissCode.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        PlayTime.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        CollisionCount.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        VaccineCodeGet.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Rating.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        Title.gameObject.SetActive(false);
        Percent.gameObject.SetActive(false);
        MissCode.gameObject.SetActive(false);
        PlayTime.gameObject.SetActive(false);
        CollisionCount.gameObject.SetActive(false);
        VaccineCodeGet.gameObject.SetActive(false);
        Rating.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        if (PlayerData.Instance != null && GameSceneManager.Instance != null)
        {
            for (int i = 0; i < StageName.Length-1; i++) // 0 1 2 3
            {
                if (StageName[i] != PlayerData.Instance.SceneName) continue;
                if(i + 1 < StageName.Length) 
                {
                    PlayerData.Instance.PlayerCurrentStageScene(StageName[i + 1]);
                    GameSceneManager.Instance.LoadSceneByName(StageName[i + 1]);
                    break;
                }
                else
                {
                    PlayerData.Instance.PlayerCurrentStageScene(StageName[i]);
                    GameSceneManager.Instance.LoadSceneByName(StageName[i]);
                    break;
                }
            }
        }
        
    }
}
