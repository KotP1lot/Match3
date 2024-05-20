using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITypeBtn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI text;
    public event Action<UITypeBtn> OnTypeBtnClick;
    public GemType gemType;
    [SerializeField] List<ColorType> colorTypes;
    [SerializeField] Image tab;
    [Serializable]
    public struct ColorType 
    {
        public GemType type;
        public Color color;
    }
    public void Setup(GemType type)
    {
        gemType = type;
        tab.color = colorTypes.Find(x=>x.type == type).color;
        text.text = type.ToString();
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
