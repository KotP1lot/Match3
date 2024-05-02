using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BonusGem : Gem
{
    [SerializeField]protected BGSO bgSo;
    protected BGLVLInfo lvl_bgInfo;
    protected GemType gemType;
    protected List<GridCell> cells;
    protected GridCell cell;
    public bool isActivated;
    public void Setup(GemType gemType, int lvl = 0)
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
}
