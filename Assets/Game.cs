using UnityEngine;

public class Game : MonoBehaviour
{
    public int TotalScore{ get; private set; }
    [SerializeField] GridManager gridManager;

    public void Start()
    {
        EventManager.instance.OnScoreUpdate += OnGemDestroyedHandler;
        TotalScore = 0;
    }
    public void OnGemDestroyedHandler(int score) 
    {
        TotalScore += score;
        UIDebug.Instance.Show($"Score:", $"{TotalScore}");
    }
}
