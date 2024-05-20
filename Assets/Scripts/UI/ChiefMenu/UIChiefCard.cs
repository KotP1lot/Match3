using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIChiefCard : MonoBehaviour, IPointerClickHandler
{
    public event Action<ChiefPlayerData> OnOpenChief;
    public ChiefPlayerData data;
    [SerializeField] Image isUnlockedImg;
    [SerializeField] Image chiefImg;
    [SerializeField] TextMeshProUGUI lvlText;
    bool isUnlocked;
    public void Setup(ChiefPlayerData data)
    {
        this.data = data;
        isUnlocked = data.unlocked;
        if (!data.unlocked)
        {
            chiefImg.gameObject.SetActive(false);
            lvlText.gameObject.SetActive(false);
            isUnlockedImg.gameObject.SetActive(true);
            return;
        }
        isUnlockedImg.gameObject.SetActive(false);
        lvlText.text = data.lvl.ToString();
        lvlText.gameObject.SetActive(true);
        chiefImg.sprite = data.chief.GetLvlInfo(data.lvl).sprite;
        chiefImg.gameObject.SetActive(true);
    }
    public void SetActive(bool isActive) 
    {
        gameObject.SetActive(isActive);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isUnlocked) return;
        OnOpenChief?.Invoke(data);
    }
}
