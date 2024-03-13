using UnityEngine;
[CreateAssetMenu()]
public class GridObjectSO : ScriptableObject
{
    public string Name;
    public Color Sprite;
   // public Sprite Sprite;
    public int Score;
    public GridObjectType Type;
    public bool IsAffectedByGravity;
}

public enum GridObjectType 
{
    fish,
    meat,
    salat,
    
    
    
    wall
}