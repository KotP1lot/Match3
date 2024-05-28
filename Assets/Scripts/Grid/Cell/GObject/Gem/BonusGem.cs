using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BonusGem : Gem
{
    [SerializeField]protected BGSO bgSo;
    protected BGLVLInfo lvl_bgInfo;
    protected GemType gemType;
    protected List<GridCell> cells;
    protected GridCell cell;
    public bool isActivated;
    virtual public void Setup(GemType gemType, int lvl = 0)
    {
        this.gemType = gemType;
        lvl_bgInfo = bgSo.GetLvlInfo(lvl);
        SetupUI(bgSo.GetSprite(gemType));
    }
    override public Tween SetGridCoord(GridCell gridCell)
    {
        cell = gridCell;
        return base.SetGridCoord(gridCell);
    }
    public override int GetScore() => lvl_bgInfo.score;
    public bool IsMoveWithLine() => bgSo.isMoveWithLine;
    public override GemType GetGemType() => gemType;
    protected void Shake() 
    {
        GetComponent<Canvas>().sortingOrder = 3;
        Tween shake = transform.DOPunchRotation(new Vector3(0, 0, transform.rotation.z + 15), 5, 2)
    .SetEase(Ease.OutSine); ;
        Tween scale = transform.DOScale(1.2f, 0.2f)
            .SetEase(Ease.OutElastic);
    }
    protected void BGReset(Quaternion rotation)
    {
        transform.DOKill();
        transform.localScale = Vector3.one;
    }
}
