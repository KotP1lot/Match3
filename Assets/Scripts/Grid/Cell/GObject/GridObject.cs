using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public class GridObject : MonoBehaviour
{ 
    public bool IsAffectedByGravity;
    virtual public Tween Spawn(Transform spawnPos, GridCell gridCell) 
    {
        transform.position = new Vector2(gridCell.transform.position.x, spawnPos.position.y);
        return SetGridCoord(gridCell);
    }
    virtual public Tween SetGridCoord(GridCell gridCell)
    {
        transform.SetParent(gridCell.transform);
        transform.localScale = Vector2.one;
        return transform.DOLocalMove(Vector2.zero, 0.075f);
    }
    virtual public async Task Destroy(Action callback, Transform target) {}
    virtual public void Clear() { }
}
