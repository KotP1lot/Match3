using UnityEngine;
using UnityEngine.UI;

public class Interier : MonoBehaviour
{
    [SerializeField] db_Interior db;
    [SerializeField] InteriorSO so;
    [SerializeField] UICaffeStat stat;
    [SerializeField] Image foto;
    [SerializeField] PointManager pointManager;

    public void Awake() 
    {
        db.OnDataUpdate += UpdateVisual;
    }
    private void UpdateVisual() 
    {
        foto = GetComponent<Image>();
        PlayerInteriorData data = db.GetPlayerDataByType(so.type);
        foto.sprite = so.GetLvlInfo(data.lvl).sprites[data.currSprite];
        pointManager.Setup(() => { stat.Setup(data, so); }, () => { }, () => { });
    }
}
