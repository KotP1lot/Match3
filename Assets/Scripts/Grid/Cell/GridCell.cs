using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class GridCell : MonoBehaviour
{
    public Action OnGemDestroyinCell;
    public Grid grid;
    private GridManager gridManager;
    private BorderManager borderManager;
    private FloorManager floorManager;
    [SerializeField] PointManager pointManager;
    public int x { get; private set; }
    public int y { get; private set; }
    public GridObject GridObject { get; private set; }
    public Gem Gem { get; private set; }
    public bool IsBorderExist(Direction dir)
    {
        return borderManager.IsBorderExist(dir);
    }
    public void AddBorder(Direction dir, BorderType type) 
    {
        borderManager.AddBorder(dir, type);
    }
    public void AddFloor(FloorType type)
    {
        floorManager.AddFloor(type);
    }
    public void Setup(GridManager gridManager, Grid grid, int x, int y)
    {
        this.gridManager = gridManager;
        this.grid = grid;
        this.x = x;
        this.y = y;
        GridObject = null;
        Gem = null;
        borderManager = GetComponent<BorderManager>();
        borderManager.Setup();
        floorManager = GetComponent<FloorManager>();
        pointManager.Setup(OnPointerDown, OnPointerEnter, OnPointerUp);
    }
    public void Setup(CelLLvlInfo info)
    {
        if (info.prefab != null)
        {
            if (info.prefab is Gem)
            {
                SetGemByType(info.gemType);
            }
            else
            {

                SetObject(Instantiate(info.prefab, transform));
            }
        }
        AddFloor(info.floorType);
        if (info.borders.Length > 0)
        {
            foreach (var borderInfo in info.borders)
            {
                AddBorder(borderInfo.direction, borderInfo.type);
            }
        }
    }
    public void SetGemByType(GemType type) 
    {
        Gem gem = gridManager.gemPoolList.GetPoolByType(type).Get();
        GridObject = gem;
        Gem = gem;
        Gem.SetGridCoord(this);
    }
    private void SetGridObject(GridObject gridObject)
    {
        GridObject = gridObject;
        Gem gem = GridObject as Gem;
        if (gem != null)
        {
            Gem = gem;
        }
    }
    public Tween SpawnObject(Transform spawnPoint, GridObject gridObject)
    {
        SetGridObject(gridObject);
        return gridObject.Spawn(spawnPoint, this);
    }
    public Tween SpawnNewObject(GridObject pref)
    {
        GridObject obj = Instantiate(pref, transform);
        SetGridObject(obj);
        return obj.SetGridCoord(this);
    }
    public Tween SetObject(GridObject gridObject)
    {
        SetGridObject(gridObject);
        return gridObject.SetGridCoord(this);
    }
    public void SetGemActive(bool isActive) 
    {
        if (IsHasGem())
        {
            Gem.SetActive(isActive);
        }
    }
    public Direction LookAt(GridCell target)
    {
        if (target.x == x && target.y == y)
        {
            return Direction.None;
        }
        else if (target.x == x)
        {
            return target.y > y ? Direction.Top : Direction.Bottom;
        }
        else if (target.y == y)
        {
            return target.x > x ? Direction.Right : Direction.Left;
        }
        else
        {
            return target.y > y ? (target.x > x ? Direction.TopRight : Direction.TopLeft) : (target.x > x ? Direction.BottomRight : Direction.BottomLeft);
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
    public async Task DestroyGridObject(Transform target = null)
    {
        if (GridObject == null) return;
        if (IsHasGem())
        {
            OnGemDestroyinCell?.Invoke();
        }
        await GridObject.Destroy(() => SetEmpty(), GetPoint(target));
    }
    private Transform GetPoint(Transform target)
    {
        if (target == null || Gem is BonusGem)
        {
            return gridManager.GetDestroyPoint();
        }
        return target;
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
        SetEmpty();
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
    public List<GridCell> GetAdjacentCells()
    {
        return grid.GetAdjacentCells(this);
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
    public void OnPointerDown()
    {
        if(IsHasGem())
        gridManager.StartLine(this);
    }
    public void OnPointerEnter()
    {
        if (gridManager.IsLineActive && IsHasGem()) 
        {
            gridManager.AddActiveGridCell(this);
        }
    }
    public void OnPointerUp()
    {
        if (gridManager.IsLineActive)
        {
            gridManager.TryToDestroyLine();
        }
    }
    #endregion
}
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