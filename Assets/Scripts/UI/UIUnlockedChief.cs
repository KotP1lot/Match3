using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIUnlockedChief : MonoBehaviour
{
    [SerializeField] db_GemSO gemSO;
    [SerializeField] db_BGemSo bGSO;

    [SerializeField] LocalizeStringEvent nameTxt;
    [SerializeField] TextMeshProUGUI bonusTxt;
    [SerializeField] LocalizeStringEvent ultimateNameInfoTxt;
    [SerializeField] LocalizeStringEvent ultimateInfoTxt;

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
        nameTxt.SetEntry(chief.name);
        bonusTxt.text = currlvlInfo.yumyBonus.ToString();
        ultimateNameInfoTxt.SetEntry(chief.bgType.ToString());
        ultimateInfoTxt.SetEntry(chief.bgType switch
        {
            BGType.saucepan => "saucepanInfo",
            BGType.box => "boxInfo",
            BGType.vertical => "verticalInfo",
            BGType.horizontal => "horizontalInfo"
        });
        if (chief.bgType == BGType.horizontal)
        {
            ultimateImg.transform.DORotate(new Vector3(0, 0, 90), 0);
        }
        else
        {
            ultimateImg.transform.rotation = Quaternion.identity;
        }
        bonusImg.sprite = gemSO.GetGemSOByType(chief.gemType).sprite;
        ultimateImg.sprite = bGSO.GetBGByType(chief.bgType).GetSprite(chief.gemType);
        foto.sprite = chief.sprite;
    }
}
