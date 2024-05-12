using UnityEngine;

public class Fastidiousness
{
    public int bonusPercent;
    public FastidiousnessType type;
    public int Bonus(int sat)
    {
        int value = type switch
        {
            FastidiousnessType.fine =>  sat * (100 + bonusPercent) / 100,
            FastidiousnessType.meh => sat * (100 - bonusPercent) / 100,
            FastidiousnessType.bad => -(sat *  bonusPercent / 100),
            _ => sat
        };
        return value;
    }
}
public enum FastidiousnessType
{
    fine,
    meh,
    bad
}