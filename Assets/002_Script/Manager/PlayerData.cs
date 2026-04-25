using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    [Header("Data")]
    [SerializeField] private int Vaccine_Code;
    [SerializeField] private int Code_Amount; // Good value stored for each VaccineCode
    [SerializeField] List<string> MyItems = new List<string>();
    [SerializeField] private string WearSkin;
    [SerializeField] private string WearMap;
    [SerializeField] private bool IntroView = false;
    [SerializeField] private string CurrentSceneName;
    private StageClearData StageResultData = new StageClearData();
    private Stack<UndoData> UndoStack = new Stack<UndoData>();
    public bool Intro => IntroView;
    public string SceneName => CurrentSceneName;

    public StageClearData StageResult => StageResultData;
    public int Code_Value => Code_Amount;

    public string EnquipSkin => WearSkin;
    public string EnquipMap => WearMap;
    public IReadOnlyList<string> Inventory => MyItems;
    public event Action Code;
    public event Action Shopping;
    public event Action<string> TypeEvent;
    public event Action<string> EquipEvent;
    public int GetCode => Vaccine_Code;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        UndoStack.Clear();
    }

    public void CodePickUp()
    {
        Vaccine_Code += Code_Amount;
        Code?.Invoke();
    }

    public bool PurchaseItem(string Id, int Amount)
    {
        if (Vaccine_Code < Amount) return false;
        Vaccine_Code = Mathf.Clamp(Vaccine_Code - Amount, 0, Vaccine_Code);
        MyItems.Add(Id);
        Shopping?.Invoke();
        TypeEvent?.Invoke(Id);
        EquipEvent?.Invoke(Id);
        return true;
    }

    public void MapEquip(string Id)
    {
        WearMap = Id;
        EquipEvent?.Invoke(Id);
    }
    public void SkinEquip(string Id)
    {
        WearSkin = Id;
        EquipEvent?.Invoke(Id);
    }

    public void StageClear(StageClearData InputData)
    {
        StageResultData = InputData;
    }
    public void IntroTrue()
    {
        IntroView = true;
    }
    public void PlayerCurrentStageScene(string Name)
    {
        CurrentSceneName = Name;
    }
    public void StackPush(UndoData data)
    {
        UndoStack.Push(data);
    }
    public bool StackPop(out UndoData data)
    {
        if(UndoStack.Count > 0)
        {
            data = UndoStack.Pop();
            return true;
        }
        data = new UndoData();
        return false;

    }
    public bool StackPeek(out UndoData data)
    {
        if (UndoStack.Count > 0)
        {
            data = UndoStack.Peek();
            return true;
        }
        data = new UndoData();
        return false;
    }
    public void RemoveItem(string Id)
    {
        MyItems.Remove(Id);
    }
    public void UndoCode(int Amount)
    {
        Vaccine_Code = Amount;
        Shopping?.Invoke();
    }
}
