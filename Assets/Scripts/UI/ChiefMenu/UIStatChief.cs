using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIStatChief : MonoBehaviour
{
    public Action OnStatClose;
    private ChiefPlayerData data;
    [SerializeField] db_Chief db;
    [SerializeField] db_GemSO gemSO;
    [SerializeField] db_BGemSo bGSO;
  
    [SerializeField] PlayerWalletSO wallet;

    [SerializeField] TextMeshProUGUI lvlTxt;
    [SerializeField] LocalizeStringEvent nameTxt;
    [SerializeField] TextMeshProUGUI bonusTxt;
    [SerializeField] LocalizeStringEvent ultimateNameInfoTxt;
    [SerializeField] LocalizeStringEvent ultimateInfoTxt;
    [SerializeField] LocalizeStringEvent newLvlTxt;
    [SerializeField] TextMeshProUGUI newLvl;
    [SerializeField] TextMeshProUGUI costTxt;
    

    [SerializeField] Image foto;
    [SerializeField] Image bonusImg;
    [SerializeField] Image ultimateImg;

    [SerializeField] Button btn;
    public void Start()
    {
        newLvlTxt.StringReference.StringChanged += (string t) => newLvl.text = t; 
    }
    public void Setup(ChiefPlayerData data) 
    {
        this.data = data;
        ChangeVisual();
    }
    public void LvlUp() 
    {
        ChiefSO chief = data.chief;
        ChiefLvlInfo lvlInfo = chief.GetLvlInfo(data.lvl+1);
        if (lvlInfo == null) 
        {
            return;
        }
        if (wallet.Money.Amount >= lvlInfo.lvlCost) 
        {
            wallet.Money.Spend(lvlInfo.lvlCost);
            db.UpdateChiefData(data.chief, data.lvl + 1);
            Debug.Log(data.lvl);
            ChangeVisual();
        }
    }
    public void ChangeVisual() 
    {
        ChiefSO chief = data.chief;
        ChiefLvlInfo currlvlInfo = chief.GetLvlInfo(data.lvl);
        lvlTxt.text = data.lvl.ToString();
        nameTxt.SetEntry(chief.name);
        bonusTxt.text = currlvlInfo.yumyBonus.ToString();
        ultimateNameInfoTxt.SetEntry(chief.bgType.ToString());
        ultimateInfoTxt.SetEntry(chief.bgType switch {
            BGType.saucepan=> "saucepanInfo",
            BGType.box => "boxInfo",
            BGType.vertical => "verticalInfo",
            BGType.horizontal => "horizontalInfo"
        });
       
        bonusImg.sprite = gemSO.GetGemSOByType(chief.gemType).sprite;
        if (chief.bgType == BGType.horizontal)
        {
            ultimateImg.transform.DORotate(new Vector3(0, 0, 90),0);
        }
        else 
        {
            ultimateImg.transform.rotation = Quaternion.identity;
        }
        ultimateImg.sprite = bGSO.GetBGByType(chief.bgType).GetSprite(chief.gemType);
        foto.sprite = data.chief.sprite;
        if (chief.GetLvlInfo(data.lvl + 1) == null)
        {
            btn.interactable = false;
            costTxt.text = "---";
            newLvlTxt.SetEntry("maxLvl");
        }
        else
        {
            ChiefLvlInfo nextlvlInfo = chief.GetLvlInfo(data.lvl + 1);
            costTxt.text = nextlvlInfo.lvlCost.ToString();
            newLvlTxt.StringReference.Arguments = new object[] { nextlvlInfo.yumyBonus };
            newLvlTxt.RefreshString();
            if (wallet.Money.Amount < nextlvlInfo.lvlCost) 
            {
                btn.interactable = false;
            }
            else 
            {
                btn.interactable= true;
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(newLvlTxt.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
    }
    private void OnDisable()
    {
        OnStatClose?.Invoke();
    }
}
