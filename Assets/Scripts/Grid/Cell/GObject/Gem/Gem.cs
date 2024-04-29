using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Gem : GridObject
{
    [SerializeField]private GemSO info;
    public Action<Gem> OnGemDestroy;
    public Action<Gem> OnGemDeactivate;
    private GameObject activeIndicator;
    private GameObject arrow;
    public bool isActive = false;
    private void Awake()
    {
       if(info is not null) SetupUI(info.sprite); 
    }
    public void Setup(GemSO objectInfo)
    {
        info = objectInfo;
        SetupUI(info.sprite);
        IsAffectedByGravity = true;
    }
    protected void SetupUI(Sprite sprite) 
    {
        UIGem ui = GetComponent<UIGem>();
        activeIndicator = ui.activeIndicator;
        arrow = ui.arrow;
        GetComponent<Image>().sprite = sprite;
    }
    public virtual void SetActive(bool isActive)
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
    override public bool Destroy()
    {
        OnGemDestroy?.Invoke(this);
        EventManager.instance.OnGemDestroy?.Invoke(this);
        DeactivateGem();
        return true;
    }
    override public void Clear()
    {
        DeactivateGem();
    }
    protected void DeactivateGem()
    {
        OnGemDeactivate?.Invoke(this);
        arrow.SetActive(false);
        SetActive(false);
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector2.one;
    }
    virtual public GemType GetGemType() => info.type;
    virtual public int GetScore() => info.score;

}
