using System;
using System.Collections.Generic;
using UnityEngine;

public class GemMatchLine
{
    private GridObjectType firstMatchType;
    private List<GridCell> activeGridCells;
    private GridCell activeGridCell;
    public Action OnGridChanged;
    private Queue<ActivableObject> activableObjects;
    public GemMatchLine() 
    {
        EventManager.instance.ObjectsActivatedEvent += OnObjectsActivatedHendler;
    }
    public void NewGemMatch3Line(GridCell cell)
    {
        activeGridCells = new List<GridCell>();
        activableObjects = new Queue<ActivableObject>();
        firstMatchType = cell.GridObject.Info.Type;
        AddActiveGridCell(cell);
    }
    public bool AddMatch3Gem(GridCell cell) 
    {
        if (cell.GridObject.Info.Type != firstMatchType) return false;
        if (activeGridCells.Contains(cell))
        {
            if (activeGridCells.Count > 1)
            {
                if (activeGridCells[activeGridCells.Count - 2] == cell)
                {
                    GridCell gridCell = activeGridCells[activeGridCells.Count - 1];
                    gridCell.SetGemActive(false);
                    activeGridCells.RemoveAt(activeGridCells.Count - 1);
                    activeGridCell = cell;
                }
            }
            return false;
        }
        if (Mathf.Abs(activeGridCell.X - cell.X) <= 1 && Mathf.Abs(activeGridCell.Y - cell.Y) <= 1)
        {
                AddActiveGridCell(cell);
        }
        return false;
    }
    public void DeactivateCells()
    {
        if (activeGridCells.Count >= 3)
        {
            foreach (GridCell cell in activeGridCells)
            {
                cell.DestroyGridObject();
            }
            CheckForActiveObject();
            OnGridChanged?.Invoke();
        }
        else
        {
            foreach (GridCell cell in activeGridCells)
            {

                cell.SetGemActive(false);
            }
        }
        activeGridCells.Clear();
    }
    private void AddActiveGridCell(GridCell cell)
    {
        activeGridCells.Add(cell);
        activeGridCell = cell;
        cell.SetGemActive(true);
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

}
