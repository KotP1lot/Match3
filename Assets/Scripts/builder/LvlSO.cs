using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LvlSO : ScriptableObject
{
    public CelLLvlInfo[] cells;
    public TurnInfo turns;
    public GoalInfo[] goals;
    public CustomerInfo[] customers;
    public int moneyFromCustomer;
    //Розблокуємий повар
    //Рекомендуємі повари
    //Тутор
    //Діалог
}
[Serializable]
public struct CustomerInfo 
{
    public CustomerType type;
    public int chancePercent;
    public int bonusPercent;
}
[Serializable]
public struct CustomerType
{
    public bool isFine;
    public bool isMeh;
    public bool isBad;
}

[Serializable]
public struct GoalInfo 
{
    public GoalType type;
    public int count;
}
[Serializable]
public struct CelLLvlInfo 
{
    public BorderNDirection[] borders;
    public FloorType floorType;
    public GridObject prefab;
}
[Serializable]
public struct TurnInfo 
{
    public int max;
    public int minFor1Star;
    public int minFor2Star;
    public int minFor3Star;
}
[Serializable]
public struct BorderNDirection 
{
    public BorderType type;
    public Direction direction;
}
