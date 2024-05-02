using System;
using System.Collections.Generic;
using UnityEngine;

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
            Direction dir = LookAt(cell);
            GridCell neibourActive;
            GridCell neibourNext;
            switch (dir) 
            {
                case Direction.Top:
                    if(activeGC.IsBorderExist(dir) || cell.IsBorderExist(Direction.Bottom))
                        return;
                    break;
                case Direction.Bottom:
                    if (activeGC.IsBorderExist(dir) || cell.IsBorderExist(Direction.Top))
                        return;
                    break;
                case Direction.Left:
                    if (activeGC.IsBorderExist(dir) || cell.IsBorderExist(Direction.Right))
                        return;
                    break;
                case Direction.Right:
                    if (activeGC.IsBorderExist(dir) || cell.IsBorderExist(Direction.Left))
                        return;
                    break;
                case Direction.TopLeft:
                    neibourActive = activeGC.grid.GetCell(Math.Clamp(activeGC.x-1,0,6), activeGC.y);
                    neibourNext = activeGC.grid.GetCell(activeGC.x, Math.Clamp(activeGC.y + 1, 0, 6));
                    if ((activeGC.IsBorderExist(Direction.Top) && activeGC.IsBorderExist(Direction.Left)) 
                        || ((cell.IsBorderExist(Direction.Bottom) && cell.IsBorderExist(Direction.Right))
                        || (activeGC.IsBorderExist(Direction.Top) && neibourActive.IsBorderExist(Direction.Top))
                        || (cell.IsBorderExist(Direction.Bottom) && neibourNext.IsBorderExist(Direction.Bottom))
                        ))
                        return;
                    break;
                case Direction.TopRight:
                    neibourActive = activeGC.grid.GetCell(Math.Clamp(activeGC.x + 1, 0, 6), activeGC.y);
                    neibourNext = activeGC.grid.GetCell(activeGC.x, Math.Clamp(activeGC.y + 1, 0, 6));
                    if ((activeGC.IsBorderExist(Direction.Top) && activeGC.IsBorderExist(Direction.Right)) 
                        || ((cell.IsBorderExist(Direction.Bottom) && cell.IsBorderExist(Direction.Left))
                          || (activeGC.IsBorderExist(Direction.Top) && neibourActive.IsBorderExist(Direction.Top))
                        || (cell.IsBorderExist(Direction.Bottom) && neibourNext.IsBorderExist(Direction.Bottom))
                        ))
                        return;
                    break;
                case Direction.BottomLeft:
                    neibourActive = activeGC.grid.GetCell(Math.Clamp(activeGC.x - 1, 0, 6), activeGC.y);
                    neibourNext = activeGC.grid.GetCell(activeGC.x, Math.Clamp(activeGC.y - 1, 0, 6));
                    if ((activeGC.IsBorderExist(Direction.Bottom) && activeGC.IsBorderExist(Direction.Left)) 
                        || ((cell.IsBorderExist(Direction.Top) && cell.IsBorderExist(Direction.Right))
                           || (activeGC.IsBorderExist(Direction.Bottom) && neibourActive.IsBorderExist(Direction.Bottom))
                        || (cell.IsBorderExist(Direction.Top) && neibourNext.IsBorderExist(Direction.Top))
                        ))
                        return;
                    break;
                case Direction.BottomRight:
                    neibourActive = activeGC.grid.GetCell(Math.Clamp(activeGC.x + 1, 0, 6), activeGC.y);
                    neibourNext = activeGC.grid.GetCell(activeGC.x, Math.Clamp(activeGC.y - 1, 0, 6));
                    if ((activeGC.IsBorderExist(Direction.Bottom) && activeGC.IsBorderExist(Direction.Right)) 
                        || ((cell.IsBorderExist(Direction.Top) && cell.IsBorderExist(Direction.Left))
                         || (activeGC.IsBorderExist(Direction.Bottom) && neibourActive.IsBorderExist(Direction.Bottom))
                        || (cell.IsBorderExist(Direction.Top) && neibourNext.IsBorderExist(Direction.Top))
                        ))
                        return;
                    break;
            }
            AddActiveGridCell(cell);
        }
        return;
    }
    public void DeactivateCells()
    {
        if (gridCells.Count >= 3)
        {
            foreach (GridCell cell in gridCells)
            {
                cell.DestroyGridObject();
            }
            EventManager.instance.OnMaxComboChanged?.Invoke(gridCells.Count);
            CheckForActiveObject();
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
        if (activeGC is not null && activeGC.Gem is BonusGem bonus && bonus.IsMoveWithLine())
        {
            Debug.Log("Add new BG");
            BGroad.Push(activeGC);
            SwapBonusGem(activeGC, cell);
        }
    }
    private void RemoveBGRoad()
    { 
        if (BGroad.Count > 0)
        {
            Debug.Log("Re-Swap");
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
            if (gridCells[gridCells.Count - 2] == cell)
            {
                RemoveBGRoad();
                GridCell gridCell = gridCells[gridCells.Count - 1];
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
        if (count > 3)
        {

            UIDebug.Instance.Show($"COMBO", $" x{count}!");
        }
        else 
        {
            UIDebug.Instance.Hide("COMBO");
        }
    }
    private void OnObjectsActivatedHendler(ActivableObject activableObject)
    {
        if (!activableObjects.Contains(activableObject))
            activableObjects.Enqueue(activableObject);
    }
    private void CheckForActiveObject() 
    {
        if (activableObjects.Count == 0) return;
        activableObjects.Dequeue().Activate();
        CheckForActiveObject();
    }
    private Direction LookAt(GridCell target)
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
