using System;
using UnityEngine;

public class UndoSystem : MonoBehaviour
{
    public event Action<string> OnUndo;
    public void Undo()
    {
        if (PlayerData.Instance == null) return;

        if(PlayerData.Instance.StackPop(out UndoData data))
        {
            PlayerData.Instance.RemoveItem(data.ItemId);
            PlayerData.Instance.UndoCode(data.ItemValue + PlayerData.Instance.GetCode);
            PlayerData.Instance.SkinEquip(data.WearSkin);
            PlayerData.Instance.MapEquip(data.WearMap);
            OnUndo?.Invoke(data.ItemId);
        }
    }
}
