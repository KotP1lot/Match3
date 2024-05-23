using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI bonusTxt;
    [SerializeField] TextMeshProUGUI ultimateNameTxt;
    [SerializeField] TextMeshProUGUI ultimateNameInfoTxt;
    [SerializeField] TextMeshProUGUI ultimateInfoTxt;
    [SerializeField] TextMeshProUGUI newLvlTxt;
    [SerializeField] TextMeshProUGUI costTxt;

    [SerializeField] Image foto;
    [SerializeField] Image bonusImg;
    [SerializeField] Image ultimateImg;

    [SerializeField] Button btn;
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
        lvlTxt.text = currlvlInfo.lvl.ToString();
        nameTxt.text = chief.name;
        bonusTxt.text = currlvlInfo.yumyBonus.ToString();
        ultimateNameTxt.text = chief.bgType.ToString();
        ultimateNameInfoTxt.text = chief.bgType.ToString();
        ultimateInfoTxt.text = "----";
       
        bonusImg.sprite = gemSO.GetGemSOByType(chief.gemType).sprite;
        ultimateImg.sprite = bGSO.GetBGByType(chief.bgType).GetSprite(chief.gemType);
        foto.sprite = data.chief.sprite;
        if (chief.GetLvlInfo(data.lvl + 1) == null)
        {
            btn.interactable = false;
            costTxt.text = "---";
            newLvlTxt.text = "Максимальний рівень досягнуто";
        }
        else
        {
            ChiefLvlInfo nextlvlInfo = chief.GetLvlInfo(data.lvl + 1);
            costTxt.text = nextlvlInfo.lvlCost.ToString();
            int diff = nextlvlInfo.yumyBonus - currlvlInfo.yumyBonus;
            newLvlTxt.text = $"+{diff} до Бонусу!";
            if (wallet.Money.Amount < nextlvlInfo.lvlCost) 
            {
                btn.interactable = false;
            }
            else 
            {
                btn.interactable= true;
            }
        }
    }
    private void OnDisable()
    {
        OnStatClose?.Invoke();
    }
}
