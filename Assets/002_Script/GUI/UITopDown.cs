using System.Collections;
using UnityEngine;

public class UITopDown : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private ScreenTopDown screenTransition;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerInputReader MyPlayerInputReader; 
    
    private bool IsRespawning;

    public void SavePointEnable()
    {
        if (IsRespawning || SavePointManager.Instance == null) return;
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        IsRespawning = true;


        PlayerInputBlock(false);
        yield return screenTransition.Close();
        PlayerInputBlock(true);
        // 실제 처리
        SavePointManager.Instance.ReturnToSavePoint(player);
        // 또는 SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        yield return new WaitForSeconds(0.5f);

        // 열기
        yield return screenTransition.Open();
       

        IsRespawning = false;
    }

    private void PlayerInputBlock(bool value)
    {
        if (MyPlayerInputReader != null) MyPlayerInputReader.enabled = value;
    }
}
