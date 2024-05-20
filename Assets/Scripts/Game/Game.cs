using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public int TotalScore{ get; private set; }
    [SerializeField] GridManager gridManager;
    [SerializeField] CustomerManager customerManager;
    [SerializeField] GoalManager goalManager;
    [SerializeField] LvlPlayerData playerLvlData;
    [SerializeField] db_Chief chiefs;
    [SerializeField] EndScrin endScrin;
    public TurnManager turnManager;
    public static GameStat gameStat;

    public void Start()
    {
        playerLvlData = LvlSelector.LvL;
        LvlSO lvl = playerLvlData.lvl;
        turnManager = new TurnManager(lvl.turns);
        turnManager.OnTurnsEnded += OnGameFailed;
        customerManager.Setup(lvl.customers, lvl.moneyFromCustomer);
        goalManager.Setup(lvl.goals);
        gridManager.Setup(lvl);
        EventManager.instance.OnChiefBonus += (type, value) => OnScoreChange(value);
        TotalScore = 0;
    }
    public void OnGameRestart() 
    {
        LvlSelector.LvL = playerLvlData;
        SceneManager.LoadScene(0);
    }
    public void OnGameFinished()
    {
        int stars = Mathf.Clamp(turnManager.GetStars() - playerLvlData.stars, 0, 3);
        gameStat = new()
        {
            lvl = playerLvlData.lvl,
            lvlStars = turnManager.GetStars(),
            stars = stars,
            money = playerLvlData.moneyReceived?customerManager.totalMoney: customerManager.totalMoney + playerLvlData.lvl.moneyFromLvl,
            moneyFromLvlRecived = true
        };
        SceneManager.LoadScene(1);
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
public class GameStat 
{
    public LvlSO lvl;
    public int stars;
    public int lvlStars;
    public int money;
    public bool moneyFromLvlRecived;
}
