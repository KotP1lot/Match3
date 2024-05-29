using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GemMatchLine
{
    private GemType firstMatchType;
    private List<GridCell> gridCells;
    private GridCell activeGC;
    private Stack<GridCell> BGroad;
    public Action OnLineDestroy;
    private Queue<ActivableObject> activableObjects;
    public GemMatchLine() 
    {
        EventManager.instance.ObjectsActivated += OnObjectsActivatedHendler;
    }
    public void NewGemMatch3Line(GridCell cell)
    {
        BGroad = new();
        gridCells = new List<GridCell>();
        activableObjects = new Queue<ActivableObject>();
        firstMatchType = cell.Gem.GetGemType();
        AddActiveGridCell(cell);
    }
    public void AddMatch3Gem(GridCell cell) 
    {
        if (cell.Gem.GetGemType() != firstMatchType) return;
        if (gridCells.Contains(cell))
        {
            RemoveActiveGridCell(cell);
            return;
        }
        if (Mathf.Abs(activeGC.x - cell.x) <= 1 && Mathf.Abs(activeGC.y - cell.y) <= 1)
        {
           if(CanMatchLine(cell, activeGC))
                AddActiveGridCell(cell);
        }
        return;
    }
    public bool CanMatchLineOld(GridCell cell, GridCell activeGC) 
    {
        Direction dir = LookAt(cell, activeGC);
        GridCell neibourActive;
        GridCell neibourNext;
        switch (dir)
        {
            case Direction.Top:
                if (activeGC.IsBorderExist(dir) || cell.IsBorderExist(Direction.Bottom))
                    return false;
                break;
            case Direction.Bottom:
                if (activeGC.IsBorderExist(dir) || cell.IsBorderExist(Direction.Top))
                    return false;
                break;
            case Direction.Left:
                if (activeGC.IsBorderExist(dir) || cell.IsBorderExist(Direction.Right))
                    return false;
                break;
            case Direction.Right:
                if (activeGC.IsBorderExist(dir) || cell.IsBorderExist(Direction.Left))
                    return false;
                break;
            case Direction.TopLeft:
                neibourActive = activeGC.grid.GetCell(Math.Clamp(activeGC.x - 1, 0, 6), activeGC.y);
                neibourNext = activeGC.grid.GetCell(activeGC.x, Math.Clamp(activeGC.y + 1, 0, 6));
                if ((activeGC.IsBorderExist(Direction.Top) && activeGC.IsBorderExist(Direction.Left))
                    || ((cell.IsBorderExist(Direction.Bottom) && cell.IsBorderExist(Direction.Right))
                    || (activeGC.IsBorderExist(Direction.Top) && neibourActive.IsBorderExist(Direction.Top))
                    || (cell.IsBorderExist(Direction.Bottom) && neibourNext.IsBorderExist(Direction.Bottom))
                    ))
                    return false;
                break;
            case Direction.TopRight:
                neibourActive = activeGC.grid.GetCell(Math.Clamp(activeGC.x + 1, 0, 6), activeGC.y);
                neibourNext = activeGC.grid.GetCell(activeGC.x, Math.Clamp(activeGC.y + 1, 0, 6));
                if ((activeGC.IsBorderExist(Direction.Top) && activeGC.IsBorderExist(Direction.Right))
                    || ((cell.IsBorderExist(Direction.Bottom) && cell.IsBorderExist(Direction.Left))
                      || (activeGC.IsBorderExist(Direction.Top) && neibourActive.IsBorderExist(Direction.Top))
                    || (cell.IsBorderExist(Direction.Bottom) && neibourNext.IsBorderExist(Direction.Bottom))
                    ))
                    return false;
                break;
            case Direction.BottomLeft:
                neibourActive = activeGC.grid.GetCell(Math.Clamp(activeGC.x - 1, 0, 6), activeGC.y);
                neibourNext = activeGC.grid.GetCell(activeGC.x, Math.Clamp(activeGC.y - 1, 0, 6));
                if ((activeGC.IsBorderExist(Direction.Bottom) && activeGC.IsBorderExist(Direction.Left))
                    || ((cell.IsBorderExist(Direction.Top) && cell.IsBorderExist(Direction.Right))
                       || (activeGC.IsBorderExist(Direction.Bottom) && neibourActive.IsBorderExist(Direction.Bottom))
                    || (cell.IsBorderExist(Direction.Top) && neibourNext.IsBorderExist(Direction.Top))
                    ))
                    return false;
                break;
            case Direction.BottomRight:
                neibourActive = activeGC.grid.GetCell(Math.Clamp(activeGC.x + 1, 0, 6), activeGC.y);
                neibourNext = activeGC.grid.GetCell(activeGC.x, Math.Clamp(activeGC.y - 1, 0, 6));

                if ((activeGC.IsBorderExist(Direction.Bottom) && activeGC.IsBorderExist(Direction.Right))
                    || ((cell.IsBorderExist(Direction.Top) && cell.IsBorderExist(Direction.Left))
                     || (activeGC.IsBorderExist(Direction.Bottom) && neibourActive.IsBorderExist(Direction.Bottom))
                    || (cell.IsBorderExist(Direction.Top) && neibourNext.IsBorderExist(Direction.Top))
                    ))
                    return false;
                break;
        }
        return true;
    }
    public bool CanMatchLine(GridCell next, GridCell current)
    {
        Direction dir = LookAt(next, current);
        GridCell neibourActive;
        GridCell neibourNext;
        if (dir == Direction.Top)
        {
            if (current.IsBorderExist(dir) || next.IsBorderExist(Direction.Bottom))
                return false;
        }
        if (dir == Direction.Bottom)
        {
            if (current.IsBorderExist(dir) || next.IsBorderExist(Direction.Top))
                return false;
        }
        if (dir == Direction.Left)
        {
            if (current.IsBorderExist(dir) || next.IsBorderExist(Direction.Right))
                return false;
        }
        if (dir == Direction.Right)
        {
            if (current.IsBorderExist(dir) || next.IsBorderExist(Direction.Left))
                return false;
        }
        if (dir == Direction.BottomLeft)
        {
            neibourActive = current.grid.GetCell(Math.Clamp(current.x - 1, 0, 6), current.y);
            neibourNext = current.grid.GetCell(current.x, Math.Clamp(current.y - 1, 0, 6));
            return BottomCheck(dir, current, neibourNext, neibourActive, next);
        }
        if (dir == Direction.BottomRight) 
        {
            neibourActive = current.grid.GetCell(Math.Clamp(current.x + 1, 0, 6), current.y);
            neibourNext = current.grid.GetCell(current.x, Math.Clamp(current.y - 1, 0, 6));
            return BottomCheck(dir, current, neibourNext, neibourActive, next);

        }
        if (dir == Direction.TopLeft) 
        {
            neibourActive = current.grid.GetCell(Math.Clamp(current.x - 1, 0, 6), current.y);
            neibourNext = current.grid.GetCell(current.x, Math.Clamp(current.y + 1, 0, 6));
            return TopCheck(dir, current, neibourNext, neibourActive, next);
        
        }
        if (dir == Direction.TopRight)
        {
            neibourActive = current.grid.GetCell(Math.Clamp(current.x + 1, 0, 6), current.y);
            neibourNext = current.grid.GetCell(current.x, Math.Clamp(current.y - 1, 0, 6));
            return TopCheck(dir, current, neibourNext, neibourActive, next);
        }
        return true;
    }
    private bool BottomCheck(Direction dir, GridCell current, GridCell under, GridCell neighbor, GridCell next)
    {
        if ((current.IsBorderExist(Direction.Bottom) && neighbor.IsBorderExist(Direction.Bottom)) || (under.IsBorderExist(Direction.Top) && next.IsBorderExist(Direction.Top)))
            return false;
        if ((current.IsBorderExist(Direction.Bottom) && !neighbor.IsEmpty() && neighbor.GridObject is Wall) || (next.IsBorderExist(Direction.Top) && !under.IsEmpty() && under.GridObject is Wall))
            return false;
        if (dir == Direction.BottomRight)
        {
            if ((current.IsBorderExist(Direction.Right) && current.IsBorderExist(Direction.Bottom)) || (next.IsBorderExist(Direction.Left) && next.IsBorderExist(Direction.Top)))
            {
                return false;
            }
        }
        if (dir == Direction.BottomLeft)
        {
            if ((current.IsBorderExist(Direction.Left) && current.IsBorderExist(Direction.Bottom)) || (next.IsBorderExist(Direction.Right) && next.IsBorderExist(Direction.Top)))
            {
                return false;
            }
        }
        return true;
    }
    private bool TopCheck(Direction dir, GridCell current, GridCell under, GridCell neighbor, GridCell next)
    {
        if ((current.IsBorderExist(Direction.Top) && neighbor.IsBorderExist(Direction.Top)) || (under.IsBorderExist(Direction.Bottom) && next.IsBorderExist(Direction.Bottom)))
            return false;
        if ((current.IsBorderExist(Direction.Top) && !neighbor.IsEmpty() && neighbor.GridObject is Wall) || (next.IsBorderExist(Direction.Bottom) && !under.IsEmpty() && under.GridObject is Wall))
            return false;
        if (dir == Direction.TopRight)
        {
            if ((current.IsBorderExist(Direction.Right) && current.IsBorderExist(Direction.Top)) || (next.IsBorderExist(Direction.Left) && next.IsBorderExist(Direction.Bottom)))
            {
                return false;
            }
        }
        if (dir == Direction.TopLeft)
        {
            if ((current.IsBorderExist(Direction.Left) && current.IsBorderExist(Direction.Top)) || (next.IsBorderExist(Direction.Right) && next.IsBorderExist(Direction.Bottom)))
            {
                return false;
            }
        }
        return true;
    }
    public async void DeactivateCells()
    {
        if (gridCells.Count >= 3)
        {
            List<Task> tasks = new ();
            foreach (GridCell cell in gridCells)
            {
                tasks.Add(cell.DestroyGridObject());
            }
            await Task.WhenAll(tasks);
            EventManager.instance.OnComboChanged?.Invoke(gridCells.Count);
            await CheckForActiveObject();
            OnLineDestroy?.Invoke();
        }
        else
        {
            foreach (GridCell cell in gridCells)
            {
                cell.SetGemActive(false);
                cell.SetGemArrow(false,null);
            }
            RemoveBGRoad();
            
        }
        activeGC = null;
        BGroad.Clear();
        gridCells.Clear();
        CheckComboDebug();
        
    }
 
    private void AddToBGRoad(GridCell cell) 
    {
        if (activeGC != null && activeGC.Gem is BonusGem bonus && bonus.IsMoveWithLine())
        {
            BGroad.Push(activeGC);
            SwapBonusGem(activeGC, cell);
        }
    }
    private void RemoveBGRoad()
    { 
        if (BGroad.Count > 0)
        {
            GridCell cell = BGroad.Pop();
            SwapBonusGem(activeGC, cell, true);
        }
    }
    private void SwapBonusGem(GridCell activeGC, GridCell cell, bool isReSwap = false) 
    {
        if (activeGC.Gem is BonusGem bonus && bonus.IsMoveWithLine())
        {
            Gem gem = cell.Gem;
            cell.SetEmpty();
            cell.SetObject(activeGC.Gem);
            activeGC.SetEmpty();
            activeGC.SetObject(gem);
            if (isReSwap)
            {
                activeGC.SetGemActive(false);
                activeGC.SetGemArrow(false, null);
            }
        }
    }
    private void AddActiveGridCell(GridCell cell)
    {
        gridCells.Add(cell);
        int count = gridCells.Count;
        cell.SetGemActive(true);
            AddToBGRoad(cell);
        if (count > 1) 
        {
            gridCells[count - 2].SetGemArrow(true, cell);
        }
        activeGC = cell;
        CheckComboDebug();
    }
    private void RemoveActiveGridCell(GridCell cell)
    {
        if (gridCells.Count > 1)
        {
            if (gridCells[^2] == cell)
            {
                RemoveBGRoad();
                GridCell gridCell = gridCells[^1];
                gridCell.SetGemActive(false);
                cell.SetGemArrow(false, null);
                gridCells.RemoveAt(gridCells.Count - 1);
                activeGC = cell;
            }
        }
        CheckComboDebug();
    }
    private void CheckComboDebug()
    {
        int count = gridCells.Count;
        EventManager.instance.OnComboChanged?.Invoke(count);
    }
    private void OnObjectsActivatedHendler(ActivableObject activableObject)
    {
        if (!activableObjects.Contains(activableObject))
            activableObjects.Enqueue(activableObject);
    }
    private async Task CheckForActiveObject() 
    {
        if (activableObjects.Count == 0) return;
        await activableObjects.Dequeue().Activate();
        await CheckForActiveObject();
    }
    private Direction LookAt(GridCell target, GridCell activeGC)
    {
        int x = activeGC.x;
        int y = activeGC.y;
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
}
