using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_FloorSO : ScriptableObject
{
    [SerializeField] FloorSO[] floors;
    public FloorSO GetByType(FloorType type)
    {
        foreach (FloorSO flor in floors)
        {
            if (flor.type == type)
                return flor;
        }
        return null;
    }
}
