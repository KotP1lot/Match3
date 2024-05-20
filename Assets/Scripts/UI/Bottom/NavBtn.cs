using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavBtn : MonoBehaviour, IPointerClickHandler
{
    public event Action<NavBtn> OnNavBtnClick;
    [SerializeField] Sprite Active;
    [SerializeField] Sprite None;
    public void SetActive(bool isActive) 
    {
        if (isActive) 
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
            GetComponent<Image>().sprite = Active;
            return;
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(250, 250);
        GetComponent<Image>().sprite = None;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnNavBtnClick?.Invoke(this);
    }
}
