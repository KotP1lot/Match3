using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BorderManager : MonoBehaviour
{
    [SerializeField] db_BorderSO db;
    [SerializeField] Border prefab;
    Grid grid;
    GridCell cell;
    Dictionary<Direction, Border> borders = new();
    public bool IsBorderExist(Direction dir)
    {
        return borders.ContainsKey(dir);
    }
    public void Setup()
    {
        cell = GetComponent<GridCell>();
        grid = cell.grid;
    }
    public void AddBorder(Direction dir, BorderType type)
    {
        BorderSO so = db.GetByType(type);
        if (so != null && !borders.ContainsKey(dir))
        {
            Border b = Instantiate(prefab, cell.transform);
            float z = dir switch
            {
                Direction.Top => 90f,
                Direction.Bottom => -90f,
                Direction.Left => 180f,
                Direction.Right => 0f,
                _ => 0f
            };
            b.transform.DORotate(new Vector3(0, 0, z), 0f);
            b.Setup(so, dir);
            b.Subcribe(GetCells(dir));
            b.OnBorderDestroy += OnBorderDestroyHandler;
            borders.Add(dir, b);
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError("Відсутній такий тип у db_BorderSO");
        }
#endif
    }
    private void OnBorderDestroyHandler(Direction dir)
    {
        borders.Remove(dir);
    }
    private List<GridCell> GetCells(Direction dir)
    {
        int x = cell.x;
        int y = cell.y;
        List<GridCell> cells = new() { cell };
        switch (dir)
        {
            case Direction.Top:
                if (y < grid.Height - 1)
                    cells.Add(grid.GetCell(x, y + 1));
                break;
            case Direction.Bottom:
                if (y > 0)
                    cells.Add(grid.GetCell(x, y - 1));
                break;
            case Direction.Left:
                if (x > 0)
                    cells.Add(grid.GetCell(x - 1, y));
                break;
            case Direction.Right:
                if (x < grid.Width - 1)
                    cells.Add(grid.GetCell(x + 1, y));
                break;
        }
        return cells;
    }
}
