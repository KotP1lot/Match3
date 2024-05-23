using System;
using UnityEngine;

[CreateAssetMenu()]
public class ChiefSO : ScriptableObject
{
    [SerializeField] ChiefLvlInfo[] lvlInfo;
    public new string name;
    public Sprite sprite;
    public GemType gemType;
    public BGType bgType;
    public ChiefLvlInfo GetLvlInfo(int lvl)
    {
        foreach (var lvlInfo in lvlInfo)
        {
            if (lvlInfo.lvl == lvl)
                return lvlInfo;
        }
        return null;
    }
}
[Serializable]
public class ChiefLvlInfo
{
    public int lvl;
    public int yumyBonus;
    public int countToUltimate;
    public int lvlCost;
}