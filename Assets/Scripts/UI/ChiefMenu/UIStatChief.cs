using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatChief : MonoBehaviour
{
    private ChiefPlayerData data;
    [SerializeField] db_Chief db;
    [SerializeField] PlayerWalletSO wallet;
    [SerializeField] TextMeshProUGUI lvl;
    [SerializeField] Image image;
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
            ChangeVisual();
        }
    }
    public void ChangeVisual() 
    {
        ChiefSO chief = data.chief;
        ChiefLvlInfo lvlInfo = chief.GetLvlInfo(data.lvl + 1);
        if (lvlInfo == null)
        {
            btn.gameObject.SetActive(false);
        }
        lvl.text = data.lvl.ToString();
        image.sprite = data.chief.GetLvlInfo(data.lvl).sprite;
    }
}
