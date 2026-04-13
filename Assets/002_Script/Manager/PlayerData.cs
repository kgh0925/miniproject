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

    public IReadOnlyList<string> Inventory => MyItems;
    public event Action Code;
    public event Action Shopping;
    public event Action<string> TypeEvent; 
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
        return true;
    }

}
