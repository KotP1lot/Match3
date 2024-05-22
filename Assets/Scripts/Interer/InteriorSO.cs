using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class InteriorSO : ScriptableObject
{
    public InteriorType type;
    public List<InteriorLvlInfo> lvlInfo;
    public InteriorLvlInfo GetLvlInfo(int lvl) 
    {
        return lvlInfo[lvl];
    }
}
[Serializable]
public struct InteriorLvlInfo
{
    public int cost;
    public int bonus;
    public Sprite[] sprites;
}
public enum InteriorType 
{
    stul,
    stil,
    light
}