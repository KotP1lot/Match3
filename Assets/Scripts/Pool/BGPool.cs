using UnityEngine;

public class BGPool : PoolBase<BonusGem>
{
    public BGPool(Transform pos, BonusGem prefab, GemType type, int preloadCount) : 
        base(()=>Preload(pos, prefab, type), (bg) => GetAction(bg, pos), (bg) => ReturnAction(bg, pos), preloadCount)
    {}
    public static BonusGem Preload(Transform pos, BonusGem prefab, GemType type)
    {
        BonusGem bg = Object.Instantiate(prefab, pos);
        bg.Setup(type);
        return bg;
    }
    public static void GetAction(BonusGem bg, Transform pos)
    {
        bg.gameObject.SetActive(true);
        bg.OnGemDeactivate += (bg) => ReturnAction(bg, pos);
    }
    public static void ReturnAction(Gem bg, Transform pos)
    {
        bg.gameObject.SetActive(false);
        bg.transform.SetParent(pos);
    }
}
