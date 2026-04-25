using System;
using UnityEngine;
[Serializable]
public enum Direction
{
    Left,
    LeftUp,
    LeftDown,
    Right,
    RightUp,
    RightDown,
    Up,
    Down,
    None
}


[Serializable]
public enum EnemyState
{
    Idle,
    Chase,
    Death
}
[Serializable]
public enum PlayerState
{
    Idle,
    Stun,
    Climbing,
    Dead
}
[Serializable] 
public enum ItemType
{
    Skin,
    Map
}
[Serializable]
public struct ShopCatalogEntry
{
    public string Id;
    public string DisplayName;
    public ItemType Category;
    public Sprite Icon;
    public int BuyCode;
}
[Serializable]
public struct MapData
{
    public string ItemId;
    public Sprite[] Images;
}
[Serializable]
public struct SkinData
{
    public string ItemId;
    public RuntimeAnimatorController Animation;
}
[Serializable]
public struct PurchaseRequest   
{
    public string Id;
    public int BuyCode;
    public PurchaseRequest(string id, int buyCode)
    {
        Id = id;
        BuyCode = buyCode;
    }
}
[Serializable]
public struct StageClearData
{
    public int CodePercent;
    public int MissCodeCount;
    public int MinTime;
    public int SecTime;
    public int CollisionCount;
    public int AppendCodeCount;
    public int Rating;

    public StageClearData(int CodePercent, int MissCodeCount, int MinTime,
        int SecTime, int CollisionCount, int PickCodeCount, float Rating)
    {
        this.CodePercent = CodePercent;
        this.MissCodeCount = MissCodeCount;
        this.MinTime = MinTime;
        this.SecTime = SecTime;
        this.CollisionCount = CollisionCount;
        this.AppendCodeCount = PickCodeCount;
        this.Rating = (int)Rating;
    }
}
[Serializable]
public struct UndoData
{
    public string ItemId;
    public int ItemValue;
    public string WearSkin;
    public string WearMap;

    public UndoData(string id, int code, string skin, string map)
    {
        ItemId = id;
        ItemValue = code;
        WearSkin = skin;
        WearMap = map;
    }
}
public static class Direction8
{
    public static Vector2 ToVector2(Direction direction)
    {
        return direction switch
        {
            Direction.Left => Vector2.left,
            Direction.Right => Vector2.right,
            Direction.Up => Vector2.up,
            Direction.Down => Vector2.down,
            Direction.LeftUp => new Vector2(-1f, 1f).normalized,
            Direction.LeftDown => new Vector2(-1f, -1f).normalized,
            Direction.RightUp => new Vector2(1f, 1f).normalized,
            Direction.RightDown => new Vector2(1f, -1f).normalized,
            _ => Vector2.zero
        };
    }
}