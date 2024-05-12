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
        Deactivate();
        GetComponent<Canvas>().sortingOrder = 2;
        Tween shake = transform.DOPunchRotation(new Vector3(0, 0, 30), 5)
                    .SetEase(Ease.OutSine); ;
        Tween scale = transform.DOScale(2, 0.2f)
            .SetEase(Ease.OutElastic);
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
        shake.Kill();
        await base.Destroy(callback, target);
    }
}
