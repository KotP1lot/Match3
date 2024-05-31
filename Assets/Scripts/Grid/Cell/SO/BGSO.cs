using System;
using UnityEngine;
[CreateAssetMenu()]
public class BGSO : ScriptableObject
{
    [Serializable]
    public struct BGsprite 
    {
        public Sprite sprite;
        public GemType gemType;
    }
    [SerializeField] BGsprite[] sprites;
    [SerializeField] BGLVLInfo[] lvlInfo;
    public bool isMoveWithLine;
    public BGType type;
    public string bgName;
    public string describe;

    public Sprite GetSprite(GemType type) 
    {
        foreach (BGsprite gsprite in sprites) 
        {
            if (gsprite.gemType == type)
                return gsprite.sprite;
        }
        return null;
    }
    public BGLVLInfo GetLvlInfo(int lvl) 
    {
        foreach (BGLVLInfo info in lvlInfo)
        {
            if (info.lvl == lvl)
                return info;
        }
        return new BGLVLInfo();
    }
}
[Serializable]
public struct BGLVLInfo
{
    public int lvl;
    public int range;
    public int score;
}
public enum BGType
{
    saucepan,
    vertical,
    horizontal,
    box
}

