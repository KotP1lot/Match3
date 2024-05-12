using UnityEngine;

public class LvlSelector : MonoBehaviour
{
    [SerializeField] LvlBtn prefab;
    [SerializeField] db_LvlSo db;
    void Start()
    {
        foreach (LvlSO so in db.lvlSO)
        {
            LvlBtn btn = Instantiate(prefab, transform);
            btn.Setup(so);
        }
    }
}
