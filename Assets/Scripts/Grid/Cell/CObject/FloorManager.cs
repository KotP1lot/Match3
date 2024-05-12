using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] db_FloorSO db;
    [SerializeField] Floor prefab;
    GridCell cell;
    Floor floor;
    public FloorType type;

    private void Awake()
    {
        cell = GetComponent<GridCell>();
    }
    public void AddFloor(FloorType type, bool needSubcribe = true)
    {
        if (floor != null) return;
        this.type = type;
        if (type == FloorType.none) return;
        FloorSO so = db.GetByType(type);
        floor = Instantiate(prefab, transform);
        floor.transform.SetAsFirstSibling();
        floor.Setup(so);
        if (needSubcribe)
        {
            floor.Subcribe(cell);
            floor.OnFloorDestroy += OnFloorDestroyHandler;
        }
    }
    public void RemoveFloor() 
    {
        Destroy(floor.gameObject);
        floor = null;
        type = FloorType.none;
    }
    private void OnFloorDestroyHandler()
    {
        floor = null;
    }
}
