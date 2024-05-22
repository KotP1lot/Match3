using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICustomBtn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image foto;
    public int curr;
    [SerializeField] Image current;
    public event Action<UICustomBtn> OnBtnClick;
    public void Setup(Sprite sprite, bool isActive) 
    {
        foto.sprite = sprite;
        SetActive(isActive);
    }
    public void SetActive(bool isActive) 
    {
        current.gameObject.SetActive(isActive);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnBtnClick?.Invoke(this);
    }
}
