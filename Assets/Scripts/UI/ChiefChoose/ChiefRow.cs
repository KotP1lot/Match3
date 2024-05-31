using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class ChiefRow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    [SerializeField] new LocalizeStringEvent name;
    [SerializeField] LocalizeStringEvent bonus;
    [SerializeField] LocalizeStringEvent ulimate;
    [SerializeField] LocalizeStringEvent count;
    [SerializeField] TextMeshProUGUI bonusTxt;
    [SerializeField] TextMeshProUGUI countTxt;
    [SerializeField] db_BGemSo bg;
    ChiefPlayerData data;
    ChiefSO chiefSO;

    ChiefLvlInfo lvl_Info;
    public event Action<ChiefPlayerData> OnRowClick;
    private void Start()
    {
        bonus.StringReference.StringChanged += (string t) => bonusTxt.text = t;
        count.StringReference.StringChanged += (string t) => countTxt.text = t;
    }
    public void Setup(ChiefPlayerData data) 
    {
        this.data = data;
        chiefSO = data.chief;
        lvl_Info = data.chief.GetLvlInfo(data.lvl); // HARD CODE
        image.sprite = chiefSO.sprite;
        name.SetEntry(data.chief.name);
        ulimate.SetEntry(data.chief.bgType.ToString());
        bonus.StringReference.Arguments = new object[] { lvl_Info.yumyBonus };
        bonus.RefreshString();
        count.StringReference.Arguments = new object[] { lvl_Info.countToUltimate };
        count.RefreshString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRowClick?.Invoke(data);
    }

}
