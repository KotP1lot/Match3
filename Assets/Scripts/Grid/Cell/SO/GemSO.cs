using UnityEngine;
[CreateAssetMenu()]
public class GemSO : ScriptableObject
{
    public GemType type;
    public Sprite sprite;
    public Sprite doubleSprite;
    public int score;
}
public enum GemType
{
    fish,
    meat,
    salad,
    sweet,
    drink,
}