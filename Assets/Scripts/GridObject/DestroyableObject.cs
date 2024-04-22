using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyableObject : GridObject
{
    public Action<DestroyableObject> OnDestroyableObjectDestroyed;
    private GridCell cell;
    [SerializeField] List<Sprite> allState = new List<Sprite>();
    private Image image;
    private int health;
    [SerializeField] bool existAfterDestroy = false;
    public override Tween Spawn(Transform spawnPos, GridCell gridCell)
    {
        image = GetComponent<Image>();
        health = allState.Count;
        image.sprite = allState[health-1];
        return base.Spawn(spawnPos, gridCell);
    }
    override public Tween SetGridCoord(GridCell gridCell)
    {
        cell = gridCell;
        Subcribe();
        return base.SetGridCoord(gridCell);
    }
    private void Subcribe() 
    {
        foreach(GridCell cell in cell.GetNeighboringCells()) 
        {
            cell.OnGemDestroyinCell += OnGemsDestroyedInNeighboringCells;
        }
    }
    private void Unsubcribe() 
    {
        foreach (GridCell cell in cell.GetNeighboringCells())
        {
            cell.OnGemDestroyinCell -= OnGemsDestroyedInNeighboringCells;
        }
    }
    private void OnGemsDestroyedInNeighboringCells() 
    {
        health = Math.Clamp(health-1, 1, health);
        image.sprite = allState[health-1];
        if (health-1 <= 0)
        {
            if (!existAfterDestroy)
            {
                cell.Clear();
            }
            Unsubcribe();
            OnDestroyableObjectDestroyed?.Invoke(this);
        }
    }
    public override void Clear()
    {
        Unsubcribe();
        GameObject.Destroy(gameObject);
    }
}
