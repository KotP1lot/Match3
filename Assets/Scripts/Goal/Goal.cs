using System;
[Serializable]
public class Goal
{
    public Action<Goal> OnGoalAchived;
    public Action<int> OnGoalStateChanged;
    public GoalType type;
    public bool isDone;
    public int count;
    public int currentCount;
    public GemType gemType;
    virtual public void Setup(GoalType type, int count, GemType gem = GemType.fish) 
    {
        this.type = type;
        this.count = count;
        gemType = gem;
        isDone = false;
        currentCount = count;
        Subcribe();
    }
    virtual public void Setup()
    {
        isDone = false;
        currentCount = count;
        Subcribe();
    }
    void Subcribe() 
    {
        switch (type)
        {
            case GoalType.gem:
                EventManager.instance.OnGemDestroy += (Gem gem) =>
                {
                    if (gem.GetGemType() == gemType) ChangeState();
                };
                break;
            case GoalType.feed:
                EventManager.instance.OnCustomerSatisfied += () => { ChangeState(); };
                break;
            case GoalType.clean:
                EventManager.instance.OnFloorCleaned += () => { ChangeState(); };
                break;
        }
    
    }
    void ChangeState() 
    {
        if (isDone) return;
        currentCount--;
        if (currentCount <= 0)
        {
            isDone = true;
            OnGoalAchived?.Invoke(this);
            return;
        }
        OnGoalStateChanged?.Invoke(currentCount);
    }
}
public enum GoalType 
{
    gem,
    feed,
    clean
}