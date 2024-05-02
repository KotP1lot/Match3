using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BGBox : BonusGem
{
    class CellInfo 
    {
        public GridCell cell;
        public Gem gem;
    }
    [SerializeField] int radius;
    readonly List<CellInfo> cellInfos= new();
    public override void SetActive(bool isActive)
    {
        if (isActive)
        {
            List<GridCell> cells = cell.GetNeighborCellsInRadius(radius);
            foreach (GridCell cell in cells)
            {
                if (cell.IsHasGem())
                {
                    if (cell.Gem.isActive || cell.Gem is BonusGem) continue;
                    CellInfo info = new()
                    {
                        cell = cell,
                        gem = cell.Gem
                    };

                    cellInfos.Add(info);
                    cell.Clear();
                    cell.SetGemByType(GetGemType());
                }
            }
            isActivated = true;
        }
        else 
        {
            foreach (CellInfo info in cellInfos) 
            {
                info.cell.Clear();
                info.gem.gameObject.SetActive(true);
                info.cell.SetObject(info.gem);
            }
            cellInfos.Clear();
            isActivated = false;
        }
    }
    
    public override bool Destroy() 
    {
        if(isActivated) return base.Destroy();
        return false;
    }
}
