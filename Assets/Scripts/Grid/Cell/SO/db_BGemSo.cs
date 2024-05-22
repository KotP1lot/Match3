using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class db_BGemSo : ScriptableObject
{
    public List<BGSO> bonusGems;

    public BGSO GetBGByType(BGType bGType) 
    {
        return bonusGems.Find(x => x.type == bGType);
    }
}
