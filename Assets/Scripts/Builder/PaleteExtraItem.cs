using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaleteExtraItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI text;
    private Action<PaleteExtraItem> func;
    public BorderType borderType { get; private set; }
    public FloorType floorType { get; private set; }
    public void Setup(Action<PaleteExtraItem> func, BorderType type)
    {
        borderType = type;
        text.text = $"Border. {type}";
        this.func = func; 
    }
    public void Setup(Action<PaleteExtraItem> func, FloorType type)
    {
        floorType = type;
        text.text = $"Floor. {type}";
        this.func = func;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        func(this);
    }
}
