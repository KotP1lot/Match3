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
        if (floor is null) return;
        FloorSO so = db.GetByType(type);
        if (so is not null)
        {
            floor = Instantiate(prefab, cell.transform);
            floor.Setup(so);
            floor.Subcribe(cell);
            floor.OnFloorDestroy += OnFloorDestroyHandler;
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError("Відсутній такий тип у db_BorderSO");
        }
#endif
    }
    private void OnFloorDestroyHandler()
    {
        floor = null;
    }
}
