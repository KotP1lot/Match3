using System.Collections.Generic;
using UnityEngine;

public class GemPoolList
{
    private List<GemType> keysList;
    private Dictionary<GemType, GemPool> gemPools;
    public GemPoolList() 
    {
        gemPools = new Dictionary<GemType, GemPool>();
        keysList = new List<GemType>();
    }
    public void AddPool(GemType type, GemPool gemPool) 
    {
        gemPools.Add(type, gemPool);
        keysList.Add(type);
    }
    public GemPool GetPoolByType(GemType type) 
    {
        return gemPools[type];
    }
    public GemPool GetRandomPool()
    { 
        int randomIndex = Random.Range(0, keysList.Count);

        GemType randomKey = keysList[randomIndex];
        return gemPools[randomKey];
    }
}
