using System.Collections.Generic;
using UnityEngine;
public class LineDestroyer : ActivableObject
{
    [SerializeField] bool isHorizontal = true;
    [SerializeField] bool isTwoWay = true;
    [SerializeField] bool isRight_Upward = true;
    private List<GridCell> cells;

    public void Start()
    {
        predictCount = 0;
        Setup(objectSO);
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
        if (!isPredicted)
        {
            predictCount++;
            if (predictCount >= CountToActivate)
            {
                isPredicted = true;
                PredictionActivate();
            }
        }
    }
    override public void PredictionActivate()
    {
        EventManager.instance.ObjectsActivatedEvent?.Invoke(this);
    }
    override public void Activate()
    {
        if (isPredicted) 
        {
            predictCount = 0;
            isPredicted = false;
            foreach (GridCell cell in cells) 
            {
                cell.DestroyGridObject();
            }
        }
    }

    public override void Destroy()
    {
       
    }
}
