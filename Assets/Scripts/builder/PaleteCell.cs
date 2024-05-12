using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaleteCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] db_GemSO db_GemSO;
    private Action<PaleteCell> func;
    public GemType gemType { get; private set; }
    public Gem gem { get; private set; }
    public GridObject gridObjectPrefab { get; private set; }

    public void Setup(Action<PaleteCell> func, GridObject gridObjectPrefab)
    {
        this.func= func;
        this.gridObjectPrefab = gridObjectPrefab;
        Instantiate(gridObjectPrefab, transform);
    }
    public void Setup(Action<PaleteCell> func, Gem gemPref, GemType gemType)
    {
        this.func = func;
        this.gridObjectPrefab = gemPref;
        gem = Instantiate(gemPref, transform);
        gem.Setup(db_GemSO.GetGemSOByType(gemType));
    }

    public void SetupClear(Action<PaleteCell> func) 
    {
        this.func = func;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        func(this);
    }
}
