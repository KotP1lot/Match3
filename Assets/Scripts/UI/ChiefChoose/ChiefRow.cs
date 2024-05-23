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
        string description = $"Bonus: {lvl_Info.yumyBonus}\n" +
            $"Ultimative: {chiefSO.bgType}\n" +
            $"Count to charge: {lvl_Info.countToUltimate}";
        desc.text = description;  
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRowClick?.Invoke(data);
    }

}
