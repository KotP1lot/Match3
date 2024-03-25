using UnityEngine;

public class Game : MonoBehaviour
{
    public int TotalScore{ get; private set; }
    [SerializeField] GridManager gridManager;

    public void Start()
    {
        EventManager.instance.OnGemDestroy += OnGemDestroyedHandler;
        TotalScore = 0;
    }
    public void OnGemDestroyedHandler(Gem gem) 
    {
        TotalScore += gem.Info.Score;
        UIDebug.Instance.Show($"Score:", $"{TotalScore}");
    }
}
