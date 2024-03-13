using UnityEngine;

public class GridVisual : MonoBehaviour
{
    private Grid grid;
    public void SetGrid(Grid Grid) 
    {
        grid = Grid;
        GridCell[] visualCell = GetComponentsInChildren<GridCell>();
        int index = 0; 
        for (int i = 0; i < grid.Width; i++)
        {
            for (int j = 0; j < grid.Height; j++)
            {
                grid.SetCell(i, j, visualCell[index]);
                index++;
            }
        }
        grid.OnGridChanged += OnGridChangedHandler;
        SetVisual();
    }
    public void SetVisual() 
    {
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                VisualizeGridObject(x, y, grid.GetCell(x, y).GridObject);
            }
        }
    }
    private void VisualizeGridObject(int x, int y, GridObject gridObject) 
    {
        if (gridObject == null) return;
        gridObject.transform.SetParent(transform);
    }
    private void OnGridChangedHandler() 
    {
        Debug.Log("Changed");
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                VisualizeGridObject(x, y, grid.GetCell(x, y).GridObject);
            }
        }
    }
  
}
