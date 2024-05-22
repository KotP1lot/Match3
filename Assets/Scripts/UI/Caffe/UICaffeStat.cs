using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICaffeStat : MonoBehaviour
{
    [SerializeField] db_Interior db;

    [SerializeField] TextMeshProUGUI lvlTxt;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI infoTxt;
    [SerializeField] TextMeshProUGUI newLvlTxt;
    [SerializeField] TextMeshProUGUI costTxt;
    [SerializeField] UICustomise customise;

    [SerializeField] Button newLvlBtn;

    [SerializeField] Image currFurniture;
    private void Start()
    {
        db.Load();
    }
    public void Setup(InteriorType type) 
    {
        gameObject.SetActive(true);
        PlayerInteriorData data = db.GetPlayerDataByType(type);
        InteriorLvlInfo currInfo = db.GetCurrentInteriorByType(type);
        InteriorLvlInfo nextLVlinfo = db.GetInteriorByType(type, data.lvl);
        currFurniture.sprite = currInfo.sprites[data.lvl];
        nameTxt.text = type.ToString();
        int diff = nextLVlinfo.bonus - currInfo.bonus;
        newLvlTxt.text = $"+ {diff} Бонусу!";
        costTxt.text = nextLVlinfo.cost.ToString();
        customise.Setup(currInfo);
    }
}
