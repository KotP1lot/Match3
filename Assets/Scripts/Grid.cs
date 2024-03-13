using System;
using UnityEngine;

public class Grid
{
    public Action OnGridChanged;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public GridCell[,] Cells { get; private set; }
    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public Grid(int width, int height, GridCell[,] cells)
    {
        Width = width;
        Height = height;
        Cells = cells;
    }
    public void SetCells(GridCell[,] cells) 
    {
    Cells = cells;
    }
    public void SetCell(int x, int y, GridCell gridCell)
    {
        if (IsCoordInRange(x, y))
        {
            Cells[x, y] = gridCell;
        }
        Debug.LogError($"Координати не в межах сітки {x}, {y}");
    }
    public void SetValue(int x, int y, GridObject gridObject)
    {
        if (IsCoordInRange(x, y))
        {
            Cells[x, y].SetObject(gridObject);
            return;
        }
        Debug.LogError($"Координати не в межах сітки {x}, {y}");
    }
    public void SetValue(GridCell gridCell, GridObject gridObject)
    {
        gridCell.SetObject(gridObject);
    }
    public void SetCellEmpty(int x, int y) 
    {
        if (IsCoordInRange(x, y))
        {
            Cells[x, y].ClearCell();
            return;
        }
        Debug.LogError($"Координати не в межах сітки {x}, {y}");
    }
    public void SetCellEmpty(GridCell gridCell)
    {
        gridCell.ClearCell();
    }
    public GridCell GetCell(int x, int y) 
    {
        if (IsCoordInRange(x, y))
        {
           return Cells[x, y];
        }
        return null;
    }
    //public List<Vector2> GetCoordSameObjects(GridObject gridObject) 
    //{
    //    List<Vector2> coordsSameObjects = new List<Vector2>();
    //    for (int x = 0; x < Weight; x++) 
    //    {
    //        for (int y = 0; y < Height; y++) 
    //        {
    //            if (Cells[x, y].ObjectInfo.Type == gridObject.ObjectInfo.Type) 
    //            {
    //                coordsSameObjects.Add(new Vector2(x, y));
    //            }
    //        }
    //    }
    //    return coordsSameObjects;
    //} 

    private bool IsCoordInRange(int x, int y) 
    {
        return x >= 0 && y >= 0 && x < Width && y < Height;
    }
}

