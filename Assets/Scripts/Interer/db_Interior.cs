using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class db_Interior : ISaveLoadSO
{
    [SerializeField]public List<InteriorSO> interiors;
    [SerializeField]public List<PlayerInteriorData> data;

    public InteriorLvlInfo GetInteriorByType(InteriorType type, int lvl) 
    {
        return interiors.Find(x => x.type == type).GetLvlInfo(lvl);
    }
    public InteriorLvlInfo GetCurrentInteriorByType(InteriorType type) 
    {
        int lvl = GetPlayerDataByType(type).lvl;
        return interiors.Find(x => x.type == type).GetLvlInfo(lvl);
    }
    public PlayerInteriorData GetPlayerDataByType(InteriorType type) 
    {
        return data.Find(x => x.type == type);
    }
    public Sprite GetCurrentFurnitureByType(InteriorType type) 
    {
        PlayerInteriorData data = GetPlayerDataByType(type);
        InteriorLvlInfo lvlInfo = GetCurrentInteriorByType(type);
        return lvlInfo.sprites[data.currSprite];
    }
    public override void Load()
    {
        foreach (InteriorSO interior in interiors) 
        {
            data.Add(new() { type = interior.type, currSprite = 0, lvl = 0 });
        }
    }

    public override void Save()
    {
        base.Save();
    }

    public override void Setup()
    {
        base.Setup();
    }
}
[Serializable]
public struct PlayerInteriorData 
{
    public InteriorType type;
    public int lvl;
    public int currSprite;
}
