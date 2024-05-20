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
    ChiefSO chiefSO;
    ChiefLvlInfo lvl_Info;
    public event Action<ChiefSO> OnRowClick;

    public void Setup(ChiefPlayerData data) 
    {
        chiefSO = data.chief;
        lvl_Info = data.chief.GetLvlInfo(data.lvl-1); // HARD CODE
        image.sprite = lvl_Info.sprite;
        name.text = data.chief.name;
        string description = $"Bonus: {lvl_Info.yumyBonus}\n" +
            $"Ultimative: {chiefSO.bgType}\n" +
            $"Count to charge: {lvl_Info.countToUltimate}";
        desc.text = description;  
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRowClick?.Invoke(chiefSO);
    }

}
