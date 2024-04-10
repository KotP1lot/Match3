using UnityEngine;

public class Game : MonoBehaviour
{
    public int TotalScore{ get; private set; }
    [SerializeField] GridManager gridManager;
    [SerializeField] CustomerManager customerManager;

    public void Start()
    {
        customerManager.Setup(200, 400);
        customerManager.SpawnNewCustomer();
        EventManager.instance.OnGemDestroy += (gem)=> OnGemDestroyedHandler(gem.Info.Score);
        TotalScore = 0;
    }
    public void OnGemDestroyedHandler(int score) 
    {
        TotalScore += score;
        UIDebug.Instance.Show($"Score:", $"{TotalScore}");
    }
}
