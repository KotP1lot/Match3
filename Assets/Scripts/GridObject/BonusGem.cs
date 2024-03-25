using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGem : Gem
{
    [SerializeField] GridObjectType type;
    protected List<GridCell> cells;
    [SerializeField] protected  GridCell cell;
    [SerializeField] protected int countToCharge;
    [SerializeField] protected  int curentChargeState;

    private void Start()
    {
        EventManager.instance.OnGemDestroy += Charge;
    }
    override public void SetGridCoord(Vector2Int gridCoord, GridCell gridCell)
    {
        base.SetGridCoord(gridCoord, gridCell);
        cell = gridCell;
    }
    private void Charge(Gem gem) 
    {
        if (type != gem.Info.Type) return;
        curentChargeState++;

    }


}
