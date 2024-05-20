using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LvlBtn : MonoBehaviour, IPointerClickHandler
{
    public event Action<LvlPlayerData> OnLvlClick;
    [SerializeField] TextMeshProUGUI date;
    [SerializeField] Image lockImg;
    [SerializeField] Image currImg;
    [SerializeField] BtnStarsManager starsManager;
    [SerializeField] Image prevMonth;
    private LvlPlayerData lvlData;
    private bool isUnlocked;
    public void Setup(LvlPlayerData so)
    {
        Deactivate();
        if (so == null)
        {
            prevMonth.gameObject.SetActive(true);
            return;
        }
        isUnlocked = so.unlocked;
        if (!isUnlocked)
        {
            lockImg.gameObject.SetActive(true);
        }
        else 
        {
            starsManager.gameObject.SetActive(true);
            starsManager.Setup(so.stars);
        }
        if (so.curent) 
        {
            currImg.gameObject.SetActive(true);
        }
        date.gameObject.SetActive(true);
        date.text = so.lvl.Day.ToString();
        lvlData = so;
    }
    private void Deactivate()
    {
        date.gameObject.SetActive(false);
        lockImg.gameObject.SetActive(false);
        currImg.gameObject.SetActive(false);
        starsManager.gameObject.SetActive(false);
        prevMonth.gameObject.SetActive(false);
        isUnlocked = false;
        lvlData = null;
    }
    private void OnCklick() 
    {
        if(!isUnlocked)return;
        OnLvlClick?.Invoke(lvlData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCklick();
    }
}
