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
    public int GetLvlBonus(int lvl) 
    {
        if (lvl >=lvlInfo.Count)return 0;
        int i = 0;
        for (int j = 0; j <= lvl; j++) 
        {
            i+=lvlInfo[j].bonus;
        }
        return i;
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