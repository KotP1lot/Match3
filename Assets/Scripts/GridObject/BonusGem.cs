using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusGem : Gem
{
    [SerializeField] GemType type;
    protected List<GridCell> cells;
    [SerializeField] protected  GridCell cell;
    [SerializeField] protected int countToCharge;
    [SerializeField] protected  int curentChargeState;
    public Action OnBonusDestroy;
    public bool isCharged = false;

    override public Tween SetGridCoord(Vector2Int gridCoord, GridCell gridCell)
    {
        cell = gridCell;
        return base.SetGridCoord(gridCoord, gridCell);
    }
    public void Setup()
    {
        GetComponent<Image>().sprite = Info.Sprite;
    }
    public void Charge() 
    {
        if (!isCharged)
        {
            curentChargeState++;
            if (curentChargeState >= countToCharge)
            {
                EventManager.instance.OnBonusActivated(this);
                curentChargeState = 0;
                isCharged = true;
            }
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        isCharged = false;
        OnBonusDestroy?.Invoke();
    }
}
