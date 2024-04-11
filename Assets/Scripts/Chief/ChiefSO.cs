using UnityEngine;

[CreateAssetMenu()]
public class ChiefSO : ScriptableObject
{
    public Sprite sprite;
    public new string name;
    public GemType gemType;
    public BGType bgType;
    public int yumyBonus;
    public int countToUltimate;
}
