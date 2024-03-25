using System.Collections.Generic;
using UnityEngine;

public class GemPoolList
{
    private List<GridObjectType> keysList;
    private Dictionary<GridObjectType, GemPool> gemPools;
    public GemPoolList() 
    {
        gemPools = new Dictionary<GridObjectType, GemPool>();
        keysList = new List<GridObjectType>();
    }
    public void AddPool(GridObjectType type, GemPool gemPool) 
    {
        gemPools.Add(type, gemPool);
        keysList.Add(type);
    }
    public GemPool GetPoolByType(GridObjectType type) 
    {
        return gemPools[type];
    }
    public GemPool GetRandomPool()
    { 
        int randomIndex = Random.Range(0, keysList.Count);

        GridObjectType randomKey = keysList[randomIndex];
        return gemPools[randomKey];
    }

}
