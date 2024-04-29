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
    public int score;
    public bool isMoveWithLine;
    public BGType type;
    public Sprite GetSpriteByType(GemType type) 
    {
        foreach (BGsprite gsprite in sprites) 
        {
            if (gsprite.gemType == type)
                return gsprite.sprite;
        }
        return null;
    }
}
public enum BGType
{
    bomb,
    lineDestroyer,
    box
}

