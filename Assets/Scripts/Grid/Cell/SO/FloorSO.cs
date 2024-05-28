using UnityEngine;
[CreateAssetMenu()]
public class FloorSO : ScriptableObject
{
    public FloorType type;
    public Color[] hp_sprites;
}
public enum FloorType
{
    none,
    simpty,
    mur
}
