public class GemGoal : Goal
{
    public GemType type;
    public override void Setup()
    {
        base.Setup();
        EventManager.instance.OnGemDestroy += ChangeState;
    }
    private void ChangeState(Gem gem) 
    {
        if (gem.GetGemType() == type) 
        {
            currentCount--;
            if (currentCount <= 0) 
            {
                isDone = true;
            }
        }
    }
}
