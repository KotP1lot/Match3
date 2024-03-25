using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridCell: MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public Action OnGemDestroyinCell;
    private Grid grid;
    private GridManager gridManager;
    public int X { get; private set; }
    public int Y { get; private set; }
  [field:SerializeField]  public GridObject GridObject { get; private set; }
    public Gem Gem { get; private set; }
    [SerializeField] bool isHasGem;
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
        Gem gem = GridObject as Gem;
        if (gem != null)
        {
            isHasGem = true;
            Gem = gem;
        }
        else 
        {
            isHasGem = false;
        }
        gridObject.SetGridCoord(new Vector2Int(X, Y), this);
    }
    public void SetGemActive(bool isActive) 
    {
        if (IsHasGem())
        {
            Gem.SetActive(isActive);
        }
    }
    public void DestroyGridObject()
    {
        if (GridObject == null) return;

        if (IsHasGem())
        {
            OnGemDestroyinCell?.Invoke();
        }
        GridObject.Destroy();
        ClearCell();
    }
    public void ClearCell() 
    {
        GridObject = null;
        Gem = null;
    }
    public bool IsHasGem() => Gem != null;
    public bool IsEmpty() => GridObject == null;

    #region GridGetFunc
    public List<GridCell> GetNeighborCellsInRadius(int radius) 
    {
        return grid.GetNeighborCellsInRadius(this, radius);
    }
    public List<GridCell> GetNeighboringCells()
    {
        return grid.GetNeighboringCells(this);
    }
    public List<GridCell> GetRow()
    {
         return grid.GetRow(this);
    }
    public List<GridCell> GetRow(int count, bool isTwoWayCheck = true, bool isRightCheck = false)
    {
        return grid.GetRow(this, count, isTwoWayCheck, isRightCheck);
    }
    public List<GridCell> GetColumn()
    {
        return grid.GetColumn(this);
    }
    public List<GridCell> GetColumn(int count, bool isTwoWayCheck = true, bool isUpwardCheck = false)
    {
        return grid.GetColumn(this, count, isTwoWayCheck, isUpwardCheck);
    }
    #endregion

    #region PointerFunc
    public void OnPointerDown(PointerEventData eventData)
    {
        if(IsHasGem())
        gridManager.StartLine(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gridManager.IsLineActive && IsHasGem()) 
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
    #endregion
}
