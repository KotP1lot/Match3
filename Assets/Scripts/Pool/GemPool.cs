using UnityEngine;

public class GemPool : PoolBase<Gem>
{
    public GemPool(Transform pos, Gem prefab, GemSO so, int preloadCount) :
        base(() => Preload(pos, prefab, so), (gem) => GetAction(gem, pos), (gem) => ReturnAction(gem, pos), preloadCount)
    { }
    public static Gem Preload(Transform pos, Gem prefab, GemSO so)
    {
        Gem gridObject = Object.Instantiate(prefab, pos);
        gridObject.Setup(so);
        return gridObject;
    }
    public static void GetAction(Gem gem, Transform pos)
    {
        gem.gameObject.SetActive(true);
        gem.OnGemDeactivate += (gem) => ReturnAction(gem, pos);
    }
    public static void ReturnAction(Gem gem, Transform pos)
    {
        gem.gameObject.SetActive(false);
        gem.transform.SetParent(pos);
    }
}
