using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

[DefaultExecutionOrder(1)]
public class GridManager : MonoBehaviour
{
    public Grid grid;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] Vector2Int CheckGem;
    [SerializeField] GemSO[] gemSo;
    [SerializeField] Gem gem;
    [SerializeField] GridObject wall;
    [SerializeField] DestroyableObject destroyable;
    [SerializeField] DestroyableObject DestrPrefab;
    [SerializeField] LineDestroyer V_lineDestroyer;
    [SerializeField] LineDestroyer H_lineDestroyer;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] LvlSO lvl;
    public GemPoolList gemPoolList;
    [SerializeField] Transform gemPoolTransform;
    readonly List<BonusGem> waitForSpawn = new();

    public bool IsLineActive = false;
    public bool isBusy = false;
    public GemMatchLine GemLine;
    private void Awake()
    {
        gemPoolList = new GemPoolList();
        SetupGemPool();
    }
    private void SetupGemPool()
    {
        foreach (var obj in gemSo)
        {
            gemPoolList.AddPool(obj.type, new GemPool(gemPoolTransform, gem, obj, 20));
        }
    }
    private /*async Task*/ void Setup()
    {
        GridCell[] visualCell = GetComponentsInChildren<GridCell>();
        GridCell[,] gridCells = new GridCell[width, height];
        grid = new Grid(width, height);
        int index = 0;
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                gridCells[x, y] = visualCell[index];
                gridCells[x, y].Setup(this, grid, x, y);
                index++;
            }
        }
        //if (lvl is not null)
        //{
        //    index = 0;
        //    for (int y = height - 1; y >= 0; y--)
        //    {
        //        for (int x = 0; x < width; x++)
        //        {
        //            if (lvl.gridObjects[index] is null) 
        //            {
        //                index++;
        //                continue;
        //            }
        //          await  gridCells[x, y].SpawnNewObject(lvl.gridObjects[index]).AsyncWaitForCompletion();
        //            index++;
        //        }
        //    }
        //}
        grid.SetCells(gridCells);
        grid.OnGridChanged += OnGridChangedHandler;
    }
    private async void Start()
    {
        isBusy = true;
        /*await*/ Setup();
        SpawnGem();
        GemLine = new GemMatchLine();
        GemLine.OnLineDestroy += OnGridChangedHandler;
        EventManager.instance.OnBonusCharged += OnBonusActivatedHandler;
    }
    private async void SpawnGem()
    {
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                GridCell cell = grid.GetCell(x, y);
                if(cell.IsEmpty())
               cell.SpawnObject(SpawnPoint, gemPoolList.GetRandomPool().Get());
            }
        }
        for (int x = 0; x < grid.Width; x++)
        {
            GridCell cell = grid.GetCell(x, 2);
            cell.AddBorder(Direction.Top, BorderType.simplyWood);
        }
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                GridCell cell = grid.GetCell(x, y);
                cell.AddFloor(FloorType.simpty);
            }
        }
        await ChekForPosibleMathcLine();
        isBusy = false;
   
    }
    private void OnBonusActivatedHandler(BonusGem bonus)
    {
        waitForSpawn.Add(bonus);
    }
    private async Task SpawnBonusGem()
    {
        foreach (BonusGem bg in waitForSpawn)
        {
            int randomX = Random.Range(0, width);
            int randomY = Random.Range(0, height);
            GridCell cell = grid.GetCell(randomX, randomY);
            cell.Clear();
            await cell.SetObject(bg).AsyncWaitForCompletion();
        }
        waitForSpawn.Clear();
    }
    private void OnGridChangedHandler()
    {
        FallGem();
        isBusy = true;
    }
    private async Task ChekForPosibleMathcLine()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GridCell cell = grid.GetCell(x, y);
                if (IsPosibleToContinue(cell))
                {
                    return;
                }
            }
        }
        await ShuffleGem();
    }
    private async Task ShuffleGem()
    {
        List<GridCell> cells = new();
        Dictionary<GemType, Queue<Gem>> gems = new();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GridCell cell = grid.GetCell(x, y);
                if (cell.IsHasGem())
                {
                    Gem gem = cell.Gem;
                    GemType type = gem.GetGemType();
                    if (!gems.ContainsKey(type))
                    {
                        gems[type] = new Queue<Gem>();
                        gems[type].Enqueue(gem);
                    }
                    else
                    {
                        gems[type].Enqueue(gem);
                    }
                    cells.Add(cell);
                }
            }
        }
        bool isMoreThan3 = false;
        foreach (GemType key in gems.Keys)
        {
            if (gems[key].Count >= 3)
            {
                isMoreThan3 = true;
                break;
            }
        }
        if (!isMoreThan3)
        {
            GemType[] keys = gems.Keys.ToArray();
            do
            {
                gems[keys[0]].Enqueue(gemPoolList.GetPoolByType(keys[0]).Get());
                gems[keys[1]].Dequeue().Clear();
            } while (gems[keys[0]].Count < 3);
      
        }
        int count = 0;
        foreach (GemType key in gems.Keys)
        {
            foreach (Gem gem in gems[key])
            {
                await cells[count].SetObject(gem).AsyncWaitForCompletion();
                count++;
            }
        }
        cells.Clear();
        gems.Clear();
    }
    private bool IsPosibleToContinue(GridCell cell)
    {
        if (!cell.IsHasGem()) return false;
        int count = 0;
        GemType gemType = cell.Gem.GetGemType();
        List<GridCell> adjacentCells = cell.GetAdjacentCells();
        adjacentCells.Add(cell);
        foreach (GridCell a_cell in adjacentCells)
        {
            if (!a_cell.IsHasGem()) continue;
            if (a_cell.Gem.GetGemType() == gemType)
                count++;
        }
        if (count >= 3) return true;
        return false;
    }

    #region NEW FALL SYSTEM
    private async Task SpawnGemInLastY(int x)
    {
        GridCell cell = grid.GetCell(x, height - 1);
        await cell.SpawnObject(SpawnPoint, gemPoolList.GetRandomPool().Get()).AsyncWaitForCompletion();
    }
    private async void FallGem()
    {
        List<Task> tasks = new();
        for (int x = 0; x < grid.Width; x++)
        {
            tasks.Add(FallCollumn(x, 1));
        }
        await Task.WhenAll(tasks);
        for (int x = 0; x < grid.Width; x++)
        {
            await FallInColumDiagonal(x, 1);
        }
        for (int x = grid.Width - 1; x >= 0; x--)
        {
            await FallInColumDiagonal(x, 1);
        }
        await SpawnBonusGem();
        await ChekForPosibleMathcLine();
        isBusy = false;
        EventManager.instance.TurnEnded?.Invoke();
    }
    private async Task FallInColumDiagonal(int x, int y)
    {
        await GemFallInColumnDiagonal(x, y);
        y++;
        if (y < height - 1)
        {
            await FallInColumDiagonal(x, y);
        }
        else
        {
            GridCell cell = grid.GetCell(x, height - 1);
            if (cell.IsEmpty())
            {
                await SpawnGemInLastY(x);
                await FallInColumDiagonal(x, height - 1);
            }
            else
            {
                await GemFallInColumnDiagonal(x, height - 1);
                if (!cell.IsEmpty()) return;
                else await FallInColumDiagonal(x, height - 1);
            }
        }
    }
    private async Task GemFallInColumnDiagonal(int x, int y)
    {
        GridCell cell = grid.GetCell(x, y);
        if (cell.IsEmpty() || !cell.GridObject.IsAffectedByGravity || y == 0) return;
        int[] xOffset;
        if (x == 0)
        {
            xOffset = new int[2] { 0, 1 };
        }
        else if (x == width - 1)
        {
            xOffset = new int[2] { 0, -1 };
        }
        else
        {
            xOffset = new int[3] { 0, -1, 1 };
        }

        foreach (int xOff in xOffset)
        {
            GridCell next = grid.GetCell(x + xOff, y - 1);
            Direction dir = xOff switch
            {
                0 => Direction.Bottom,
                -1 => Direction.Left,
                1 => Direction.Right,
                _ => Direction.Bottom
            };
            if (next.IsEmpty())
            {
                GridCell under = grid.GetCell(x, y - 1);
                if (dir == Direction.Bottom && next.IsBorderExist(Direction.Top) || cell.IsBorderExist(dir)) continue;
                if (dir == Direction.Right && (next.IsBorderExist(Direction.Top) && next.IsBorderExist(Direction.Left)) || (cell.IsBorderExist(Direction.Bottom) && cell.IsBorderExist(dir)))continue;
                if (dir == Direction.Left && (next.IsBorderExist(Direction.Top) && next.IsBorderExist(Direction.Right)) || (cell.IsBorderExist(Direction.Bottom) && cell.IsBorderExist(dir))) continue;
                if ((under.IsBorderExist(Direction.Top) && next.IsBorderExist(Direction.Top))) continue;
                await next.SetObject(cell.GridObject).AsyncWaitForCompletion(); // Очікуємо завершення анімації

                cell.SetEmpty();
                await GemFallInColumnDiagonal(next.x, --y);
                return;
            }
        }
    }
    private async Task FallCollumn(int x, int y)
    {
        await GemFallInColumn(x, y);
        y++;
        if (y < height - 1)
        {
            await FallCollumn(x, y);
        }
        else
        {
            GridCell cell = grid.GetCell(x, height - 1);
            if (cell.IsEmpty())
            {
                await SpawnGemInLastY(x);
                await FallCollumn(x, height - 1);
            }
            else
            {
                await GemFallInColumn(x, height - 1);
                if (!cell.IsEmpty()) return;
                else await FallCollumn(x, height - 1);
            }
        }
    }
    private async Task GemFallInColumn(int x, int y)
    {
        GridCell cell = grid.GetCell(x, y);
        if (cell.IsEmpty() || !cell.GridObject.IsAffectedByGravity || y == 0) return;

        GridCell cellUnder = grid.GetCell(x, y - 1);
        if (cellUnder.IsEmpty() && !cellUnder.IsBorderExist(Direction.Top) && !cell.IsBorderExist(Direction.Bottom))
        {
            await cellUnder.SetObject(cell.GridObject).AsyncWaitForCompletion(); // Очікуємо завершення анімації

            cell.SetEmpty();
            await GemFallInColumn(x, --y);
        }
    }
    #endregion
    public void StartLine(GridCell gridCell)
    {
        if (isBusy) return;
        if (gridCell.IsEmpty()) return;
        IsLineActive = true;
        GemLine.NewGemMatch3Line(gridCell);
    }
    public void AddActiveGridCell(GridCell gridCell)
    {
        if (isBusy) return;
        if (gridCell.IsEmpty()) return;
        GemLine.AddMatch3Gem(gridCell);
    }
    public void TryToDestroyLine()
    {
        if (isBusy) return;
        IsLineActive = false;
        GemLine.DeactivateCells();
    }
}
