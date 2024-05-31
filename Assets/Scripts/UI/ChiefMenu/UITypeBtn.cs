using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UITypeBtn : MonoBehaviour, IPointerClickHandler
{

    public event Action<UITypeBtn> OnTypeBtnClick;
    public GemType gemType;
    [SerializeField] Image tab;
    [SerializeField] Image tabImg;
    [SerializeField] List<ColorType> colorTypes;
    [Serializable]
    public struct ColorType 
    {
        public GemType type;
        public Sprite sprite;
    }
    public void Setup(GemType type)
    {
        gemType = type;
        tabImg.sprite = colorTypes.Find(x => x.type == gemType).sprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnTypeBtnClick?.Invoke(this);
    }
    public void SetActive(bool isActive) 
    {
        RectTransform rectTransform = tab.GetComponent<RectTransform>();
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, isActive?-30:0);
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, isActive ? 30 : 0);
    }
}
