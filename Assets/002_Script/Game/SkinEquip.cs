using System.Collections.Generic;
using UnityEngine;

public class SkinEquip : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] Animator MyAnimator;
    [SerializeField] List<SkinData> MyData = new List<SkinData>();
    private Dictionary<string, SkinData> MyDict = new Dictionary<string, SkinData>();
    //public IReadOnlyDictionary<string, MapData> Data { get; private set; }

    private void Awake()
    {
        MyDict.Clear();
        if (MyData != null)
        {
            foreach (SkinData data in MyData)
            {
                if (!MyDict.TryGetValue(data.ItemId, out SkinData skindata))
                {
                    MyDict.Add(data.ItemId, data);
                }
            }
        }
    }
    private void Start()
    {
        if (PlayerData.Instance == null) return;
        SkinSettings(PlayerData.Instance.EnquipSkin);

    }
    private void SkinSettings(string ItemId)
    {
        if (string.IsNullOrWhiteSpace(ItemId.Trim()))
        {
            return;
        }
        if (!MyDict.TryGetValue(ItemId, out SkinData skin))
        {
            return;
        }

        MyAnimator.runtimeAnimatorController = skin.Animation;
        //Debug.Log(ItemId);

        
    }
}
