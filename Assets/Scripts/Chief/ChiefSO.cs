using System;
using UnityEngine;

[CreateAssetMenu()]
public class ChiefSO : ScriptableObject
{
    [SerializeField] ChiefLvlInfo[] lvlInfo;
    public new string name;
    public GemType gemType;
    public BGType bgType;
    public ChiefLvlInfo GetLvlInfo(int lvl)
    {
        foreach (var lvlInfo in lvlInfo)
        {
            if (lvlInfo.lvl == lvl)
            {
                return lvlInfo;
            }
        }
        return new ChiefLvlInfo();
    }
}
[Serializable]
public struct ChiefLvlInfo
{
    public int lvl;
    public Sprite sprite;
    public int yumyBonus;
    public int countToUltimate;
}