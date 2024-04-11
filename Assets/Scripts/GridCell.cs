using DG.Tweening;
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
    public bool isHasGem;
    public enum Direction
    {
        None,
        Top,
        Bottom,
        Left,
        Right,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
    public Direction LookAt(GridCell target)
    {
        if (target.X == X && target.Y == Y)
        {
            return Direction.None;
        }
        else if (target.X == X)
        {
            return target.Y > Y ? Direction.Top : Direction.Bottom;
        }
        else if (target.Y == Y)
        {
            return target.X > X ? Direction.Right : Direction.Left;
        }
        else
        {
            return target.Y > Y ? (target.X > X ? Direction.TopRight : Direction.TopLeft) : (target.X > X ? Direction.BottomRight : Direction.BottomLeft);
        }
    }
    public void Setup(GridManager gridManager, Grid grid, int x, int y)
    {
        this.gridManager = gridManager;
        this.grid = grid;
        X = x;
        Y = y;
        GridObject = null;
    }
    private void SetGridObject(GridObject gridObject)
    {
        GridObject = gridObject;
        Gem gem = GridObject as Gem;
        isHasGem = gem != null;
        if (isHasGem)
        {
            Gem = gem;
        }
    }
    public Tween SpawnObject(Transform spawnPoint, GridObject gridObject)
    {
        SetGridObject(gridObject);
        return gridObject.Spawn(spawnPoint, new Vector2Int(X, Y), this);
    }

    public Tween SetObject(GridObject gridObject)
    {
        SetGridObject(gridObject);
        return gridObject.SetGridCoord(new Vector2Int(X, Y), this);
    }
    public void SetGemActive(bool isActive) 
    {
        if (IsHasGem())
        {
            Gem.SetActive(isActive);
        }
    }
    public void SetGemArrow(bool isActive, GridCell cell)
    {
        if (IsHasGem())
        {
            float z = 0;
            if (isActive)
            {
                Direction direction = LookAt(cell);
               z = direction switch
                {
                    Direction.None => 0f, // Немає обертання, оскільки ціль розташована на тій самій клітині
                    Direction.Top => 90f, // Вгору
                    Direction.Bottom => -90f, // Вниз
                    Direction.Left => 180f, // Вліво
                    Direction.Right => 0f, // Вправо
                    Direction.TopLeft => 135f, // Діагональ вгору-ліворуч
                    Direction.TopRight => 45f, // Діагональ вгору-праворуч
                    Direction.BottomLeft => -135f, // Діагональ вниз-ліворуч
                    Direction.BottomRight => -45f, // Діагональ вниз-праворуч
                    _ => 0f
                };
            }
            Gem.SetArrowDir(isActive, z);
        }
    }
    public void DestroyGridObject()
    {
        if (GridObject == null) return;

        if (IsHasGem())
        {
            OnGemDestroyinCell?.Invoke();
        }
        if (GridObject.Destroy())
        {
            SetEmpty();
        }
    }
    public void SetEmpty()
    { 
        GridObject = null;
        Gem = null;
    }
    public void Clear() 
    {
        if (GridObject == null) return;
        GridObject.Clear();
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
