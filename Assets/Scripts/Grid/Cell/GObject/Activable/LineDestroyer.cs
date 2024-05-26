using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LineDestroyer : ActivableObject
{
    [SerializeField] bool isHorizontal = true;
    [SerializeField] bool isTwoWay = true;
    [SerializeField] bool isRight_Upward = true;
    private List<GridCell> cells;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image image;
    override public void Setup()
    {
        isReady = true;
        count = 0;
        image.sprite = sprites[count];
        if (isTwoWay)
        {
            cells = isHorizontal ? cell.GetRow() : cell.GetColumn();
        }
        else
        {
            cells = isHorizontal ? cell.GetRow(7, false, isRight_Upward) : cell.GetColumn(7, false, isRight_Upward);
        }
    }
    override public void OnGemsDestroyInNeighboringCells()
    {
        if (isReady)
        {
            isReady = false;
            count++;
            image.sprite = sprites[count];
            if (count >= CountToActivate)
            {
                PredictionActivate();
            }
        }
    }
    override public void PredictionActivate()
    {
        EventManager.instance.ObjectsActivated?.Invoke(this);
    }
    override public async Task Activate()
    {
        List<Task> tasks = new();
        Quaternion rotation = transform.rotation;
        Tween shake = transform.DOPunchRotation(new Vector3(0, 0, 15), 5, 2)
    .SetEase(Ease.OutSine); ;
        Tween scale = transform.DOScale(1.2f, 0.2f)
            .SetEase(Ease.OutElastic);
        foreach (GridCell cell in cells)
        {
            tasks.Add(cell.DestroyGridObject(transform));
        }
        await Task.WhenAll(tasks);
        shake.Kill();
        scale.Kill(); 
        transform.localScale = Vector3.one;
        transform.rotation = rotation;
        count = 0;
        image.sprite = sprites[count];
    }
    public override async Task Destroy(Action callback, Transform target)
    {
        PredictionActivate();
    }
    public override void Clear()
    {
        GameObject.Destroy(gameObject);
    }
}
