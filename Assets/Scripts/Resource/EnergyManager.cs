using UnityEngine;
using System;

public class EnergyManager: MonoBehaviour
{
    public int Energy { get; private set; }
    private int energyRecoveryTime;
    private int maxEnergy;
    private DateTime nextRecoveryDate;
    private DateTime curentTimer;

    public void Setup(EnergySO so)
    {
        energyRecoveryTime = so.energyRecoveryTime;
        maxEnergy = so.maxEnergy;
        if (Energy < maxEnergy)
        {
            int spended = DateTime.Now.Second - nextRecoveryDate.Second;
            int energyCharged = Mathf.Clamp(spended / energyRecoveryTime, 0, maxEnergy - Energy);
            AddEnergy(energyCharged);
            if (Energy != maxEnergy)
            {
                nextRecoveryDate = DateTime.Now.AddSeconds(spended % energyRecoveryTime);
            }
        }
    }
    private void UpdateTime() 
    {
        curentTimer = DateTime.Now;
        nextRecoveryDate.AddSeconds(energyRecoveryTime);
       // InvokeRepeating(nameof(RegenerateEnergy), interval, interval);
    }
    public void AddEnergy(int value) 
    {
        if(Energy + value >= maxEnergy) Energy = maxEnergy;
        Energy += value;
    }
    public void SetEnergyFull() => Energy = maxEnergy;
    public void Save() 
    {
    
    }
    public void Load() 
    {
    
    }
}
