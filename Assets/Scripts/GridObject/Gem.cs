using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Gem : GridObject
{
    public Action<Gem> OnGemDestroy;
    public Action<Gem> OnGemDeactivate;
    [SerializeField] GameObject activeIndicator;
    [SerializeField] GameObject arrow;
    public bool isActive = false;
    virtual public void Setup(GridObjectSO objectInfo)
    {
        Info = objectInfo;
        GetComponent<Image>().sprite = Info.Sprite;
    }
    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        activeIndicator.SetActive(isActive);
    }
    public void SetArrowDir(bool isActive, float z)
    {
        arrow.SetActive(isActive);
        if (!isActive) return;
        arrow.transform.DORotate(new Vector3(0, 0, z), 0f);
    }
    override public void Destroy()
    {
        OnGemDestroy?.Invoke(this);
        EventManager.instance.OnGemDestroy?.Invoke(this);
        DeactivateGem();
    }
    override public void Clear()
    {
        DeactivateGem();
    }
    private void DeactivateGem()
    {
        SetActive(false);
        transform.SetParent(null);
        OnGemDeactivate?.Invoke(this);
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector2.one;
    }
    public GemType GetGemType() => Info.Type;
    public int GetScore() => Info.Score;
}
