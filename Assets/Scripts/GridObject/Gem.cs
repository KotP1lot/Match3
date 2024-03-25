using System;
using UnityEngine;

public class Gem : GridObject
{
    public Action<Gem> OnGemDestroy;
    [SerializeField] GameObject activeIndicator;
    [SerializeField] GameObject predictIndicator;
    public bool isActive = false;
    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        activeIndicator.SetActive(isActive);
    }
    override public void Destroy()
    {
        SetActive(false);
        transform.SetParent(null);
        OnGemDestroy?.Invoke(this);
        EventManager.instance.OnGemDestroy?.Invoke(this);
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector2.one;
    }
}
