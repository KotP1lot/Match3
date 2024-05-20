using UnityEngine;

public class ISaveLoadSO : ScriptableObject
{
    virtual public void Save() { }
    virtual public void Load() { }
    virtual public void Setup() { }
    virtual public void SaveGameStat(GameStat stat) { }
    virtual public void Clear() { }
}
