using DG.Tweening;
using System;
using UnityEngine;

public class DestroyableObject : GridObject
{
    public Action<DestroyableObject> OnDestroyableObjectDestroyed;
    private GridCell cell;
    public int Health;
    public bool ExistAfterDestroy = false;
    override public Tween SetGridCoord(Vector2Int gridCoord, GridCell gridCell)
    {
        cell = gridCell;
        Subcribe();
        return base.SetGridCoord(gridCoord, gridCell);
    }
    private void Subcribe() 
    {
        foreach(GridCell cell in cell.GetNeighboringCells()) 
        {
            cell.OnGemDestroyinCell += OnGemsDestroyedInNeighboringCells;
        }
    }
    private void Unsubcribe() 
    {
        foreach (GridCell cell in cell.GetNeighboringCells())
        {
            cell.OnGemDestroyinCell -= OnGemsDestroyedInNeighboringCells;
        }
    }
    private void OnGemsDestroyedInNeighboringCells() 
    {
        Health--;
        if (Health <= 0)
        {
            if (!ExistAfterDestroy)
            {
                Unsubcribe();
                cell.DestroyGridObject();
            }
            OnDestroyableObjectDestroyed?.Invoke(this);
        }
    }
}
