using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EnergyManager : MonoBehaviour
{
    public event Action OnTimeChanged;
    public event Action OnEnergyCharged;
    public EnergySO energySO;
    private bool isCharging;
    private bool delete;

    // Залишений час до наступного поповнення енергії

    private void Awake()
    {
        energySO.OnSetup += Setup;
        energySO.OnUpdate += EnergySOChanged;
    }

    public void Setup()
    {
        if (energySO.energy < energySO.maxEnergy)
        {
            isCharging = true;
            InvokeRepeating(nameof(UpdateEnergy), 0f, 1f); // Виклик методу кожну секунду
        }
        else
        {
            OnEnergyCharged?.Invoke();
        }
    }

    private void UpdateEnergy()
    {
        energySO.timeLeftToRecharge -= 1; // Зменшуємо час до поповнення енергії на одну секунду
        OnTimeChanged?.Invoke();

        if (energySO.timeLeftToRecharge <= 0)
        {
            energySO.AddEnergy(1);
            energySO.timeLeftToRecharge = energySO.energyRecoveryTime; // Встановлюємо час до наступного поповнення енергії
            if (energySO.energy >= energySO.maxEnergy)
            {
                isCharging = false;
                CancelInvoke(nameof(UpdateEnergy)); // Скасувати виклик методу, коли енергія досягнута максимуму
                OnEnergyCharged?.Invoke();
            }
        }
    }
    public void SpendEnergy(int value)
    {
        if (energySO.SpendEnergy(value))
        {
            if (!isCharging)
            {
                isCharging = true;
                energySO.timeLeftToRecharge = energySO.energyRecoveryTime; // Встановлюємо час до наступного поповнення енергії
                InvokeRepeating(nameof(UpdateEnergy), 0f, 1f); // Почати відлік, якщо енергія була витрачена і вона не досягла максимуму
            }
        }
    }
    public void EnergySOChanged() 
    { 
        if (energySO.energy < energySO.maxEnergy && !isCharging)
        {
            isCharging = true;
            energySO.timeLeftToRecharge = energySO.energyRecoveryTime; // Встановлюємо час до наступного поповнення енергії
            InvokeRepeating(nameof(UpdateEnergy), 0f, 1f); // Виклик методу кожну секунду
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            CancelInvoke(nameof(UpdateEnergy));
        }
    }
    private void OnDisable()
    {
        energySO.OnSetup -= Setup;
        energySO.OnUpdate -= EnergySOChanged;
        CancelInvoke(nameof(UpdateEnergy));
    }
    public int GetEnergy() => energySO.energy;

}