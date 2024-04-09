using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
[DefaultExecutionOrder(1)]
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
    [SerializeField] Transform SpawnPoint;
    GemPoolList gemPoolList;
    [SerializeField] Transform gemPoolTransform;


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
            gemPoolList.AddPool(obj.Type, new GemPool(gemPoolTransform, gem, obj, 20));
        }
    }

    private void Start()
    {
        SpawnGem();
        GemLine = new GemMatchLine();
        GemLine.OnGridChanged += OnGridChangedHandler;
        EventManager.instance.OnBonusActivated += OnBonusActivatedHandler;
    }

    private void OnBonusActivatedHandler(BonusGem bonus)
    {
        int randomX = Random.Range(0, width);
        GridCell cell = grid.GetCell(randomX, height - 1);
        cell.DestroyGridObject();
        cell.SetObject(bonus);
        bonus.gameObject.SetActive(true);
    }

    private void Setup()
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
                GridCell cell = grid.GetCell(x, y);
                cell.SpawnObject(SpawnPoint, gemPoolList.GetRandomPool().Get());
            }
        }
        EventManager.instance.OnGemFallen?.Invoke();
        for (int i = 0; i < 5; i++)
        {
            GridObject gridObject = Instantiate(wall, transform);
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            GridCell cell = grid.GetCell(x, y);
            cell.DestroyGridObject();
            cell.SetObject(gridObject);
        }
        for (int i = 0; i < 1; i++)
        {
            GridObject gridObject = Instantiate(wall, transform);
            GridCell cell = grid.GetCell(0, 4);
            cell.DestroyGridObject();
            cell.SetObject(gridObject);
        }
        //BonusGem bonusGem = Instantiate(BGLine);
        //int x1 = Random.Range(0, width);
        //int y1 = Random.Range(0, height);
        //bonusGem.Setup(gemSo[1]);
        //grid.GetCell(x1, y1).DestroyGridObject();
        //grid.SetValue(x1, y1, bonusGem);

        //BonusGem bonusGem1 = Instantiate(BGBomb);
        //x1 = Random.Range(0, width);
        //y1 = Random.Range(0, height);
        //bonusGem1.Setup(gemSo[3]);
        //grid.GetCell(x1, y1).DestroyGridObject();
        //grid.SetValue(x1, y1, bonusGem1);

    }
    private void OnGridChangedHandler()
    {
        FallGem();
    }
    #region NEW FALL SYSTEM
    private async Task SpawnGemInLastY(int x)
    {
        GridCell cell = grid.GetCell(x, height - 1);
        await cell.SpawnObject(SpawnPoint, gemPoolList.GetRandomPool().Get()).AsyncWaitForCompletion();
    }
    private void FallGem()
    {
        for (int x = 0; x < grid.Width; x++)
        {
            FallCollumn(x,1);
        }
    }
    private async void FallCollumn(int x, int y) 
    {
        await GemFallInColumn(x,y);
        y++;
        if (y < height-1)
        {
            FallCollumn(x, y);
        }
        else 
        {
            GridCell cell = grid.GetCell(x, height - 1);
            if (cell.IsEmpty()) 
            {
                await SpawnGemInLastY(x);
                FallCollumn(x, height - 1);
            }
            else 
            {
                await GemFallInColumn(x, height-1);
                if (!cell.IsEmpty()) return;
                else FallCollumn(x, height - 1);
            }
        }
    }
    private async Task GemFallInColumn(int x, int y) 
    {
        GridCell cell = grid.GetCell(x, y);
        if (cell.IsEmpty() || !cell.GridObject.Info.IsAffectedByGravity || y == 0) return;

        GridCell cellUnder = grid.GetCell(x, y - 1);
        if (cellUnder.IsEmpty())
        {
            await cellUnder.SetObject(cell.GridObject).AsyncWaitForCompletion(); // Очікуємо завершення анімації

            cell.ClearCell();
            await GemFallInColumn(x, --y);
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
