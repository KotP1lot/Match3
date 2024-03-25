using UnityEngine;

public class BGBomb : BonusGem
{
    [SerializeField] int radius;
    public override void Destroy()
    {
        cells = cell.GetNeighborCellsInRadius(radius);
        foreach (GridCell cell in cells)
        {
            cell.DestroyGridObject();
        }
        base.Destroy();
    }
}
