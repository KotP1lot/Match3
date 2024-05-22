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
    PlayerInteriorDataSave loadedData;
   public Action OnDataUpdate;

    public void UpgradeType(InteriorType type) 
    {
        PlayerInteriorData data = GetPlayerDataByType(type);
        data.lvl++;
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
        bonus.moneyBonus = interiorSO.Find(x=>x.type == InteriorType.stul).GetLvlInfo(data.Find(x=>x.type == InteriorType.stul).lvl).bonus;
        bonus.yummyBonus = interiorSO.Find(x=>x.type == InteriorType.stil).GetLvlInfo(data.Find(x=>x.type == InteriorType.stil).lvl).bonus;
        bonus.energyBonus = interiorSO.Find(x=>x.type == InteriorType.light).GetLvlInfo(data.Find(x=>x.type == InteriorType.light).lvl).bonus;
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
