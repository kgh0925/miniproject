using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    public static SavePointManager Instance { get; private set; }

    private List<IResettable> _resettables = new List<IResettable>();
    public void Register(IResettable resettable) => _resettables.Add(resettable);
    public void Unregister(IResettable resettable) => _resettables.Remove(resettable);

    private Vector2 LastSavedPosition;
    public void ReturnToSavePoint(GameObject player)
    {
        Rigidbody2D playerRb2d = player.GetComponent<Rigidbody2D>();
        if (playerRb2d == null )
        {
            return;
        }
        playerRb2d.position = LastSavedPosition;
        playerRb2d.linearVelocity = Vector2.zero;
        playerRb2d.angularVelocity = 0;
        List<IResettable> TempTable = new List<IResettable>(_resettables);
        // 등록된 모든 리셋 대상을 초기화 
        foreach (var resettable in TempTable)
        {
            resettable.ResetToSavePoint();
        } 
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    public void SavePointTrigger(Vector2 position)
    {
        LastSavedPosition = position;
    }
}
