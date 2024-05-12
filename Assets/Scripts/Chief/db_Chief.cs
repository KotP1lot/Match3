using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class db_Chief : ScriptableObject
{
    public List<ChiefSO> unlocked;
    public List<ChiefSO> all;
    public List<ChiefSO> notUnlocked;
    [Serializable]
    class UnlockedChiefData
    {
        public List<ChiefSO> unlocked;
    }
    public void UnlockChief(ChiefSO chief)
    {
        if (unlocked.Contains(chief)) return;
        unlocked.Add(chief);
        notUnlocked.Remove(chief);
        Save();
    }
    public List<ChiefSO> GetUnlockedChiefsByType(GemType gem)
    {
        return unlocked.FindAll(chief => chief.gemType == gem);
    }
    public List<ChiefSO> GetChiefsByType(GemType gem) 
    {
        return all.FindAll(chief => chief.gemType == gem);
    }
    private List<ChiefSO> GetAllNotUnlocked()
    {
        List<ChiefSO> notUnlocked = new List<ChiefSO>();
        foreach (var chief in all)
        {
            if (!unlocked.Contains(chief))
            {
                notUnlocked.Add(chief);
            }
        }
        return notUnlocked;
    }
    public void Setup() 
    {
        Load();
        notUnlocked = GetAllNotUnlocked();
    }
    public void Save() 
    {
        string chiefs = JsonUtility.ToJson(new UnlockedChiefData() {unlocked = unlocked});
        PlayerPrefs.SetString("UnlockedChief", chiefs);
        Debug.Log(chiefs);
    }
    public void Load() 
    {
        string chiefs = PlayerPrefs.GetString("UnlockedChief");
        UnlockedChiefData data = JsonUtility.FromJson<UnlockedChiefData>(chiefs);
        unlocked = data.unlocked; 
    }
}
