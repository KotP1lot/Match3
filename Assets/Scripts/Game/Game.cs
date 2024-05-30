using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

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
    [SerializeField] UITips tips;
   
    public void Start()
    {
        gameStat = null;
        playerLvlData = LvlSelector.LvL;
        if (playerLvlData == null)
            playerLvlData = textData;
        tips.OnTipsEnded += OnTipsEnded;
        tips.Setup(playerLvlData);
        EventManager.instance.OnAllGoalAchived += OnGameSuccess;
    }
    private void OnTipsEnded()
    {
        LvlSO lvl = playerLvlData?.lvl;
        turnManager = new TurnManager(lvl.turns);
        turnManager.OnTurnsEnded += OnGameFailed;
        customerManager.Setup(lvl.customers, lvl.moneyFromCustomer);
        goalManager.Setup(lvl?.goals);
        goalManager.SetupTurns(turnManager);
        gridManager.Setup(lvl);
        menuStart.Setup(playerLvlData);
        menuPause.Setup(playerLvlData, false);
        energyManager.Setup();
    }
    public void RestartGame()
    {
        DOTween.KillAll();
            LvlSelector.PlayedCount++;
        if (LvlSelector.PlayedCount % 3 == 0)
        {
            AdManager.Instance.interstitial.ShowAd(() =>
            {
                energyManager.SpendEnergy(2);
                LvlSelector.LvL = playerLvlData;
                SceneManager.LoadScene(1);
            },
            () =>
            { });
            return;
        }
        energyManager.SpendEnergy(2);
        LvlSelector.LvL = playerLvlData;
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }
    public void GetBonusMoney()
    {
        AdManager.Instance.rewarded.ShowAd(() =>
        {
            menuSuccess.ShowAd();
            gameStat.money += gameStat.money * 50 / 100;
            menuSuccess.Setup(gameStat);
        }, () => 
        {
            menuSuccess.Setup(gameStat);
        });
    }
    public void GetExtraTurns()
    {
        AdManager.Instance.rewarded.ShowAd(() =>
        {
            turnManager.curentTurn -= 2;
            goalManager.UpdateTurns(turnManager);
            menuFailure.gameObject.SetActive(false);
        },
        () => 
        { });
    }
    public void GetExtraEnergyAndRestart()
    {
        AdManager.Instance.rewarded.ShowAd(() =>
        {
            LvlSelector.LvL = playerLvlData;
            SceneManager.LoadScene(1);
        }, () => { });

    }
    private void OnGameSuccess() 
    {
        menuSuccess.gameObject.SetActive(true);
        Debug.Log($"{turnManager.GetStars()} // {turnManager.curentTurn}");
        Debug.Log($"{playerLvlData.stars}");
        int stars = Mathf.Clamp(turnManager.GetStars() - playerLvlData.stars, 0, 3);
        gameStat = new()
        {
            lvl = playerLvlData.lvl,
            lvlStars = turnManager.GetStars() <= playerLvlData.stars ? playerLvlData.stars : turnManager.GetStars(),
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
