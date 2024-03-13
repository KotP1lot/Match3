using System;
using UnityEngine;
using UnityEngine.UI;

public class GridObject : MonoBehaviour
{
    public  Action<GridObject> OnDestroy;
    public GridObjectSO Info { get; private set; }
    public Vector2Int GridCoord { get; private set; }
    
    [SerializeField] GameObject activeIndicator;

    public void Setup(GridObjectSO objectInfo)
    {
        Info = objectInfo;
        GetComponent<Image>().color = Info.Sprite;
    }
    public void SetGridCoord(Vector2Int gridCoord, GridCell gridCell) 
    {
        GridCoord = gridCoord;
        transform.SetParent(gridCell.transform);
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector2.one;
    }
    public void SetActive(bool isActive) 
    {
        activeIndicator.SetActive(isActive);
    }
    public void Destroy()
    {
        transform.SetParent(null);
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector2.one;
        activeIndicator.SetActive(false);
        OnDestroy?.Invoke(this);
    }

}
