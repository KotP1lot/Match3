using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[CreateAssetMenu()]
public class db_LvlSo : ISaveLoadSO
{
    public LvlSO[] lvlSO;
    public List<LvlPlayerData> playerLvlData;
    private List<LvlPlayerData> loadedData;
    public void UpdateLvlData(LvlPlayerData lvl) 
    {
        int dataID = playerLvlData.FindIndex(x => x.lvl == lvl.lvl);
        playerLvlData[dataID].stars = lvl.stars;
        playerLvlData[dataID].moneyReceived = true;
        if (dataID + 1 == playerLvlData.Count) return;
        if (playerLvlData[dataID].curent)
        {
            playerLvlData[dataID].curent = false;
            playerLvlData[dataID + 1].curent = true;
        }
        playerLvlData[dataID + 1].unlocked = true;
       
        Save();
    }
    override public void Setup()
    {
        if (playerLvlData.Count == 0)
        {
            for (int i = 0; i < lvlSO.Length; i++)
            {
                playerLvlData.Add(new() { lvl = lvlSO[i], stars = 0, unlocked = i == 0, curent = i == 0 }); 
            }
        }
        if (loadedData == null) return;
        foreach (var lvl in loadedData)
        {
            if (playerLvlData.Find(x=>x.lvl == lvl.lvl) != null)
            {
                LvlPlayerData data = playerLvlData.Find(x => x.lvl == lvl.lvl);
                data.lvl = lvl.lvl;
                data.unlocked = lvl.unlocked;
                data.stars = lvl.stars;
                data.curent = lvl.curent;
                data.moneyReceived = lvl.moneyReceived;
                data.tipsShowed = lvl.tipsShowed;
            }
        }
    }
    public List<LvlPlayerData> GetUnlocked() 
    {
        return playerLvlData.FindAll(x => x.unlocked);
    }
    public List<LvlPlayerData> GetLvlsByMonth(MonthType month)
    {
        return playerLvlData.FindAll(x => x.lvl.Month == month);
    }
    override public void Save()
    {
        string lvlData = JsonUtility.ToJson(new LvlPlayerDataSave() { passedLvl = playerLvlData });
        PlayerPrefs.SetString("PassedLvl", lvlData);
        PlayerPrefs.Save();
    }
    override public void Load()
    {
        loadedData = null;
        string lvlData = PlayerPrefs.GetString("PassedLvl");
        LvlPlayerDataSave data = JsonUtility.FromJson<LvlPlayerDataSave>(lvlData);
        if (data == null) return;
        loadedData = data.passedLvl;
    }
    public override void SaveGameStat(GameStat stat)
    {
        UpdateLvlData(new() { lvl = stat.lvl, stars = stat.lvlStars, moneyReceived = stat.moneyFromLvlRecived });
        base.SaveGameStat(stat);
    }
    public override void Clear()
    {
        Debug.Log("yes");
        loadedData = null;
        playerLvlData = new();
        Debug.Log(playerLvlData.Count);
        PlayerPrefs.DeleteKey("PassedLvl");
    }
}
public class LvlPlayerDataSave 
{
    public List<LvlPlayerData> passedLvl;
}
[Serializable]
public class LvlPlayerData 
{
    public LvlSO lvl;
    public int stars;
    public bool unlocked;
    public bool curent;
    public bool moneyReceived;
    public bool tipsShowed;
}