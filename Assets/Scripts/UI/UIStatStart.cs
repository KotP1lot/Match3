using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIStatStart : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] RectTransform rect;
    Image curImg;
    [SerializeField] TextMeshProUGUI day;
    [SerializeField] LocalizeStringEvent month;
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] LocalizeStringEvent chiefs;
    [SerializeField] List<TextMeshProUGUI> starsTxt;

    [SerializeField] List<Image> stars;

    [SerializeField] Sprite Completed;
    [SerializeField] Sprite None;

    [SerializeField] Image info;
    [SerializeField] Image all;
    [SerializeField] Image chief;

    [SerializeField] UIGoalCalendar goals;

    public void OnPointerClick(PointerEventData eventData)
    {
        rect.DOKill();
        rect.DOAnchorPosX(2000, 0f);
        curImg.enabled = false;
    }

    public void Setup(LvlPlayerData data)
    {

        if (data == null) return;
        curImg = GetComponent<Image>();
        rect.DOAnchorPosX(0, 1.5f).SetEase(Ease.InOutBack).OnComplete(
            () =>
            {
                rect.DOAnchorPosX(0, 1.5f).OnComplete(
                    () =>
                    {
                        rect.DOAnchorPosX(2000, 1.5f).SetEase(Ease.InOutBack);
                        curImg.enabled = false;
                    }
                );
            });
        day.text = data.lvl.Day.ToString();
        month.SetEntry(data.lvl.Month.ToString());
        goals.Setup(data.lvl.goals);
        for (int i = 0; i < 3; i++)
        {
            starsTxt[i].text = data.lvl.turns.turnForStar[i].ToString();
            stars[i].sprite = i + 1 <= data.stars ? Completed : None;
        }
        if (data.lvl.unlockChief != null)
        {
            chief.gameObject.SetActive(true);
            chiefs.SetEntry(data.lvl.unlockChief.name);
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
