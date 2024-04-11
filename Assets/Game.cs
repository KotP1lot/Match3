using UnityEngine;

public class Game : MonoBehaviour
{
    public int TotalScore{ get; private set; }
    [SerializeField] GridManager gridManager;
    [SerializeField] CustomerManager customerManager;

    public void Start()
    {
        customerManager.Setup(200, 400);
        EventManager.instance.OnGemDestroy += (gem)=> OnGemDestroyedHandler(gem.GetScore());
        EventManager.instance.OnChiefBonus += OnGemDestroyedHandler;
        TotalScore = 0;
    }
    public void OnGemDestroyedHandler(int score) 
    {
        TotalScore += score;
        UIDebug.Instance.Show($"score:", $"{TotalScore}");
    }
}
