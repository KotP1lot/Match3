using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class BGridCell : MonoBehaviour, IPointerClickHandler
{
    public event Action<BGridCell> OnBGCClick;
    public GridObject gridObjectPrefab { get; private set; }
    [SerializeField] db_GemSO db_GemSO;
    private GridObject gridObject;
    private GemType gemType;
    private BorderManager borderManager;
    private FloorManager floorManager;
    private void Start() 
    {
        borderManager = GetComponent<BorderManager>();
        floorManager = GetComponent<FloorManager>();
    }
    public void AddBorder(Direction dir, BorderType type)
    {
        borderManager.AddBorder(dir, type, false);
    }
    public void RemoveBorder(Direction dir)
    {
        borderManager.RemoveBorder(dir);
    }
    public void AddFloor(FloorType type)
    {
        floorManager.AddFloor(type, false);
    }
    public void RemoveFloor()
    {
        floorManager.RemoveFloor();
    }
    public void Setup(CelLLvlInfo info) 
    {
        SetGridObject(info.prefab, info.gemType);
        AddFloor(info.floorType);
        if (info.borders.Length > 0) 
        {
            foreach(var borderInfo in info.borders) 
            {
                AddBorder(borderInfo.direction, borderInfo.type);
            }
        }
    }
    public void SetGridObject(GridObject gridObjectPrefab, GemType gemType) 
    {
        if (gridObjectPrefab == null)
        {
            Clear(); 
            return;
        }
        Clear();
        this.gridObjectPrefab = gridObjectPrefab;
        gridObject = Instantiate(gridObjectPrefab, transform);
        if (gridObject is Gem gem) 
        {
            gem.Setup(db_GemSO.GetGemSOByType(gemType));
            this.gemType = gem.GetGemType();
        }
    }
    public void Clear() 
    {
        gridObjectPrefab = null;
        if (gridObject != null)
        {
            Destroy(gridObject.gameObject);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnBGCClick?.Invoke(this);
    }
    public CelLLvlInfo GetCellInfo() 
    {
        CelLLvlInfo info = new()
        {
            floorType = floorManager.type,
            borders = borderManager.GetBorderInfo(),
            prefab = gridObjectPrefab,
            gemType = gemType,
        };
        return info; 
    }
}
