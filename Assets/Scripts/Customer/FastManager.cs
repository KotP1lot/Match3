using System.Collections.Generic;
using UnityEngine;

public class FastManager
{ 
    private readonly Dictionary<GemType, Fastidiousness> fastidiousnesses = new();
    public void Setup(CustomerType customerType) 
    {
        if(customerType.isFine)
            fastidiousnesses.Add(GetRandomGemType(), new Fastidiousness() { type = FastidiousnessType.fine });
        if (customerType.isMeh)
            fastidiousnesses.Add(GetRandomGemType(), new Fastidiousness() { type = FastidiousnessType.meh });
        if(customerType.isBad)
            fastidiousnesses.Add(GetRandomGemType(), new Fastidiousness() { type = FastidiousnessType.bad });      
    }
    public int SatWithFast(Gem gem) 
    {
        GemType gemType = gem.GetGemType();
        if (!fastidiousnesses.ContainsKey(gemType)) return gem.GetScore();
        return fastidiousnesses[gemType].Bonus(gem.GetScore());
    }
    public GemType GetRandomGemType() 
    {
        int random = Random.Range(0, 5);
        return random switch
        {
            0 => GemType.salad,
            1 => GemType.sweet,
            2 => GemType.drink,
            3 => GemType.meat,
            4 => GemType.fish,
            _ => GemType.salad
        };
    }
}
