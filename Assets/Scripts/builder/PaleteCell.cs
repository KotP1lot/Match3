using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaleteCell : MonoBehaviour, IPointerClickHandler
{
    private Action<GridObject> func;
    public GridObject gridObjectPrefab { get; private set; }
    public void Setup(Action<GridObject> func, GridObject gridObjectPrefab)
    {
        this.func= func;
        this.gridObjectPrefab = gridObjectPrefab;
        Instantiate(gridObjectPrefab, transform);
    }
    public void SetupClear(Action<GridObject> func) 
    {
        this.func = func;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        func(gridObjectPrefab);
    }
}
