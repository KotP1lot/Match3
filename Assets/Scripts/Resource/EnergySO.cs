using System;
using UnityEngine;

[CreateAssetMenu()]
public class EnergySO : ISaveLoadSO
{
    public Action OnSetup;
    public int maxEnergy;
    public int energyRecoveryTime;
    public int energy;
    public string lastSavedTime;
    public int timeLeftToRecharge;
    private PlayerEnergyData loadedData;


    public override void Clear()
    {
        PlayerPrefs.DeleteKey("PlayerEnergy");
        energy = 1;
        maxEnergy = 15;
        timeLeftToRecharge = energyRecoveryTime;
    }

    public override void Setup()
    {
        if (loadedData != null)
        {
            maxEnergy = loadedData.maxEnergy;
            energy = loadedData.energy;
            lastSavedTime = loadedData.lastSavedTime;
            DateTime savedTime = DateTime.Parse(lastSavedTime);
            timeLeftToRecharge = loadedData.timeLeftToRecharge;
            int timeSinceLastSave = (int)(DateTime.Now - savedTime).TotalSeconds;
            Debug.Log($"Час збереження: {savedTime} | Поточний час {DateTime.Now} | Пройшло секунд {timeSinceLastSave}");
            if (timeSinceLastSave >= timeLeftToRecharge) 
            {
                timeSinceLastSave -= timeLeftToRecharge;
                timeLeftToRecharge = energyRecoveryTime;
                energy = Mathf.Clamp(energy + 1, 0, maxEnergy);
                if (timeSinceLastSave > 0) 
                {
                    int energyGained = (timeSinceLastSave / energyRecoveryTime);
                    timeLeftToRecharge = (timeSinceLastSave % energyRecoveryTime);
                    energy = Mathf.Clamp(energy + energyGained, 0, maxEnergy);
                }
            }
            else timeLeftToRecharge -= timeSinceLastSave;
        }
        else
        {
            energy = 1;
            maxEnergy = 15;
           timeLeftToRecharge = energyRecoveryTime;
        }

        OnSetup?.Invoke();
    }

    public void AddEnergy(int value)
    {
        energy = Mathf.Clamp(energy + value, 0, maxEnergy);
        Save();
    }

    public bool SpendEnergy(int value)
    {
        if (energy < value)
            return false;

        energy -= value;
        Save();
        return true;
    }
    public void AddMaxEnergy(int value) 
    {
        maxEnergy += value;
        Save();
    }
    public override void Load()
    {
        string strData = PlayerPrefs.GetString("PlayerEnergy");
        loadedData = JsonUtility.FromJson<PlayerEnergyData>(strData);
    }

    public override void Save()
    {
        lastSavedTime = DateTime.Now.ToString();
        string data = JsonUtility.ToJson(new PlayerEnergyData(maxEnergy, energy, lastSavedTime, timeLeftToRecharge));
        PlayerPrefs.SetString("PlayerEnergy", data);
        Debug.Log(data);
    }
}
public class PlayerEnergyData
{
    public int maxEnergy;
    public int energy;
    public string lastSavedTime;
    public int timeLeftToRecharge;

    public PlayerEnergyData(int maxEnergy, int energy, string lastSavedTime, int timeLeftToRecharge)
    {
        this.maxEnergy = maxEnergy;
        this.energy = energy;
        this.lastSavedTime = lastSavedTime;
        this.timeLeftToRecharge = timeLeftToRecharge;
    }
}