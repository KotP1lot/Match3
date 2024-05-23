using UnityEngine;
[CreateAssetMenu()]
public class BonusSO : ScriptableObject
{
    public int energyBonus;
    public int moneyBonus;
    public int yummyBonus;

    public void Clear() 
    {
        energyBonus = 0;
        moneyBonus = 0;
        yummyBonus = 0;
    }
}
