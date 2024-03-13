using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GridVisual gridVisual;
    public Grid grid;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] Vector2Int CheckGem;
    [SerializeField] GridObjectSO[] objectsSO;
    [SerializeField] GridObject gem;
    [SerializeField] GridObject wall;
    [SerializeField] GemPool fishPool;
    [SerializeField] GemPool salatPool;
    [SerializeField] GemPool bratPool;
    [SerializeField] GemPool wallPool;


    public bool IsLineActive = false;
    [SerializeField] List<GridCell> activeGridCells = new List<GridCell>();
    Dictionary<int, List<int>> emptyCells = new Dictionary<int, List<int>>();
    Dictionary<int, List<int>> emptyX_YCoord = new Dictionary<int, List<int>>();
    private void Awake()
    {
        fishPool = new GemPool(gem, objectsSO[0], 20);
        salatPool = new GemPool(gem, objectsSO[1], 20);
        bratPool = new GemPool(gem, objectsSO[2], 20);
        wallPool = new GemPool(wall, objectsSO[3], 5);
        Setup();
    }
    private void Start()
    {
        SpawnGem();
    }
    private void Setup()
    {
        GridCell[] visualCell = GetComponentsInChildren<GridCell>();
        GridCell[,] gridCells = new GridCell[width, height];
        grid = new Grid(width, height);
        int index = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                gridCells[j, i] = visualCell[index];
                gridCells[j, i].Setup(this, grid, j, i);
                index++;
            }
        }
        grid.SetCells(gridCells);
        grid.OnGridChanged += OnGridChangedHandler;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnGem();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(grid.GetCell(CheckGem.x, CheckGem.y).GridObject.Info.Name);
        }
    }
    private GemPool GetRandomGemPool()
    {
        int random = UnityEngine.Random.Range(0, 3);
        return random switch
        {
            0 => fishPool,
            1 => salatPool,
            2 => bratPool,
            _ => fishPool
        };
    }
    private void SpawnGem()
    {
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                grid.SetValue(x, y, GetRandomGemPool().Get());
            }
        }
        grid.GetCell(3, 4).DestroyGridObject();
        grid.SetValue(3, 4, wallPool.Get());

        grid.GetCell(2, 4).DestroyGridObject();
        grid.SetValue(2, 4, wallPool.Get());

        grid.GetCell(1, 4).DestroyGridObject();
        grid.SetValue(1, 4, wallPool.Get());


        grid.GetCell(4, 4).DestroyGridObject();
        grid.SetValue(4, 4, wallPool.Get());

        grid.GetCell(3, 1).DestroyGridObject();
        grid.SetValue(3, 1, wallPool.Get());

        grid.GetCell(2, 1).DestroyGridObject();
        grid.SetValue(2, 1, wallPool.Get());

        grid.GetCell(1, 1).DestroyGridObject();
        grid.SetValue(1, 1, wallPool.Get());


        grid.GetCell(4, 3).DestroyGridObject();
        grid.SetValue(4, 3, wallPool.Get());
     
}
    private void OnGridChangedHandler()
    {
        FallGem();
    }
    #region NEW FALL SYSTEM
    private void CellFallingDown(GridCell cell)
    {
        if (cell.Y == 0 || cell.IsEmpty() || !cell.GridObject.Info.IsAffectedByGravity)
        {
            return;
        }
        GridCell newCell = grid.GetCell(cell.X, cell.Y - 1);
        if (newCell.IsEmpty())
        {
            newCell.SetObject(cell.GridObject);
            cell.ClearCell();
            CellFallingDown(newCell);
            if (cell.Y == height - 1)
            {
                SpawnGemInLastY(cell.X, 1);
                CellFallingDown(cell);
            }
        }
    }
    private void CellFalling(GridCell cell) 
    {
        int x = cell.X;
        int y = cell.Y;

        if (cell.IsEmpty() || y == 0 || !cell.GridObject.Info.IsAffectedByGravity) return;

        int[] xOffset = { 0, 1, -1 };

        foreach (int offset in xOffset)
        {
            int newX = x + offset;
            if (newX >= 0 && newX < grid.Width)
            {
                GridCell newCell = grid.GetCell(newX, y - 1);
                if (newCell.IsEmpty())
                {
                    newCell.SetObject(cell.GridObject);
                    cell.ClearCell();
                    CellFalling(newCell);
                    if (y == height - 1)
                    {
                        SpawnGemInLastY(x, 1);
                    }
                    else
                    {
                        for (int y1 = y + 1; y1 < height; y1++)
                        {
                            GridCell upperCell = grid.GetCell(x, y1);
                            CellFalling(upperCell);
                        }
                    }
                    CellFalling(cell);
                    return;
                }
            }
        }
    }
    private void FallGem() 
    {
        if (emptyX_YCoord.Count == 0) return;

        foreach (int x in emptyX_YCoord.Keys)
        {
            for (int y = 0; y < height; y++)
            {
                GridCell cell = grid.GetCell(x, y);
                if (!cell.IsEmpty() && cell.GridObject.Info.IsAffectedByGravity) 
                {
                    CellFallingDown(cell);
                }
                if (cell.IsEmpty() && y == height - 1) 
                {
                    SpawnGemInLastY(x, 1);
                    CellFallingDown(cell);
                }
            }   
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = height - 1; y > 0; y--)
            {
                GridCell cell = grid.GetCell(x, y);
                CellFalling(cell);
            }
        }
    }
    private void SpawnGemInLastY(int x, int count) 
    {
        if (count == 0) return;
        for (int y = count; y > 0; y--)
        {
            grid.SetValue(x, height - y, GetRandomGemPool().Get());
        }
    }
    #endregion

    public void StartLine(GridCell gridCell)
    {
        if (gridCell.IsEmpty()) return;
        IsLineActive = true;
        activeGridCells.Add(gridCell);
        gridCell.GridObject.SetActive(true);
    }
    public void AddActiveGridCell(GridCell gridCell)
    {
        GridCell activeGridCell;
        if (gridCell.IsEmpty()) return;
        if (activeGridCells.Contains(gridCell))
        {
            if (activeGridCells.Count > 1)
            {
                activeGridCell = activeGridCells[activeGridCells.Count - 2];
                if (activeGridCell == gridCell)
                {
                    activeGridCells[activeGridCells.Count - 1].GridObject.SetActive(false);
                    activeGridCells.RemoveAt(activeGridCells.Count - 1);
                }
            }
            return;
        }
        activeGridCell = activeGridCells[activeGridCells.Count - 1];
        if (Mathf.Abs(activeGridCell.X - gridCell.X) <= 1 && Mathf.Abs(activeGridCell.Y - gridCell.Y) <= 1)
        {
            if (gridCell.GridObject.Info.Type == activeGridCells[0].GridObject.Info.Type)
            {
                activeGridCells.Add(gridCell);
                gridCell.GridObject.SetActive(true);
            }
        }
    }
    public void TryToDestroyLine()
    {
        IsLineActive = false;

        foreach (GridCell cell in activeGridCells)
        {
            if (activeGridCells.Count >= 3)
            {
                if (!emptyX_YCoord.ContainsKey(cell.X)) emptyX_YCoord[cell.X] = new List<int>();
                emptyX_YCoord[cell.X].Add(cell.Y);
                cell.DestroyGridObject();
            }
            else 
            {
                cell.GridObject.SetActive(false);
            }
        }
        activeGridCells.Clear();
        OnGridChangedHandler();
    }

}
