using System;
using TMPro;
using UnityEngine;

public class UIEnergy : MonoBehaviour
{
    [SerializeField] EnergyManager energyManager;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI timer;
    private void Start()
    {
        ChangeEnergyText();
        ChangeTime();

    }
    private void OnEnable()
    {
        energyManager.OnEnergyCharged += HideTimer;
        energyManager.OnTimeChanged += ChangeTime;
        energyManager.OnEnergyChanged += ChangeEnergyText;
    }

    private void OnDisable()
    {
        energyManager.OnEnergyCharged -= HideTimer;
        energyManager.OnTimeChanged -= ChangeTime;
        energyManager.OnEnergyChanged -= ChangeEnergyText;
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
            return;
        }  
        if (!timer.gameObject.activeSelf)
            timer.gameObject.SetActive(true);
        int timeLeft = energyManager.energySO.timeLeftToRecharge;
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timer.text = $"{minutes:00}:{seconds:00}";
    }

    private void HideTimer()
    {
        timer.gameObject.SetActive(false);
    }
}