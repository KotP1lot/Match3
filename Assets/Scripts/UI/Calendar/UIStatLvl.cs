using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatLvl : MonoBehaviour
{
    [SerializeField] EnergyManager energy;

    [SerializeField] TextMeshProUGUI day;
    [SerializeField] TextMeshProUGUI month;
    [SerializeField] List<TextMeshProUGUI> starsTxt;
    
    [SerializeField] List<Image> stars;

    [SerializeField] Sprite Completed;
    [SerializeField] Sprite None;

    [SerializeField] Button start;

    [SerializeField] Image ad;
    [SerializeField] Image info;
    [SerializeField] Image all;

    [SerializeField] UIGoalCalendar goals;

    public void Setup(LvlPlayerData data) 
    {
        day.text = data.lvl.Day.ToString();
        month.text = data.lvl.Month.ToString();
        goals.Setup(data.lvl.goals);
        for (int i = 0; i < 3; i++)
        {
            starsTxt[i].text = data.lvl.turns.turnForStar[i].ToString();
            stars[i].sprite = i + 1 <= data.stars? Completed:None;
        }

        if (energy.GetEnergy() >= 2)
        {
            start.interactable = true;
            ad.gameObject.SetActive(false);
        }
        else 
        {
            start.interactable = false;
            ad.gameObject.SetActive(true);
        }
        gameObject.SetActive(true); 
        LayoutRebuilder.ForceRebuildLayoutImmediate(info.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(all.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
    }
}
