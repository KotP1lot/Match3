using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BonusGem : Gem
{
    [SerializeField]protected BGSO bgInfo;
    protected GemType gemType;
    protected List<GridCell> cells;
    protected GridCell cell;
    public bool isActivated;
    public void Setup(GemType gemType)
    {
        this.gemType = gemType;
        SetupUI(bgInfo.GetSpriteByType(gemType));
    }
    override public Tween SetGridCoord(GridCell gridCell)
    {
        cell = gridCell;
        return base.SetGridCoord(gridCell);
    }
    public override int GetScore() => bgInfo.score;
    public bool IsMoveWithLine() => bgInfo.isMoveWithLine;
    public override GemType GetGemType() => gemType;
}
