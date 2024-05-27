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
        if (Game.gameStat == null || Game.gameStat.lvl.unlockChief == null) return;
        all.gameObject.SetActive(true);
        ChiefSO chief = Game.gameStat.lvl.unlockChief;
        ChiefLvlInfo currlvlInfo = chief.GetLvlInfo(0);
        nameTxt.text = chief.name;
        bonusTxt.text = currlvlInfo.yumyBonus.ToString();
        ultimateNameInfoTxt.text = bGSO.GetBGByType(chief.bgType).bgName;
        ultimateInfoTxt.text = bGSO.GetBGByType(chief.bgType).describe;

        bonusImg.sprite = gemSO.GetGemSOByType(chief.gemType).sprite;
        ultimateImg.sprite = bGSO.GetBGByType(chief.bgType).GetSprite(chief.gemType);
        foto.sprite = chief.sprite;
    }
}
