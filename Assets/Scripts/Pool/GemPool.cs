using UnityEngine;

public class GemPool : PoolBase<GridObject>
{
    public GemPool( GridObject gemPrefab, GridObjectSO gridObjectSO, int preloadCount) :
        base(() => Preload(gemPrefab, gridObjectSO), GetAction, ReturnAction, preloadCount)
    {    }
    public static GridObject Preload( GridObject gemPrefab, GridObjectSO gridObjectSO)
    {
        GridObject gridObject = Object.Instantiate(gemPrefab);
        gridObject.Setup(gridObjectSO);
        return gridObject;
    }
    public static void GetAction(GridObject gridObject) 
    {
        gridObject.gameObject.SetActive(true);
        gridObject.OnDestroy += ReturnAction;
    }
    public static void ReturnAction(GridObject gridObject) 
    {
        gridObject.gameObject.SetActive(false);
    }
}
