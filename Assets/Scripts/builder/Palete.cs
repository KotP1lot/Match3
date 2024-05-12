using System;
using UnityEngine;

public class Palete : MonoBehaviour
{
    [SerializeField] PaleteCell cellPref;
    [SerializeField] PaleteExtraItem cellExtraPref;
    [SerializeField] GridObject[] gridObjects;
    [SerializeField] Gem gemPref;
    [SerializeField] BuilderManager builderManager;

    private void Start()
    {
        SetGridObjects();
        SetGem();
        SetFloors();
        SetBorders();
    }
    private void SetGridObjects() 
    {
        foreach (GridObject gridObject in gridObjects)
        {
            PaleteCell cell = Instantiate(cellPref, transform);
            cell.Setup((pref) =>
            {
                builderManager.ChoosePref(pref.gridObjectPrefab);
            }, gridObject);
        }
        PaleteCell clear = Instantiate(cellPref, transform);
        clear.SetupClear((pref) =>
        {
            builderManager.Clear();
        });
    }
    private void SetGem()
    {
        GemType[] floors = (GemType[])Enum.GetValues(typeof(GemType));
        foreach (GemType value in floors)
        {
            PaleteCell cell = Instantiate(cellPref, transform);
            cell.Setup((pref) =>
            {
                builderManager.ChoosePref(pref.gridObjectPrefab);
                builderManager.gemType = value;
            }, gemPref, value);
        }
    }
    private void SetFloors() 
    {
        FloorType[] floors = (FloorType[])Enum.GetValues(typeof(FloorType));
        foreach (FloorType value in floors)
        {
            if (value == FloorType.none) continue;
            PaleteExtraItem cell = Instantiate(cellExtraPref, transform);
            cell.Setup((item) =>
            {
                builderManager.floorType =item.floorType;
            }, value);
        }
    }
    private void SetBorders() 
    {
        BorderType[] borders = (BorderType[])Enum.GetValues(typeof(BorderType));
        foreach (BorderType value in borders)
        {
            PaleteExtraItem cell = Instantiate(cellExtraPref, transform);
            cell.Setup((item) =>
            {
                builderManager.borderType = item.borderType;
            }, value);
        }
    }
}
