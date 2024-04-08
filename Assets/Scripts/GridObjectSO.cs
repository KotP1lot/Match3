using UnityEngine;
[CreateAssetMenu()]
public class GridObjectSO : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public int Score;
    public GemType Type;
    public bool IsAffectedByGravity;
}

public enum GemType 
{
    fish,
    meat,
    salat,
    sweet,
    drink,
}