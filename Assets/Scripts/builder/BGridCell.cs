using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class BGridCell : MonoBehaviour, IPointerClickHandler
{
    public event Action<BGridCell> OnBGCClick;
    public GridObject gridObjectPrefab { get; private set; }
    private GridObject gridObject;
    public void SetGridObject(GridObject gridObjectPrefab) 
    {
        if (gridObjectPrefab is null)
        {

            Clear(); 
            return;
        }
        Clear();
        this.gridObjectPrefab = gridObjectPrefab;
        gridObject = Instantiate(gridObjectPrefab, transform);
    }
    public void Clear() 
    {
        this.gridObjectPrefab = null;
        if (gridObject != null)
        {
            Destroy(gridObject.gameObject);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnBGCClick?.Invoke(this);
    }
}
