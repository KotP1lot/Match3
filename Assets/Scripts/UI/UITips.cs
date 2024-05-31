using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using UnityEngine.Video;
public class UITips : MonoBehaviour
{
    public Action OnTipsEnded;
    [SerializeField] private VideoPlayer video;
    [SerializeField] private LocalizeStringEvent tips;
    public LvlPlayerData plData;
    public void Setup(LvlPlayerData data)
    {
        plData = data;
        if (data.lvl.tips.video == null || data.tipsShowed)
        {
            OnTipsEnded?.Invoke();
            gameObject.SetActive(false);
            return;
        }
        video.clip = data.lvl.tips.video;
        tips.SetEntry(data.lvl.tips.text);
        LayoutRebuilder.ForceRebuildLayoutImmediate(tips.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        Canvas.ForceUpdateCanvases();
    }
    public void OnConfirm()
    {
        plData.tipsShowed = true;
        OnTipsEnded?.Invoke();
        gameObject.SetActive(false);
    }
}