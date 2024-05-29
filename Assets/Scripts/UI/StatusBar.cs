using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Image progress;
    [SerializeField] TextMeshProUGUI text;
    public event Action OnComplited;
    private int progressValue;
    private int maxValue;
    //bool isMoving;
    //bool isChange;
    public void Setup(int maxValue)
    {
        this.maxValue = maxValue;
        progressValue = 0;
      /*  if(!isMoving) */OnChangeProgressValue();
        //else isChange = true;
    } 
    public void Setup(int maxValue, int currentValue)
    {
        this.maxValue = maxValue;
        progressValue = currentValue;
        OnChangeProgressValue();
    }
    public void AddProgress(int value)
    {
        progressValue += value;
        if (progressValue >= maxValue) OnComplited?.Invoke(); 
        progressValue = Mathf.Clamp(progressValue, 0, maxValue);
        OnChangeProgressValue();
    }
    public void ChangeProgress(int progressValue)
    {
        this.progressValue = progressValue;
        if (progressValue >= maxValue) OnComplited?.Invoke();
        this.progressValue = Mathf.Clamp(progressValue, 0, maxValue);
        OnChangeProgressValue();
    }
    public void SetActive(bool isActive)
    {
        //isMoving = true;
        GetComponent<RectTransform>().DOAnchorPosX(isActive?20f:-600f, 1f).SetEase(Ease.InOutBack)/*.OnComplete(() => { isMoving = false; if (isChange) OnChangeProgressValue(); })*/;
    }
    private void OnChangeProgressValue()
    {
        string textStr = $"{maxValue - progressValue}";
        text.text = textStr;
        float curentPercent = (float)progressValue / (float)maxValue;
        progress.transform.DOScaleY(curentPercent, 0.2f);
    }

}
