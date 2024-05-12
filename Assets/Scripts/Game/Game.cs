using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int TotalScore{ get; private set; }
    [SerializeField] GridManager gridManager;
    [SerializeField] CustomerManager customerManager;
    [SerializeField] GoalManager goalManager;
    [SerializeField] LvlSO lvl;
    [SerializeField] db_Chief chiefs;
    TurnManager turnManager;

    public void Start()
    {
        lvl = LvlBtn.LvL;
        turnManager = new TurnManager(lvl.turns);
        turnManager.OnTurnsEnded += OnGameFailed;
        customerManager.Setup(200, 400);
        goalManager.Setup(lvl.goals);
        gridManager.Setup(lvl);
        EventManager.instance.OnChiefBonus += (type, value) => OnScoreChange(value);
        EventManager.instance.OnAllGoalAchived += OnGameFinished;
        TotalScore = 0;
    }
    private void OnGameFinished() 
    {
        PlayerWaller.AddStart(turnManager.GetStars());
        chiefs.UnlockChief(lvl.unlockChief);
    }
    private void OnGameFailed() 
    {
        Debug.LogError($" ≤Õ≈÷‹ Á≥ÓÍ{turnManager.GetStars()}");
    }
    public void OnScoreChange(int score) 
    {
        TotalScore += score;
        UIDebug.Instance.Show($"score:", $"{TotalScore}");
    }
}
