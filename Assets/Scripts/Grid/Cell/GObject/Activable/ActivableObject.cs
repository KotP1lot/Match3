using DG.Tweening;
using System;
using UnityEngine;

public class ActivableObject : GridObject
{
    public Action<ActivableObject> OnActivated;
    protected GridCell cell;
    public int CountToActivate;
    protected int count;
    [SerializeField]protected int predictCount;
    protected bool isActive = false;
    protected bool isPredicted = false;
    override public Tween SetGridCoord(GridCell gridCell)
    {
        cell = gridCell;
        Subcribe();
        return base.SetGridCoord(gridCell);

    }
    private void Subcribe()
    {
        foreach (GridCell cell in cell.GetNeighboringCells())
        {
            cell.OnGemDestroyinCell += OnGemsDestroyInNeighboringCells;
        }
    }
    private void Unsubcribe()
    {
        foreach (GridCell cell in cell.GetNeighboringCells())
        {
            cell.OnGemDestroyinCell -= OnGemsDestroyInNeighboringCells;
        }
    }
    virtual public void OnGemsDestroyInNeighboringCells() { }
    virtual public void PredictionActivate() { }
    virtual public void PredictionDeactivate() { }
    virtual public void Activate() { }
}
