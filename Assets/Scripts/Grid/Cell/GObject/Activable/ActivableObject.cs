using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class ActivableObject : GridObject
{
    protected GridCell cell;
    public int CountToActivate;
    protected int count;
    protected bool isReady;
    protected bool isPredicted = false;
    override public Tween SetGridCoord(GridCell gridCell)
    {
        cell = gridCell;
        Setup();
        Subcribe();
        return base.SetGridCoord(gridCell);

    }
    protected void Subcribe()
    {
        foreach (GridCell cell in cell.GetNeighboringCells())
        {
            cell.OnGemDestroyinCell += OnGemsDestroyInNeighboringCells;
        }
        EventManager.instance.OnTurnEnded += Deactivate;
    }
    protected void Unsubcribe()
    {
        foreach (GridCell cell in cell.GetNeighboringCells())
        {
            cell.OnGemDestroyinCell -= OnGemsDestroyInNeighboringCells;
        }
        EventManager.instance.OnTurnEnded -= Deactivate;
    }
    virtual public void Setup() { }
    virtual public void OnGemsDestroyInNeighboringCells() { }
    virtual public void PredictionActivate() { }
    virtual public void Deactivate() { isReady = true; }
    virtual public async Task Activate() { }
}
