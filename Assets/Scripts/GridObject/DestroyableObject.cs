using System;
using UnityEngine;

public class DestroyableObject : GridObject
{
    public Action<DestroyableObject> OnDestroyableObjectDestroyed;
    private GridCell cell;
    public int Health;
    public bool ExistAfterDestroy = false;
    [SerializeField] GridObjectSO objectSO;
    public void Start()
    {
        Setup(objectSO);
    }
    override public void SetGridCoord(Vector2Int gridCoord, GridCell gridCell)
    {
        base.SetGridCoord(gridCoord, gridCell);
        cell = gridCell;
        Subcribe();
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
