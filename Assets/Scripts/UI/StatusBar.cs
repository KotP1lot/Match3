using DG.Tweening;
using System;
using System.Threading.Tasks;
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
    public void Setup(int maxValue)
    {
        this.maxValue = maxValue;
        transform.DOScaleX(0, 0f);
        progressValue = 0;
        string textStr = $"{progressValue} / {maxValue}";
        text.text = textStr;
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
    public async Task Deactivate()
    {
        await transform.DOScaleX(0, 0.2f).SetEase(Ease.InBounce).AsyncWaitForCompletion();
        gameObject.SetActive(false);
    }
    public void Activate()
    {
        gameObject.SetActive(true);
        transform.DOScaleX(1, 0.5f).SetEase(Ease.OutBounce);
    }
    private void OnChangeProgressValue()
    {
        string textStr = $"{progressValue} / {maxValue}";
        text.text = textStr;
        float curentPercent = (float)progressValue / (float)maxValue;
        transform.DOShakeScale(0.2f, 0.1f);
        progress.transform.DOScaleX(curentPercent, 0.2f);
    }

}
