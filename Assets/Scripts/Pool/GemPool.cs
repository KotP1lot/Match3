using UnityEngine;

public class GemPool : PoolBase<Gem>
{
    public GemPool(Gem gemPrefab, GridObjectSO gridObjectSO, int preloadCount) :
        base(() => Preload(gemPrefab, gridObjectSO), GetAction, ReturnAction, preloadCount)
    {    }
    public static Gem Preload(Gem gemPrefab, GridObjectSO gridObjectSO)
    {
        Gem gridObject = Object.Instantiate(gemPrefab);
        gridObject.Setup(gridObjectSO);
        return gridObject;
    }
    public static void GetAction(Gem gridObject) 
    {
        gridObject.gameObject.SetActive(true);
        gridObject.OnGemDestroy += ReturnAction;
    }
    public static void ReturnAction(Gem gridObject) 
    {
        gridObject.gameObject.SetActive(false);
    }
}
