using System.Collections.Generic;
using UnityEngine;

public class MapEquip : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] SpriteRenderer[] MySpriteRenderer;
    [SerializeField] List<MapData> MyData = new List<MapData>();
    private Dictionary<string, MapData> MyDict = new Dictionary<string, MapData>();
    //public IReadOnlyDictionary<string, MapData> Data { get; private set; }

    private void Awake()
    {
        MyDict.Clear();
        if(MyData != null)
        {
            foreach(MapData data in MyData)
            {
                if(!MyDict.TryGetValue(data.ItemId, out MapData map))
                {
                    MyDict.Add(data.ItemId, data);
                }
            }
        }
    }
    private void Start()
    {
        if (PlayerData.Instance == null) return;
        MapSettings(PlayerData.Instance.EnquipMap);
        
    }
    private void MapSettings(string ItemId)
    {
        if(string.IsNullOrWhiteSpace(ItemId.Trim()))
        {
            return;
        }
        if(!MyDict.TryGetValue(ItemId, out MapData map))
        {
            return;
        }
        
        int count = Mathf.Min(map.Images.Length, MySpriteRenderer.Length);
        
        for(int i = 0; i < count; i++)
        {
            MySpriteRenderer[i].sprite = map.Images[i];
        }
    }
}
