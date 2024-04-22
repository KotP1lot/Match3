using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    public LvlSO lvl;
    int width = 7;
    int height = 7;
    private GridObject currentPref;
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
                gridCells[x, y].OnBGCClick += SetObjectToCell;
                gridCells[x, y].SetGridObject(lvl.gridObjects[index]);
                index++;
            }
        }
    }

    private void SetObjectToCell(BGridCell cell)
    {
        Debug.Log($"Set");
        cell.SetGridObject(currentPref);
    }
    public void Clear()
    {
        Debug.Log($"Chosen is null");
        currentPref = null;
    }
    public void ChoosePref(GridObject pref)
    {
        Debug.Log($"{pref.name} chosen");
        currentPref = pref;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }
#if UNITY_EDITOR
    public void Save()
    {
        int index = 0;
        Debug.Log("Save");
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                if (gridCells[x, y].gridObjectPrefab is null)
                {
                    lvl.gridObjects[index] = null;
                }
                lvl.gridObjects[index] = gridCells[x, y].gridObjectPrefab;
                index++;
            }
        }
        lvl.S = 69;
        EditorUtility.SetDirty(lvl);
        AssetDatabase.SaveAssets();
    }
}
#endif