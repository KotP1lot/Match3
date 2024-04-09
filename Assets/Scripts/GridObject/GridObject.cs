using DG.Tweening;
using UnityEngine;
public class GridObject : MonoBehaviour
{ 
    [field: SerializeField] public GridObjectSO Info { get; protected set; }
    public Vector2Int GridCoord { get; protected set; }
    public Tween Spawn(Transform spawnPos, Vector2Int gridCoord, GridCell gridCell) 
    {
        transform.position = new Vector2(gridCell.transform.position.x, spawnPos.position.y);
        return SetGridCoord(gridCoord, gridCell);
    }
    virtual public Tween SetGridCoord(Vector2Int gridCoord, GridCell gridCell)
    {
        GridCoord = gridCoord;
        transform.SetParent(gridCell.transform);
        return transform.DOLocalMove(Vector2.zero, 0.1f);
    }
    virtual public void Destroy() { }
}
