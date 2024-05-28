using System;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu()]
public class LvlSO : ScriptableObject
{
    public MonthType Month;
    public int Day;
    public CelLLvlInfo[] cells;
    public TurnInfo turns;
    public GoalInfo[] goals;
    public CustomerInfo[] customers;
    public int moneyFromCustomer;
    public int moneyFromLvl;
    public RecomededChief[] recChiefs;
    public ChiefSO unlockChief;
    public Tips tips;
    //Діалог
}
public enum MonthType 
{
    cherven,
    lupen,
}
[Serializable]
public class Tips 
{
    public VideoClip video;
    public string text;
}
[Serializable]
public struct CustomerInfo 
{
    public CustomerType type;
    public int chancePercent;
    public int bonusPercent;
    public int minSat;
    public int maxSat;
}
[Serializable]
public struct CustomerType
{
    public bool isFine;
    public bool isMeh;
    public bool isBad;
}
[Serializable]
public struct RecomededChief 
{
    public GemType gemType;
    public BGType bgType;
}
[Serializable]
public struct GoalInfo 
{
    public GoalType type;
    public GemType gemType;
    public int count;
}
[Serializable]
public struct CelLLvlInfo 
{
    public BorderNDirection[] borders;
    public FloorType floorType;
    public GridObject prefab;
    public GemType gemType;
}
[Serializable]
public struct TurnInfo 
{
    public int max;
    public int[] turnForStar;
}
[Serializable]
public struct BorderNDirection 
{
    public BorderType type;
    public Direction direction;
}
