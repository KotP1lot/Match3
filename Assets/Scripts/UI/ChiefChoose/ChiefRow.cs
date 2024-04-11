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
    public event Action<ChiefSO> OnRowClick;

    public void Setup(ChiefSO chief) 
    {
        chiefSO = chief;
        image.sprite = chief.sprite;
        name.text = chief.name;
        string description = $"Bonus: {chiefSO.yumyBonus}\n" +
            $"Ultimative: {chiefSO.bgType}\n" +
            $"Count to charge: {chiefSO.countToUltimate}";
        desc.text = description;  
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRowClick?.Invoke(chiefSO);
    }

}
