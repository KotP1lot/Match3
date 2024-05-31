using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class db_Chief : ISaveLoadSO
{
    public List<ChiefSO> all;
    public List<ChiefPlayerData> playerChief;
    private List<ChiefPlayerData> loadedData;
    public void UnlockChief(ChiefSO chief)
    {
        ChiefPlayerData chiefPlayer = playerChief.Find(x => x.chief == chief);
        if (chiefPlayer == null) return;
        if (chiefPlayer.unlocked) return;
        chiefPlayer.unlocked = true;
        chiefPlayer.lvl = 0;
        Save();
    }
    public void UpdateChiefData(ChiefSO chief, int lvl) 
    {
        if (playerChief.Find(x => x.chief == chief) == null) return;
        ChiefPlayerData data = playerChief.Find(x => x.chief == chief);
        data.lvl = lvl;
        Save();
    }
    public List<ChiefPlayerData> GetUnlockedChiefsByType(GemType gem)
    {
        return playerChief.FindAll(x => x.chief.gemType == gem && x.unlocked);
    }
    public List<ChiefPlayerData> GetChiefsByType(GemType gem)
    {
        return playerChief.FindAll(x => x.chief.gemType == gem);
    }
    private List<ChiefPlayerData> GetAllNotUnlocked(GemType gem)
    {
        return playerChief.FindAll(x => x.chief.gemType == gem && !x.unlocked);
    }
    override public void Setup()
    {
        playerChief = new();
        for (int i = 0; i < all.Count; i++)
        {
            playerChief.Add(new() { chief = all[i], lvl = 0, unlocked = false });
        }
        if (loadedData == null) return;
        foreach (var load in loadedData)
        {
            if (playerChief.Find(x => x.chief == load.chief) != null)
            {
                ChiefPlayerData data = playerChief.Find(x => x.chief == load.chief);
                data.lvl = load.lvl;
                data.unlocked = load.unlocked;
            }
        }
    }
    override public void Save()
    {
        string chiefs = JsonUtility.ToJson(new ChiefsPlayerDataSave() { chiefs = playerChief });
        PlayerPrefs.SetString("UnlockChief", chiefs);
        PlayerPrefs.Save();
    }
    override public void Load()
    {
        loadedData = null;
        string chiefs = PlayerPrefs.GetString("UnlockChief");
        ChiefsPlayerDataSave data = JsonUtility.FromJson<ChiefsPlayerDataSave>(chiefs);
        if (data == null) return;
        loadedData = data.chiefs;
    }
    public override void SaveGameStat(GameStat stat)
    {
        UnlockChief(stat.lvl.unlockChief);
        base.SaveGameStat(stat);
    }
    public override void Clear()
    {
        loadedData = null;
        playerChief = new();
        PlayerPrefs.DeleteKey("UnlockChief");
    }
}
public class ChiefsPlayerDataSave
{
    public List<ChiefPlayerData> chiefs;
}
[Serializable]
public class ChiefPlayerData
{
    public ChiefSO chief;
    public int lvl;
    public bool unlocked;
}
