using UnityEngine;

public class BGLineDestroyer : BonusGem
{
    [SerializeField] bool isHorizontal;
    [SerializeField] int gemCountToDestroy;
    public override bool Destroy()
    {
        isActivated = true;
        cells = isHorizontal ? cell.GetRow(gemCountToDestroy) : cell.GetColumn(gemCountToDestroy);
        foreach (GridCell cell in cells)
        {
            if (cell.IsEmpty() ||( cell.GridObject is BonusGem bonus && bonus.isActivated))
            {
                continue;
            }
            cell.DestroyGridObject();
        }
        return base.Destroy();
    }
}
