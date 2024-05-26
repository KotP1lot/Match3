using UnityEngine;
[CreateAssetMenu()]
public class FloorSO : ScriptableObject
{
    public FloorType type;
    public Sprite[] hp_sprites;
}
public enum FloorType
{
    none,
    simpty,
}
