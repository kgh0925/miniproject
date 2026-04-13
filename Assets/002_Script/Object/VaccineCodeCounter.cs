using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VaccineCodeCounter : MonoBehaviour
{
    [Header("Vaccine Codes")]
    [SerializeField] List<VaccineCode> VaccineCodes;

    [Header("Debug")]
    [SerializeField] private int Count;


    public int TotalCount => Count;
    public int CurrentCount => VaccineCodes.Count;

    private void Awake()
    {
        VaccineCodes = GetComponentsInChildren<VaccineCode>().ToList();
        Count = VaccineCodes.Count;
    }
    private void OnEnable()
    {
        foreach(VaccineCode v in VaccineCodes)
        {
            v.DestroyVCode += DestroyVaccineCode;
        }
    }

    private void DestroyVaccineCode(VaccineCode VCode)
    {
        if(VaccineCodes.Contains(VCode))
        {
            VaccineCodes.Remove(VCode);
        }
    }


}
