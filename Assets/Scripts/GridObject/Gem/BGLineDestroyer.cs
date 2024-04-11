using UnityEngine;

public class BGLineDestroyer : BonusGem
{
    [SerializeField] bool isHorizontal;
    [SerializeField] int gemCountToDestroy;

    public override bool Destroy()
    {
        cells = isHorizontal ? cell.GetRow(gemCountToDestroy) : cell.GetColumn(gemCountToDestroy);
        foreach (GridCell cell in cells)
        {
            cell.DestroyGridObject();
        }
        return base.Destroy();
    }
}
