using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScrin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI star;
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] Image continueBtn;
    [SerializeField] Game game;
    private void Start()
    {
        EventManager.instance.OnAllGoalAchived += Show;
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
        int stars = game.turnManager.GetStars();
        star.text += stars.ToString();
        money.text += "багацько";
        if (stars == 0) 
        {
            continueBtn.gameObject.SetActive(false); 
        }
    }
    public void StartNewGame() 
    {
        SceneManager.LoadScene(0);
    }
}
