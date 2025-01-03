using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "JY/Stat/New Stat", fileName = "STAT_")]
public class JYStat : ScriptableObject, ICloneable
{
    public event Action<float> onStatValueChanged;

    [SerializeField] private int statId;
    [SerializeField] private string codeName;
    [SerializeField] private string statName;
    [SerializeField] private string statDesc;

    [SerializeField] private float maxValue;
    [SerializeField] private float minValue;
    [SerializeField] private float defaultValue;

    public int StatId => statId;
    public string CodeName => codeName;
    public string StatName => statName;
    public string StatDesc => statDesc;

    public float MaxValue => maxValue;
    public float MinValue => minValue;
    public float DefaultValue => defaultValue;

    public float BonusValue { get; private set; }
    public float Value => Mathf.Clamp(defaultValue + BonusValue, MinValue, MaxValue);

    private Dictionary<string, Dictionary<int, float>> bonusStatDict = new();
    
    public void SetBonusValue(string key, int stack, float value)
    {
        if (!bonusStatDict.ContainsKey(key))
            bonusStatDict[key] = new Dictionary<int, float>();
        else
            BonusValue -= bonusStatDict[key][stack];

        bonusStatDict[key][stack] = value;
        BonusValue += bonusStatDict[key][stack];

        onStatValueChanged?.Invoke(Value);
    }

    public void SetBonusValue(string key, float value)
        => SetBonusValue(key, 0, value);
    
    public void RemoveBonusValue(string key)
    {
        
    }

    public object Clone() => Instantiate(this);    
}
