public class Fastidiousness
{
    public int bonusPercent;
    public FastidiousnessType type;
    public int Bonus(int sat)
    {
        return type switch
        {
            FastidiousnessType.fine => sat * (100 + bonusPercent) / 100,
            FastidiousnessType.meh => sat * (100 - bonusPercent) / 100,
            FastidiousnessType.bad => -(sat * (100 - bonusPercent) / 100),
            _ => 0
        };
    }
}
public enum FastidiousnessType
{
    fine,
    meh,
    bad
}