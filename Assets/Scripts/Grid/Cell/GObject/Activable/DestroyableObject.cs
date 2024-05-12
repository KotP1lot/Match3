using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyableObject : ActivableObject
{
    [SerializeField] List<Sprite> allState = new List<Sprite>();
    private Image image;
    [SerializeField] bool existAfterDestroy = false;
    public override Tween Spawn(Transform spawnPos, GridCell gridCell)
    {
        image = GetComponent<Image>();
        CountToActivate = allState.Count;
        image.sprite = allState[CountToActivate - 1];
        return base.Spawn(spawnPos, gridCell);
    }
    public override void Setup()
    {

        isReady = true;
        image = GetComponent<Image>();
        CountToActivate = allState.Count;
        image.sprite = allState[CountToActivate - 1];
    }
    override public void OnGemsDestroyInNeighboringCells()
    {
        if (isReady)
        {
            CountToActivate--;
            isReady = false;
            if (CountToActivate - 1 >= 0)
            {
                image.sprite = allState[CountToActivate - 1];
            }
            if (CountToActivate <= 0)
            {
                if (!existAfterDestroy)
                {
                    cell.Clear();
                }
                Unsubcribe();
            }
        }
    }
    public override void Clear()
    {
        Unsubcribe();
        GameObject.Destroy(gameObject);
    }
}
