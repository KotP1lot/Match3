using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Gem : GridObject
{
    public GemSO info;
    public Action<Gem> OnGemDestroy;
    public Action<Gem> OnGemDeactivate;
    private GameObject activeIndicator;
    protected GameObject arrow;
    public bool isActive = false;
    private void Awake()
    {
       if(info != null) SetupUI(info.sprite); 
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
    override public async Task Destroy(Action callback, Transform target)
    {
        Deactivate();
        await MoveTo(target).AsyncWaitForCompletion();
        OnGemDestroy?.Invoke(this);
        EventManager.instance.OnGemDestroy?.Invoke(this);
        DeactivateGem();
        callback();
    }
    override public void Clear()
    {
        DeactivateGem();
    }
    protected void DeactivateGem() 
    {
        OnGemDeactivate?.Invoke(this);
    }
    virtual protected void Deactivate()
    {
        arrow.SetActive(false);
        SetActive(false);
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector2.one;
        transform.localRotation = Quaternion.identity;
    }
    public Tween MoveTo(Transform target)
    {
        ChangeCanvasLayout(2);
        return transform.DOMove(target.position, 0.5f);
    }
    public void ChangeCanvasLayout(int layout) 
    {
        GetComponent<Canvas>().sortingOrder = layout;
    }
    virtual public GemType GetGemType() => info.type;
    virtual public int GetScore() => info.score;

}
