using UnityEngine;
using System;

public class EnergyManager : MonoBehaviour
{
    public event Action OnTimeChanged;
    public event Action OnEnergyCharged;
    public event Action OnEnergyChanged;
    public EnergySO energySO;
    private bool isCharging;
    private bool delete;

    // Залишений час до наступного поповнення енергії

    private void Awake()
    {
        energySO.OnSetup += Setup;
        energySO.Load();
        energySO.Setup();
    }

    private void Setup()
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
            OnEnergyChanged?.Invoke();

            if (energySO.energy >= energySO.maxEnergy)
            {
                isCharging = false;
                CancelInvoke(nameof(UpdateEnergy)); // Скасувати виклик методу, коли енергія досягнута максимуму
                OnEnergyCharged?.Invoke();
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) delete = true;
        if (Input.GetKeyDown(KeyCode.Space)) SpendEnergy();
    }
    public void SpendEnergy()
    {
        if (energySO.SpendEnergy(1))
        {
            if (!isCharging)
            {
                isCharging = true;
                energySO.timeLeftToRecharge = energySO.energyRecoveryTime; // Встановлюємо час до наступного поповнення енергії
                InvokeRepeating(nameof(UpdateEnergy), 0f, 1f); // Почати відлік, якщо енергія була витрачена і вона не досягла максимуму
            }
            OnEnergyChanged?.Invoke();
        }
    }

    private void OnDisable()
    {
        if (!delete) energySO.Save();
        else energySO.Clear();
    }
    public int GetEnergy() => energySO.energy;
}