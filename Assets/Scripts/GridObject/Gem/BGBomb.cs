using UnityEngine;

public class BGBomb : BonusGem
{
    [SerializeField] int radius;
    public override bool Destroy()
    {
 
        cells = cell.GetNeighborCellsInRadius(radius);
        foreach (GridCell cell in cells)
        {
            cell.DestroyGridObject();
        }
        return base.Destroy();
    }
}
