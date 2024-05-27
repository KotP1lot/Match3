using System;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
public class UITips : MonoBehaviour
{
    public Action OnTipsEnded;
    [SerializeField] private VideoPlayer video;
    [SerializeField] private TextMeshProUGUI tips;
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
        tips.text = data.lvl.tips.text;
    }
    public void OnConfirm()
    {
        plData.tipsShowed = true;
        OnTipsEnded?.Invoke();
        gameObject.SetActive(false);
    }
}