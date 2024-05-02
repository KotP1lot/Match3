using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] db_FloorSO db;
    [SerializeField] Floor prefab;
    GridCell cell;
    Floor floor;

    private void Start()
    {
        cell = GetComponent<GridCell>();
    }
    public void AddFloor(FloorType type)
    {
        if (floor != null) return;
        FloorSO so = db.GetByType(type);
        floor = Instantiate(prefab, cell.transform);
        floor.transform.SetAsFirstSibling();
        floor.Setup(so);
        floor.Subcribe(cell);
        floor.OnFloorDestroy += OnFloorDestroyHandler;
    }
    private void OnFloorDestroyHandler()
    {
        floor = null;
    }
}
