using UnityEngine;

public class FloorSO : ScriptableObject
{
    public FloorType type;
    public Sprite[] hp_sprites;
}
public enum FloorType 
{
    simpty,
}
