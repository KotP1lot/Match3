using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BGLineDestroyer : BonusGem
{
    [SerializeField] bool isHorizontal;
    [SerializeField] int gemCountToDestroy;
    public override async Task Destroy(Action callback, Transform target)
    {
        isActivated = true;
        cells = isHorizontal ? cell.GetRow(gemCountToDestroy) : cell.GetColumn(gemCountToDestroy);
        DeactFoBG();
        Quaternion rotation = transform.rotation;
        Shake();
        List<Task> tasks = new();
        foreach (GridCell cell in cells)
        {
            if (cell.IsEmpty() ||( cell.GridObject is BonusGem bonus && bonus.isActivated))
            {
                continue;
            }
            tasks.Add(cell.DestroyGridObject(transform));
        }
        await Task.WhenAll(tasks);
        BGReset(rotation);
        await base.Destroy(callback, target);
    }
}
