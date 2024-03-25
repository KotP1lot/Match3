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
    [SerializeField] protected GridObjectSO objectSO; 
    override public void SetGridCoord(Vector2Int gridCoord, GridCell gridCell)
    {
        base.SetGridCoord(gridCoord, gridCell);
        cell = gridCell;
        Subcribe();
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
