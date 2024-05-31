using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FastManager
{ 
    public readonly Dictionary<GemType, Fastidiousness> fastidiousnesses = new();
    public void Setup(CustomerType customerType)
    {
        GemType[] types = GetRandomGemTypes(3);
        if (customerType.isFine)
            fastidiousnesses.Add(types[0], new Fastidiousness() { bonusPercent = 20, type = FastidiousnessType.fine });
        if (customerType.isMeh)
            fastidiousnesses.Add(types[1], new Fastidiousness() { bonusPercent = 20, type = FastidiousnessType.meh });
        if(customerType.isBad)
            fastidiousnesses.Add(types[2], new Fastidiousness() { bonusPercent = 20, type = FastidiousnessType.bad });
    }
    public int SatWithFast(GemType type, int value) 
    {
        if (fastidiousnesses.TryGetValue(type, out Fastidiousness fastidiousness))
        {
            return fastidiousness.Bonus(value);
        }
        return value;
    }
    private GemType GetRandomGemType()
    {
        return (GemType)Random.Range(0, 5);
    }
    private GemType[] GetRandomGemTypes(int count)
    {
        HashSet<GemType> result = new();

        while (result.Count < count)
        {
            result.Add(GetRandomGemType());
        }

        return result.ToArray();
    }
}
