using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : MonoBehaviour
{
    [SerializeField] EnergyManager energyManager;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] Image adBtn;
    private bool isAdLoaded;
    private void Start()
    {
        ChangeEnergyText();
        ChangeTime();

    }
    private void OnEnable()
    {
        energyManager.OnEnergyCharged += HideTimer;
        energyManager.OnTimeChanged += ChangeTime;
        energyManager.energySO.OnUpdate += ChangeEnergyText;
    }

    //private void AdLoaded(bool isLoaded)
    //{
    //    isAdLoaded = true;
    //    if (isAdLoaded && energyManager.GetEnergy() <= energyManager.energySO.maxEnergy)
    //        adBtn.gameObject.SetActive(true);
    //}
    //private void AdShowed()
    //{
    //    isAdLoaded = false;
    //}

    private void OnDisable()
    {
        energyManager.OnEnergyCharged -= HideTimer;
        energyManager.OnTimeChanged -= ChangeTime;
        energyManager.energySO.OnUpdate -= ChangeEnergyText;
    }

    private void ChangeEnergyText()
    {
        energyText.text = $"{energyManager.energySO.energy}/{energyManager.energySO.maxEnergy}";
    }

    private void ChangeTime()
    {
        if (energyManager.GetEnergy() >= energyManager.energySO.maxEnergy)
        {
            timer.gameObject.SetActive(false);
            adBtn.gameObject.SetActive(false);
            return;
        }
        if (!timer.gameObject.activeSelf)
        {
            timer.gameObject.SetActive(true);
            adBtn.gameObject.SetActive(true);
        }
        int timeLeft = energyManager.energySO.timeLeftToRecharge;
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timer.text = $"{minutes:00}:{seconds:00}";
    }

    private void HideTimer()
    {
        timer.gameObject.SetActive(false);
        adBtn.gameObject.SetActive(false);
    }
}