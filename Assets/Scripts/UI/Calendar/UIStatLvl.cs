using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStatLvl : MonoBehaviour
{
    [SerializeField] EnergyManager energy;

    [SerializeField] TextMeshProUGUI day;
    [SerializeField] TextMeshProUGUI month;
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] TextMeshProUGUI chiefs;
    [SerializeField] List<TextMeshProUGUI> starsTxt;
    
    [SerializeField] List<Image> stars;

    [SerializeField] Sprite Completed;
    [SerializeField] Sprite None;

    [SerializeField] Button start;

    [SerializeField] Image ad;
    [SerializeField] Image info;
    [SerializeField] Image all;
    [SerializeField] Image chief;

    [SerializeField] UIGoalCalendar goals;
    LvlPlayerData lvlData;
    public async void Setup(LvlPlayerData data, bool isActive = true) 
    {
        if (data == null) return;
        lvlData = data;
        day.text = data.lvl.Day.ToString();
        month.text = data.lvl.Month switch 
        {
            MonthType.cherven => "Червець",
            MonthType.lupen => "Лапень",
           // MonthType.serpen => "Кивень",
            _=> "Червець"
        
        };

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
        if (data.lvl.unlockChief != null)
        {
            chief.gameObject.SetActive(true);
            chiefs.text = data.lvl.unlockChief.name;
        }
        else 
        {
            chief.gameObject.SetActive(false);
        }
        money.text = data.lvl.moneyFromLvl.ToString();
        LayoutRebuilder.ForceRebuildLayoutImmediate(all.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(info.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();

        gameObject.SetActive(isActive);
    }
    public void StartLvl() 
    {
        LvlSelector.LvL = lvlData;
        energy.SpendEnergy(2);
        SceneManager.LoadScene(1);
    }
}
