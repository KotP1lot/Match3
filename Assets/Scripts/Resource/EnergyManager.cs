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

    // ��������� ��� �� ���������� ���������� ����㳿

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
            InvokeRepeating(nameof(UpdateEnergy), 0f, 1f); // ������ ������ ����� �������
        }
        else
        {
            OnEnergyCharged?.Invoke();
        }
    }

    private void UpdateEnergy()
    {
        energySO.timeLeftToRecharge -= 1; // �������� ��� �� ���������� ����㳿 �� ���� �������
        OnTimeChanged?.Invoke();

        if (energySO.timeLeftToRecharge <= 0)
        {
            energySO.AddEnergy(1);
            energySO.timeLeftToRecharge = energySO.energyRecoveryTime; // ������������ ��� �� ���������� ���������� ����㳿
            OnEnergyChanged?.Invoke();

            if (energySO.energy >= energySO.maxEnergy)
            {
                isCharging = false;
                CancelInvoke(nameof(UpdateEnergy)); // ��������� ������ ������, ���� ������ ��������� ���������
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
                energySO.timeLeftToRecharge = energySO.energyRecoveryTime; // ������������ ��� �� ���������� ���������� ����㳿
                InvokeRepeating(nameof(UpdateEnergy), 0f, 1f); // ������ ����, ���� ������ ���� ��������� � ���� �� ������� ���������
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