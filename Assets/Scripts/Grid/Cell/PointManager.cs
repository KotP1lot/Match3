using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointManager : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public Action Down;
    public Action Enter;
    public Action Up;
    public void Setup(Action down, Action enter, Action up) 
    {
        Down = down;
        Enter = enter;
        Up = up;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Gay");
        Down?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Enter?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Up?.Invoke();
    }
}
