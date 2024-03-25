using UnityEngine;

public class BGLineDestroyer : BonusGem
{
    [SerializeField] bool isHorizontal;
    [SerializeField] int gemCountToDestroy;

    public override void Destroy()
    {
        cells = isHorizontal ? cell.GetRow(3) : cell.GetColumn(3);
        foreach (GridCell cell in cells)
        {
            cell.DestroyGridObject();
        }
        base.Destroy();
    }
}
