using UnityEngine;

public class Game : MonoBehaviour
{
    public int TotalScore{ get; private set; }
    [SerializeField] GridManager gridManager;
    [SerializeField] CustomerManager customerManager;
    [SerializeField] EndScrin scrin;
    int customerSut = 0;
    int maxCombo = 0;
    int gemDestroyed = 0;
    int turnCount = 0;
    float timeToTry = 0;
    string stat;

    public void Start()
    {
        customerManager.Setup(200, 400);
        EventManager.instance.OnGemDestroy += (gem)=> OnGemDestroyedHandler(gem.GetScore());
        EventManager.instance.OnChiefBonus += OnGemDestroyedHandler;

        EventManager.instance.OnGameStarted += OnGameStarted;
        EventManager.instance.OnCustomerSatisfied += OnCustomerSut;
        EventManager.instance.TurnEnded += OnTurnEnded;
        EventManager.instance.OnAllGoalAchived += OnGameFinidhed;
        EventManager.instance.OnMaxComboChanged += OnMaxComboChanged;
        TotalScore = 0;
    }
    private void OnGameFinidhed() 
    {
        float curTime = Time.time;
        float timeElapsed = curTime - timeToTry;
        float min = Mathf.FloorToInt(timeElapsed / 60); 
        float sec = Mathf.FloorToInt(timeElapsed % 60);
        
        stat += $"<color=\"blue\">FED: </color> {customerSut} customers\n";
        stat += $"<color=\"blue\">GEM DESTROYED: </color> {gemDestroyed} gems\n";
        stat += $"<color=\"blue\">MAX COMBO: </color> õ{maxCombo}\n";
        stat += $"<color=\"blue\">TURN SPENT: </color> {turnCount} turn\n";
        if (min != 0)
            stat += $"<color=\"blue\">TIME SPENT: </color> {min}min {sec}sec\n";
        else
            stat += $"<color=\"blue\">TIME SPENT: </color> {sec}sec\n";

        scrin.ShowStat(stat);
    }
    private void OnCustomerSut() => customerSut++;
    private void OnMaxComboChanged(int count)
    {
        if (count > maxCombo) maxCombo = count;
    }
    private void OnTurnEnded()=>turnCount++;
    private void OnGameStarted() => timeToTry = Time.time;
    public void OnGemDestroyedHandler(int score) 
    {
        gemDestroyed++;
        TotalScore += score;
        UIDebug.Instance.Show($"score:", $"{TotalScore}");
    }
}
