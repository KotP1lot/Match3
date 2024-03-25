using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class GridManager : MonoBehaviour
{
    public Grid grid;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] Vector2Int CheckGem;
    [SerializeField] GridObjectSO[] gemSo;
    [SerializeField] Gem gem;
    [SerializeField] GridObject wall;
    [SerializeField] DestroyableObject destroyable;
    [SerializeField] LineDestroyer V_lineDestroyer;
    [SerializeField] LineDestroyer H_lineDestroyer;
    [SerializeField] BonusGem BGLine;
    [SerializeField] BonusGem BGBomb;
    GemPoolList gemPoolList;


    public bool IsLineActive = false;
    public GemMatchLine GemLine;
    private void Awake()
    {
        gemPoolList = new GemPoolList();
        SetupGemPool();
        Setup();
    }

    private void SetupGemPool()
    {
        foreach (var obj in gemSo) 
        {
            gemPoolList.AddPool(obj.Type, new GemPool(gem, obj, 20));
        }
    }

    private void Start()
    {
        SpawnGem();
        GemLine = new GemMatchLine();
        GemLine.OnGridChanged += OnGridChangedHandler;
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
    private void SpawnGem()
    {
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                grid.SetValue(x, y, gemPoolList.GetRandomPool().Get());
            }
        }
        for (int i = 0; i < 5; i++)
        {
            GridObject gridObject = Instantiate(wall);
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            grid.GetCell(x, y).DestroyGridObject();
            grid.SetValue(x, y, gridObject);
        }
        BonusGem bonusGem = Instantiate(BGLine);
        int x1 = Random.Range(0, width);
        int y1 = Random.Range(0, height);
        bonusGem.Setup(gemSo[1]);
        grid.GetCell(x1, y1).DestroyGridObject();
        grid.SetValue(x1, y1, bonusGem);

        BonusGem bonusGem1 = Instantiate(BGBomb);
        x1 = Random.Range(0, width);
        y1 = Random.Range(0, height);
        bonusGem1.Setup(gemSo[3]);
        grid.GetCell(x1, y1).DestroyGridObject();
        grid.SetValue(x1, y1, bonusGem1);

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
        for (int x = 0; x < width; x++)
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
            grid.SetValue(x, height - y, gemPoolList.GetRandomPool().Get());
        }
    }
    #endregion
    public void StartLine(GridCell gridCell)
    {
        if (gridCell.IsEmpty()) return;
        IsLineActive = true;
        GemLine.NewGemMatch3Line(gridCell);
    }
    public void AddActiveGridCell(GridCell gridCell)
    {
        if (gridCell.IsEmpty()) return;
        GemLine.AddMatch3Gem(gridCell);
    }
    public void TryToDestroyLine()
    {
        IsLineActive = false;
        GemLine.DeactivateCells();
    }

}
