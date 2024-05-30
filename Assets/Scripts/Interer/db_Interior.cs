using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[CreateAssetMenu()]
public class db_Interior : ISaveLoadSO
{
    [SerializeField] List<InteriorSO> interiorSO;
    [SerializeField]public List<PlayerInteriorData> data;
    [SerializeField] public BonusSO bonus;
    [SerializeField] public EnergySO energy;
    PlayerInteriorDataSave loadedData;
   public Action OnDataUpdate;

    public void UpgradeType(InteriorType type) 
    {
        PlayerInteriorData currData = GetPlayerDataByType(type); 
        currData.lvl++;
        switch (type) 
        {
            case InteriorType.stul:
                bonus.moneyBonus += interiorSO.Find(x => x.type == type).GetLvlInfo(currData.lvl).bonus;
                break;
            case InteriorType.stil:
                bonus.yummyBonus += interiorSO.Find(x => x.type == type).GetLvlInfo(currData.lvl).bonus;
                break;
            case InteriorType.light:
                bonus.energyBonus += interiorSO.Find(x => x.type == type).GetLvlInfo(currData.lvl).bonus;
                energy.AddMaxEnergy(bonus.energyBonus);
                break;
        }
        OnDataUpdate?.Invoke();
        Save();
    }
    public void ChangeCurrSpriteByType(InteriorType type, int curr) 
    {
        PlayerInteriorData data = GetPlayerDataByType(type);
        data.currSprite = curr;
        OnDataUpdate?.Invoke();
        Save();
    }
    public PlayerInteriorData GetPlayerDataByType(InteriorType type) 
    {
        return data.Find(x => x.type == type);
    }
    public override void Clear()
    {
        PlayerPrefs.DeleteKey("PlayerInterier");
        bonus.Clear();
    }
    public override void Setup()
    {
        if (loadedData == null)
        {
            data = new()
            {
            new(){ type = InteriorType.stul, currSprite= 0, lvl = 0 },
            new(){ type = InteriorType.stil, currSprite= 0, lvl = 0 },
            new(){ type = InteriorType.light, currSprite= 0, lvl = 0 }
            };
        }
        else
        {
            data = loadedData.data;
        }
        bonus.Clear();
        InteriorSO so = interiorSO.Find(x => x.type == InteriorType.stul);
        PlayerInteriorData playerData = GetPlayerDataByType(InteriorType.stul);
        for (int i = 0; i <= playerData.lvl; i++) 
        {
            bonus.moneyBonus += so.GetLvlInfo(i).bonus;
        }
        so = interiorSO.Find(x => x.type == InteriorType.stil);
        playerData = GetPlayerDataByType(InteriorType.stil);
        for (int i = 0; i <= playerData.lvl; i++)
        {
            bonus.yummyBonus += so.GetLvlInfo(i).bonus;
        }
        so = interiorSO.Find(x => x.type == InteriorType.light);
        playerData = GetPlayerDataByType(InteriorType.light);
        for (int i = 0; i <= playerData.lvl; i++)
        {
            bonus.energyBonus += so.GetLvlInfo(i).bonus;
        }
        OnDataUpdate?.Invoke();
    }
    public override void Load()
    {
        string strData = PlayerPrefs.GetString("PlayerInterier");
        loadedData = JsonUtility.FromJson<PlayerInteriorDataSave>(strData);
    }

    public override void Save()
    {
        string data = JsonUtility.ToJson(new PlayerInteriorDataSave() { data = this.data });
        PlayerPrefs.SetString("PlayerInterier", data);
        Debug.Log(data);
    }
}
[Serializable]
public struct InteriorInfo 
{
    public InteriorSO so;
    public PlayerInteriorData data;
}
[Serializable]
public class PlayerInteriorDataSave
{
    public List<PlayerInteriorData> data;
}
[Serializable]
public class PlayerInteriorData 
{
    public InteriorType type;
    public int lvl;
    public int currSprite;
}
