using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class LineDestroyer : ActivableObject
{
    [SerializeField] bool isHorizontal = true;
    [SerializeField] bool isTwoWay = true;
    [SerializeField] bool isRight_Upward = true;
    private List<GridCell> cells;
    override public void Setup()
    {
        isReady = true;
        count = 0;
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
        Debug.Log("Tik-Line");
        if (isReady)
        {
            isReady = false;
            count++;
            if (count >= CountToActivate)
            {
                PredictionActivate();
            }
            Debug.Log("Tack");
        }
    }
    override public void PredictionActivate()
    {
        EventManager.instance.ObjectsActivated?.Invoke(this);
    }
    override public async Task Activate()
    {
        List<Task> tasks = new();
        foreach (GridCell cell in cells)
        {
            tasks.Add(cell.DestroyGridObject(transform));
        }
        await Task.WhenAll(tasks);
        count = 0;
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
