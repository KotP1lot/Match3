using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUnlockedChief : MonoBehaviour
{
    [SerializeField] db_GemSO gemSO;
    [SerializeField] db_BGemSo bGSO;

    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI bonusTxt;
    [SerializeField] TextMeshProUGUI ultimateNameInfoTxt;
    [SerializeField] TextMeshProUGUI ultimateInfoTxt;

    [SerializeField] Image foto;
    [SerializeField] Image bonusImg;
    [SerializeField] Image ultimateImg;
    [SerializeField] Image all;

    void Start()
    {
        Debug.Log("You");
        if (Game.gameStat == null || Game.gameStat.lvl.unlockChief == null) return;
        Debug.Log("why");
        all.gameObject.SetActive(true);
        ChiefSO chief = Game.gameStat.lvl.unlockChief;
        ChiefLvlInfo currlvlInfo = chief.GetLvlInfo(0);
        nameTxt.text = chief.name;
        bonusTxt.text = currlvlInfo.yumyBonus.ToString();
        ultimateNameInfoTxt.text = chief.bgType.ToString();
        ultimateInfoTxt.text = "----";

        bonusImg.sprite = gemSO.GetGemSOByType(chief.gemType).sprite;
        ultimateImg.sprite = bGSO.GetBGByType(chief.bgType).GetSprite(chief.gemType);
        foto.sprite = chief.sprite;
    }
}
