using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScrin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statText;
    public void ShowStat(string stat) 
    {
        gameObject.SetActive(true);
        statText.text = stat;
    }
    public void StartNewGame() 
    {
        SceneManager.LoadScene(0);
    }
}
