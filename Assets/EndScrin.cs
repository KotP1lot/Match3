using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScrin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statText;
    [SerializeField] Game game;
    public void ShowStat(string stat) 
    {
        gameObject.SetActive(true);
        statText.text = stat;
    }
    public void StartNewGame() 
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
