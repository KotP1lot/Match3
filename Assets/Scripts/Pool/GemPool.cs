using UnityEngine;

public class GemPool : PoolBase<Gem>
{
    public GemPool(Transform gemPoolTransform, Gem gemPrefab, GridObjectSO gridObjectSO, int preloadCount) :
        base(() => Preload(gemPoolTransform, gemPrefab, gridObjectSO), GetAction, ReturnAction, preloadCount)
    {    }
    public static Gem Preload(Transform gemPoolTransform, Gem gemPrefab, GridObjectSO gridObjectSO)
    {
        Gem gridObject = Object.Instantiate(gemPrefab, gemPoolTransform);
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
