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
        if (curentTurn <= turnInfo.minFor3Star) return 3;
        else if (curentTurn <= turnInfo.minFor2Star) return 2;
        else if (curentTurn <= turnInfo.minFor1Star) return 1;
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
        if (curentTurn >= turnInfo.minFor3Star) OnStartInfoChanged?.Invoke(2);
        else if (curentTurn >= turnInfo.minFor2Star) OnStartInfoChanged?.Invoke(1);
        else if (curentTurn >= turnInfo.minFor1Star) OnStartInfoChanged?.Invoke(0);
        if (curentTurn > turnInfo.max) 
        {
            OnTurnsEnded?.Invoke();
        }
    }
}
