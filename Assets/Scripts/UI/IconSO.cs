using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class IconSO : ScriptableObject
{
    public List<IconInfo> iconInfos;
    public Sprite GetSpriteByType(GoalType type) 
    {
        return iconInfos.Find(x => x.type == type).sprite;
    }
}
[Serializable]
public struct IconInfo 
{
    public GoalType type;
    public Sprite sprite;
}
