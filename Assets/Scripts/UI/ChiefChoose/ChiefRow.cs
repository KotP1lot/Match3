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

    public void Setup(ChiefSO chief, int lvl = 0) 
    {
        chiefSO = chief;
        lvl_Info = chief.GetLvlInfo(lvl);
        image.sprite = lvl_Info.sprite;
        name.text = chief.name;
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
