using JetBrains.Annotations;
using System;
using UnityEngine;
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
    Chase
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