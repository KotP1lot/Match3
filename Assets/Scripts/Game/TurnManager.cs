using System;

public class TurnManager
{
    public event Action<int> OnStartInfoChanged;
    public event Action OnTurnsEnded;
    public TurnInfo turnInfo;
    public int curentTurn;
    public TurnManager(TurnInfo turnInfo) 
    {
        this.turnInfo = turnInfo;
        Subcribe();
    }
    public int GetStars()
    {
        if (curentTurn <= turnInfo.turnForStar[0]) return 3;
        else if (curentTurn <= turnInfo.turnForStar[1]) return 2;
        else if (curentTurn <= turnInfo.turnForStar[2]) return 1;
        else return 0;
    }
    private void Subcribe() 
    {
        EventManager.instance.OnTurnEnded += OnTurnEndedHandler;
        EventManager.instance.OnAllGoalAchived += OnAllGoalAchivedHandler;
    }
    private void Unsubscrive() 
    {
        EventManager.instance.OnTurnEnded -= OnTurnEndedHandler;
        EventManager.instance.OnAllGoalAchived -= OnAllGoalAchivedHandler;
    }
    private void OnAllGoalAchivedHandler() 
    {
        Unsubscrive();
    }
    private void OnTurnEndedHandler() 
    {
        curentTurn++;
        if (curentTurn >= turnInfo.turnForStar[2]) OnStartInfoChanged?.Invoke(2);
        else if (curentTurn >= turnInfo.turnForStar[1]) OnStartInfoChanged?.Invoke(1);
        else if (curentTurn >= turnInfo.turnForStar[0]) OnStartInfoChanged?.Invoke(0);
        if (curentTurn > turnInfo.max) 
        {
            OnTurnsEnded?.Invoke();
        }
    }
}
