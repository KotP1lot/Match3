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
    [SerializeField] Transform SpawnPoint;
    [SerializeField] Transform DestroyPoint;
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
        GemLine = new GemMatchLine();
        GemLine.OnLineDestroy += OnGridChangedHandler;
        SetupGemPool();
    }
    private void SetupGemPool()
    {
        foreach (var obj in gemSo)
        {
            gemPoolList.AddPool(obj.type, new GemPool(gemPoolTransform, gem, obj, 20));
        }
    }
    private void CellSetup()
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
                gridCells[x, y].Clear();
                gridCells[x, y].Setup(this, grid, x, y);
                index++;
            }
        }
        grid.SetCells(gridCells);
        grid.OnGridChanged += OnGridChangedHandler;
        
        if (lvl != null)
        {
            index = 0;
            for (int x = 0; x < 49; x++)
            {
                visualCell[x].Setup(lvl.cells[x]);
            };
        }
    }
    public Transform GetDestroyPoint() 
    {
        return DestroyPoint;
    }
    public async void Setup(LvlSO lvl)
    {
        this.lvl = lvl;
        isBusy = true;
        CellSetup();
        await FallGem();
        EventManager.instance.OnBonusCharged += OnBonusActivatedHandler;
    }
    //private async void SpawnGem()
    //{
    //    for (int x = 0; x < grid.Width; x++)
    //    {
    //        for (int y = 0; y < grid.Height; y++)
    //        {
    //            GridCell cell = grid.GetCell(x, y);
    //            if(cell.IsEmpty())
    //           cell.SpawnObject(SpawnPoint, gemPoolList.GetRandomPool().Get());
    //        }
    //    }
    //    for (int x = 0; x < grid.Width; x++)
    //    {
    //        GridCell cell = grid.GetCell(x, 2);
    //        cell.AddBorder(Direction.Top, BorderType.simplyWood);
    //    }
    //    for (int x = 0; x < grid.Width; x++)
    //    {
    //        for (int y = 0; y < grid.Height; y++)
    //        {
    //            GridCell cell = grid.GetCell(x, y);
    //            cell.AddFloor(FloorType.simpty);
    //        }
    //    }
    //    await ChekForPosibleMathcLine();
    //    isBusy = false;
    //}
    private void OnBonusActivatedHandler(BonusGem bonus)
    {
        waitForSpawn.Add(bonus);
    }
    private async Task SpawnBonusGem()
    {
        foreach (BonusGem bg in waitForSpawn)
        {
            GridCell cell = GetRandomCell(bg.GetGemType());
            cell.Clear();
            await cell.SetObject(bg).AsyncWaitForCompletion();
        }
        waitForSpawn.Clear();
    }
    private GridCell GetRandomCell(GemType type) 
    {
        int randomX = Random.Range(0, width);
        int randomY = Random.Range(0, height);
        GridCell cell =  grid.GetCell(randomX, randomY);
        if (cell.GridObject is Wall || cell.GridObject is BonusGem || cell.Gem.GetGemType() != type)
            return GetRandomCell(type);
        return cell;
    }
    private void OnGridChangedHandler()
    {
        isBusy = true;
        FallGem();
    }
    private bool CheckCellToContinue(GridCell activeGC) 
    {
        if (!activeGC.IsHasGem()) return false;
        List<GridCell> ajCells = activeGC.GetAdjacentCells();
        List<GridCell> cellWithSameGem = new();
        foreach (GridCell aj in ajCells) 
        {
            if (aj.IsHasGem() && aj.Gem.GetGemType() == activeGC.Gem.GetGemType())
                cellWithSameGem.Add(aj);
        }
        if (cellWithSameGem.Count >= 2)
        {
            int count = 0;
            foreach (GridCell cell in cellWithSameGem)
            {
                if (GemLine.CanMatchLine(cell, activeGC)) count++;
            }
            if(count>=2) return true;
        }
        return false;
    }
    private async Task CheckForContinue() 
    {
        if (ChekForPosibleMathcLine()) return;
        await ShuffleGem();
    }
    private bool ChekForPosibleMathcLine()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GridCell cell = grid.GetCell(x, y);
                if (CheckCellToContinue(cell))
                {
                    return true;
                }
            }
        }
        return false;
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
        List<Gem> allGems = gems.Values.SelectMany(queue => queue).ToList();
        await Shuffle(allGems, cells);
        cells.Clear();
        gems.Clear();
    }
    public async Task Shuffle(List<Gem> allGems, List<GridCell> cells) 
    {
        Utility.Shuffle(allGems);
        int count = 0;
        List<Task> tasks = new();
        foreach (Gem gem in allGems)
        {
            tasks.Add(cells[count].SetObject(gem).AsyncWaitForCompletion());
            count++;
        }
        await Task.WhenAll(tasks);
        if (ChekForPosibleMathcLine()) return;
   
        await Shuffle(allGems, cells);
    }

    #region NEW FALL SYSTEM
    private async Task SpawnGemInLastY(int x)
    {
        GridCell cell = grid.GetCell(x, height - 1);
        await cell.SpawnObject(SpawnPoint, gemPoolList.GetRandomPool().Get()).AsyncWaitForCompletion();
    }
    private async Task FallGem()
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
        await CheckForContinue();
        isBusy = false;
        EventManager.instance.OnTurnEnded?.Invoke();
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
            GridCell dirNeighbor = grid.GetCell(x + xOff, y);
          
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
                if (!CanFall(dir, cell, under, dirNeighbor, next)) continue;
                await next.SetObject(cell.GridObject).AsyncWaitForCompletion(); // Очікуємо завершення анімації

                cell.SetEmpty();
                await GemFallInColumnDiagonal(next.x, --y);
                return;
            }
        }
    }
    private bool CanFall(Direction dir, GridCell current, GridCell under, GridCell neighbor, GridCell next)
    {
        if (dir != Direction.Bottom)
        {
            if ((current.IsBorderExist(Direction.Bottom) && neighbor.IsBorderExist(Direction.Bottom)) || (under.IsBorderExist(Direction.Top) && next.IsBorderExist(Direction.Top)))
                return false;
            if ((current.IsBorderExist(Direction.Bottom) && !neighbor.IsEmpty() && neighbor.GridObject is Wall) || (next.IsBorderExist(Direction.Top) && !under.IsEmpty() && under.GridObject is Wall))
                return false;
            if (dir == Direction.Right)
            {
                if ((current.IsBorderExist(Direction.Right) && current.IsBorderExist(Direction.Bottom)) || (next.IsBorderExist(Direction.Left) && next.IsBorderExist(Direction.Top)))
                {
                    return false;
                }
            }
            if (dir == Direction.Left)
            {
                if ((current.IsBorderExist(Direction.Left) && current.IsBorderExist(Direction.Bottom)) || (next.IsBorderExist(Direction.Right) && next.IsBorderExist(Direction.Top)))
                {
                    return false;
                }
            }
            return true;
        }
        else 
        {
            if (next.IsBorderExist(Direction.Top) || current.IsBorderExist(Direction.Bottom)) return false;
            return true;
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
