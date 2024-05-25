using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatStart : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] TextMeshProUGUI day;
    [SerializeField] TextMeshProUGUI month;
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] TextMeshProUGUI chiefs;
    [SerializeField] List<TextMeshProUGUI> starsTxt;

    [SerializeField] List<Image> stars;

    [SerializeField] Sprite Completed;
    [SerializeField] Sprite None;

    [SerializeField] Image info;
    [SerializeField] Image all;
    [SerializeField] Image chief;

    [SerializeField] UIGoalCalendar goals;

    public void Setup(LvlPlayerData data)
    {

        if (data == null) return;
        rect = GetComponent<RectTransform>();
        rect.DOAnchorPosX(0, 1.5f).SetEase(Ease.InOutBack).OnComplete(
            () =>
            {
                rect.DOAnchorPosX(0, 1.5f).OnComplete(
                    () => rect.DOAnchorPosX(2000, 1.5f).SetEase(Ease.InOutBack));
            }
            );
        day.text = data.lvl.Day.ToString();
        month.text = data.lvl.Month.ToString();
        goals.Setup(data.lvl.goals);
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
        money.text = data.lvl.moneyFromLvl.ToString();
        gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(info.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(all.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();

    }
}
