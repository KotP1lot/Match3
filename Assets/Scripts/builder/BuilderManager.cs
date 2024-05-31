using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class BuilderManager : MonoBehaviour
{
    public LvlSO lvl;
    int width = 7;
    int height = 7;
    public FloorType floorType;
    public BorderType borderType;
    public GemType gemType;
    private GridObject currentPref;
    private BGridCell activeCell;
    BGridCell[] visualCell;
    BGridCell[,] gridCells;
    private void Start()
    {
        visualCell = GetComponentsInChildren<BGridCell>();
        gridCells = new BGridCell[width, height];
        int index = 0;
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                gridCells[x, y] = visualCell[index];
                gridCells[x, y].OnBGCClick += SetActiveCell;
                gridCells[x, y].Setup(lvl.cells[index]);
                index++;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetObjectToCell();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddFloorToCell();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveFloorInCell();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddBorderToCell(Direction.Left);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddBorderToCell(Direction.Top);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddBorderToCell(Direction.Right);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddBorderToCell(Direction.Bottom);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            RemoveBorderInCell(Direction.Left);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            RemoveBorderInCell(Direction.Top);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            RemoveBorderInCell(Direction.Right);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            RemoveBorderInCell(Direction.Bottom);
        }
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
#endif
    }
    private void SetObjectToCell()
    {
        activeCell.SetGridObject(currentPref, gemType);
    }
    private void AddBorderToCell(Direction dir)
    {
        activeCell.AddBorder(dir, borderType);
    }
    private void RemoveBorderInCell(Direction dir)
    {
        activeCell.RemoveBorder(dir);
    }
    private void AddFloorToCell()
    {
        activeCell.AddFloor(floorType);
    }
    private void RemoveFloorInCell()
    {
        activeCell.RemoveFloor();
    }
    private void SetActiveCell(BGridCell cell)
    {
        activeCell = cell;
    }
    public void Clear()
    {

        currentPref = null;
    }

    public void ChoosePref(GridObject pref)
    {

        currentPref = pref;
    }
#if UNITY_EDITOR

    public void Save()
    {

        lvl.cells = new CelLLvlInfo[49];
        for (int x = 0; x < 49; x++)
        {
            lvl.cells[x] = visualCell[x].GetCellInfo();
        };
        EditorUtility.SetDirty(lvl);
        AssetDatabase.SaveAssets();
    }
#endif
}