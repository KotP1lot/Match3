using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISuccess : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI day;
    [SerializeField] TextMeshProUGUI month;
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] TextMeshProUGUI chiefs;
    [SerializeField] List<TextMeshProUGUI> starsTxt;

    [SerializeField] List<Image> stars;

    [SerializeField] Image ad;
    [SerializeField] Image chief;

    [SerializeField] Sprite Completed;
    [SerializeField] Sprite None;
    bool isAddView = false;
    public void Setup(GameStat data)
    {
        if (data == null) return;
        day.text = data.lvl.Day.ToString();
        month.text = data.lvl.Month.ToString();

        for (int i = 0; i < 3; i++)
        {
            starsTxt[i].text = data.lvl.turns.turnForStar[i].ToString();
            stars[i].sprite = i + 1 <= data.stars ? Completed : None;
        }
        if (data.lvl.unlockChief != null)
        {
            chief.gameObject.SetActive(true);
            chiefs.text = data.lvl.unlockChief.name;
        }
        else
        {
            chief.gameObject.SetActive(false);
        }
        ad.gameObject.SetActive(!isAddView);
        money.text = data.money.ToString(); 
        gameObject.SetActive(true);
    }
    public void ShowAd() 
    {
        isAddView = true;
    }
}
