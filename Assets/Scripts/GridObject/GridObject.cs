using UnityEngine;
using UnityEngine.UI;

public class GridObject : MonoBehaviour
{ 
    public GridObjectSO Info { get; private set; }
    public Vector2Int GridCoord { get; private set; }

     public void Setup(GridObjectSO objectInfo)
    {
        Info = objectInfo;
        GetComponent<Image>().color = Info.Sprite;
    }
    virtual public void SetGridCoord(Vector2Int gridCoord, GridCell gridCell) 
    {
        GridCoord = gridCoord;
        transform.SetParent(gridCell.transform);
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector2.one;
    }
    virtual public void Destroy() { }
}
