using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GridObject : MonoBehaviour
{ 
    [field: SerializeField] public GridObjectSO Info { get; protected set; }
    public Vector2Int GridCoord { get; protected set; }
    virtual public void Setup(GridObjectSO objectInfo)
    { 
        Info = objectInfo;
        GetComponent<Image>().sprite = Info.Sprite;
    }
    virtual public void SetGridCoord(Vector2Int gridCoord, GridCell gridCell) 
    {
        GridCoord = gridCoord;
        transform.SetParent(gridCell.transform);
        transform.localScale = Vector2.one;
        transform.DOLocalMove(Vector2.zero, 1f).OnComplete(() => { });
        //transform.localPosition = Vector2.zero;
    }
    virtual public void Destroy() { }
}
