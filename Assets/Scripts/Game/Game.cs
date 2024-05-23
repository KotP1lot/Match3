using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] CustomerManager customerManager;
    [SerializeField] GoalManager goalManager;
    [SerializeField] LvlPlayerData playerLvlData;
    [SerializeField] LvlPlayerData textData;
    [SerializeField] db_Chief chiefs;
    public TurnManager turnManager;
    public EnergyManager energyManager;
    public static GameStat gameStat;

    [Header("Menu bars")]
    [SerializeField] UIStatStart menuStart;
    [SerializeField] UIStatLvl menuPause;
    [SerializeField] UISuccess menuSuccess;
    [SerializeField] UIFailure menuFailure;
   
    public void Start()
    {
        playerLvlData = LvlSelector.LvL;
        if (playerLvlData == null)
            playerLvlData = textData;
        LvlSO lvl = playerLvlData?.lvl;
        turnManager = new TurnManager(lvl.turns) ;
        turnManager.OnTurnsEnded += OnGameFailed;
        customerManager.Setup(lvl.customers, lvl.moneyFromCustomer);
        goalManager.Setup(lvl?.goals);
        goalManager.SetupTurns(turnManager);
        gridManager.Setup(lvl);
        menuStart.Setup(playerLvlData);
        menuPause.Setup(playerLvlData, false);
        energyManager.Setup();

        EventManager.instance.OnAllGoalAchived += OnGameSuccess;
    }
    public void RestartGame() 
    {
        energyManager.SpendEnergy(2);
        LvlSelector.LvL = playerLvlData;
        SceneManager.LoadScene(0);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void GetBonusMoney()
    {
        menuSuccess.ShowAd();
        gameStat.money += gameStat.money * 50 / 100;
        menuSuccess.Setup(gameStat);
    }
    public void GetExtraTurns() 
    {
        turnManager.curentTurn -= 2;
        goalManager.UpdateTurns(turnManager);
        menuFailure.gameObject.SetActive(false);
    }
    private void OnGameSuccess() 
    {
        menuSuccess.gameObject.SetActive(true);
        int stars = Mathf.Clamp(turnManager.GetStars() - playerLvlData.stars, 0, 3);
        gameStat = new()
        {
            lvl = playerLvlData.lvl,
            lvlStars = turnManager.GetStars(),
            stars = stars,
            money = playerLvlData.moneyReceived ? customerManager.totalMoney : customerManager.totalMoney + playerLvlData.lvl.moneyFromLvl,
            moneyFromLvlRecived = true
        };
        menuSuccess.Setup(gameStat);
    }
    private void OnGameFailed() 
    {
        menuFailure.gameObject.SetActive(true);
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
