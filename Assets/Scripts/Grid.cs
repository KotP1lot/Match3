using System;
using System.Collections.Generic;
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
    public GridCell GetCell(int x, int y)
    {
        if (IsCoordInRange(x, y))
        {
            return Cells[x, y];
        }
        return null;
    }
    public List<GridCell> GetRow(GridCell cell) 
    {
        List<GridCell> rowCells = new List<GridCell>();
        int y = cell.Y;
        for (int x = 0; x < Width; x++)
        {
            if (x == cell.X) continue;
            rowCells.Add(GetCell(x, y));
        }
        return rowCells;
    }
   
    public List<GridCell> GetRow(GridCell cell, int count, bool isTwoWayCheck = true, bool isRightCheck = false)
    {
        int x = cell.X;
        int y = cell.Y;
        int startX = isTwoWayCheck ? x - count : (isRightCheck ? x : x - count);

        Func<int, bool> continueCondition = isTwoWayCheck ?
                                       (checkX => checkX < x + count) :
                                       (checkX => isRightCheck ? checkX < x + count : checkX < x);
        List<GridCell> resultCells = new List<GridCell>();
        for (int checkX = startX; continueCondition(checkX); checkX++)
        {
            if (IsCoordInRange(checkX, y))
            {
                if (checkX == cell.X) continue;
                resultCells.Add(GetCell(checkX, y));
            }
        }
        return resultCells;
    }
    public List<GridCell> GetColumn(GridCell cell) 
    {
        List<GridCell> columnCells = new List<GridCell>();
        int x = cell.X;
        for (int y = 0; y < Height; y++)
        {
            if (y == cell.Y) continue;
            columnCells.Add(GetCell(x, y));
        }
        return columnCells;
    }
    public List<GridCell> GetColumn(GridCell cell, int count, bool isTwoWayCheck = true, bool isUpwardCheck = false)
    {
        int x = cell.X;
        int y = cell.Y;
        int startY = isTwoWayCheck ? y - count : (isUpwardCheck ? y : y - count);
        Func<int, bool> continueCondition = isTwoWayCheck ?
                                       (checkY => checkY < y + count) :
                                       (checkY => isUpwardCheck ? checkY < y + count : checkY < y);
        List<GridCell> resultCells = new List<GridCell>();
        for (int checkY = startY; continueCondition(checkY); checkY++)
        {
            if (IsCoordInRange(x, checkY))
            {
                if (checkY == cell.Y) continue;
                resultCells.Add(GetCell(x,checkY));
            }
        }
        return resultCells;
    }
    public List<GridCell> GetNeighborCellsInRadius(GridCell cell, int radius)
    {
        List<GridCell> neighborCells = new List<GridCell>();

        int centerX = cell.X;
        int centerY = cell.Y;

        for (int x = centerX - radius; x <= centerX + radius; x++)
        {
            for (int y = centerY - radius; y <= centerY + radius; y++)
            {
                if (!IsCoordInRange(x, y) || (x == centerX && y == centerY))
                {
                    continue;
                }
                neighborCells.Add(GetCell(x, y));
            }
        }

        return neighborCells;
    }
    public List<GridCell> GetNeighboringCells(GridCell cell) 
    {
        List<GridCell> adjacentCells = new List<GridCell>();

        int x = cell.X;
        int y = cell.Y;

        // Перевірка клітинок в околиці поточної клітинки (верх, низ, ліво, право)
        if (IsCoordInRange(x, y - 1))
            adjacentCells.Add(GetCell(x, y - 1)); // Нижня клітина
        if (IsCoordInRange(x, y + 1))
            adjacentCells.Add(GetCell(x, y + 1)); // Верхня клітина
        if (IsCoordInRange(x - 1, y))
            adjacentCells.Add(GetCell(x - 1, y)); // Ліва клітина
        if (IsCoordInRange(x + 1, y))
            adjacentCells.Add(GetCell(x + 1, y)); // Права клітина
        return adjacentCells;
    }
    public List<GridCell> GetAdjacentCells(GridCell cell)
    {
        List<GridCell> adjacentCells = new List<GridCell>();

        int x = cell.X;
        int y = cell.Y;

        // Перевірка клітинок в околиці поточної клітинки (верх, низ, ліво, право)
        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                if (xOffset == 0 && yOffset == 0) continue; // Пропустити поточну клітинку
                int newX = x + xOffset;
                int newY = y + yOffset;
                if (IsCoordInRange(newX, newY))
                {
                    adjacentCells.Add(GetCell(newX, newY));
                }
            }
        }
        return adjacentCells;
    }
    private bool IsCoordInRange(int x, int y) 
    {
        return x >= 0 && y >= 0 && x < Width && y < Height;
    }
}

