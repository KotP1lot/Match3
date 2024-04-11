using UnityEngine;

public class BGBomb : BonusGem
{
    [SerializeField] int radius;
    public override bool Destroy()
    {
        isActivated = true;
        cells = cell.GetNeighborCellsInRadius(radius);
        foreach (GridCell cell in cells)
        {
            if (cell.IsEmpty() || (cell.GridObject is BonusGem bonus && bonus.isActivated))
            {
                continue;
            }
            cell.DestroyGridObject();
        }
        return base.Destroy();
    }
}
