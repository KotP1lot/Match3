using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridCell: MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public Action<GridCell> OnCellCleaned;
    private Grid grid;
    private GridManager gridManager;
    public int X { get; private set; }
    public int Y { get; private set; }

    public GridObject GridObject { get; private set; }
    public GridCell(Grid grid, int x, int y)
    {
        this.grid = grid;
        X = x;
        Y = y;
        GridObject = null;
    }
    public void Setup(GridManager gridManager, Grid grid, int x, int y)
    {
        this.gridManager = gridManager;
        this.grid = grid;
        X = x;
        Y = y;
        GridObject = null;
    }
    public void SetObject(GridObject gridObject)
    {
        GridObject = gridObject;
        gridObject.SetGridCoord(new Vector2Int(X, Y), this);
    }
    public void DestroyGridObject() 
    {
        GridObject.Destroy();
        ClearCell();
    }
    public void ClearCell() 
    {
        OnCellCleaned?.Invoke(this);
        GridObject = null;
    }
    public bool IsEmpty() => GridObject == null;

    public void OnPointerDown(PointerEventData eventData)
    {
        gridManager.StartLine(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gridManager.IsLineActive) 
        {
            gridManager.AddActiveGridCell(this);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (gridManager.IsLineActive)
        {
            gridManager.TryToDestroyLine();
        }
    }


}
