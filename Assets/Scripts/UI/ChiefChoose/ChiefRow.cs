using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChiefRow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    [SerializeField] new TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI desc;
    [SerializeField] db_BGemSo bg;
    ChiefPlayerData data;
    ChiefSO chiefSO;

    ChiefLvlInfo lvl_Info;
    public event Action<ChiefPlayerData> OnRowClick;

    public void Setup(ChiefPlayerData data) 
    {
        this.data = data;
        chiefSO = data.chief;
        lvl_Info = data.chief.GetLvlInfo(data.lvl); // HARD CODE
        image.sprite = chiefSO.sprite;
        name.text = data.chief.name;
        string description = $"�����: <b>{lvl_Info.yumyBonus}</b>\n" +
            $"��: <b>{bg.GetBGByType(chiefSO.bgType).bgName}</b>\n" +
            $"������� ��'������: <b>{lvl_Info.countToUltimate}</b>";
        desc.text = description;  
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRowClick?.Invoke(data);
    }

}
