using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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
        this.isActive = isActive;
        if (isActive)
        {
            List<GridCell> cells = cell.GetNeighborCellsInRadius(radius);
            cellInfos.Clear();
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
        }
        else
        {
            if (isActivated) return;
            foreach (CellInfo info in cellInfos) 
            {
                info.cell.Clear();
                info.gem.gameObject.SetActive(true);
                info.cell.SetObject(info.gem);
            }
            cellInfos.Clear();
        }
    }
    
    public override async Task Destroy(Action callback, Transform target)
    {
        if (isActive) 
        {
            isActivated = true;
            Deactivate();
            await MoveTo(target).AsyncWaitForCompletion();
            DeactivateGem();
            callback();
        }
    }

    public override void Setup(GemType gemType, int lvl = 0)
    {
        base.Setup(gemType, lvl);
        radius = lvl_bgInfo.range;
    }
}
