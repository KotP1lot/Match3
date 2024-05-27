using UnityEngine;
using UnityEngine.UI;

public class Interier : MonoBehaviour
{
    [SerializeField] db_Interior db;
    [SerializeField] InteriorSO so;
    [SerializeField] UICaffeStat stat;
    [SerializeField] Image foto;
    [SerializeField] PointManager pointManager;
    bool isSubcribe =         false;

    public void Awake() 
    {
        db.OnDataUpdate += UpdateVisual;
        isSubcribe = true;
    }
    private void OnEnable()
    {
       if(!isSubcribe) db.OnDataUpdate += UpdateVisual;
    }
    private void OnDisable()
    {
        db.OnDataUpdate -= UpdateVisual;
        isSubcribe=false;
    }
    private void UpdateVisual() 
    {
        foto = GetComponent<Image>();
        PlayerInteriorData data = db.GetPlayerDataByType(so.type);
        foto.sprite = so.GetLvlInfo(data.lvl).sprites[data.currSprite];
        pointManager.Setup(() => { stat.Setup(data, so); }, () => { }, () => { });
    }
}
