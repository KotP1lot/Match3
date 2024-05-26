using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyableObject : ActivableObject
{
    [SerializeField] List<Sprite> allState = new List<Sprite>();
    [SerializeField] Image image;
    [SerializeField] RectTransform inside;
    [SerializeField] Image fon;
    [SerializeField] bool existAfterDestroy = false;
    public override Tween Spawn(Transform spawnPos, GridCell gridCell)
    {
        image = GetComponent<Image>();
        CountToActivate = allState.Count-1;
        image.sprite = allState[CountToActivate];
        return base.Spawn(spawnPos, gridCell);
    }
    public override void Setup()
    {
        isReady = true;
        fon.enabled = true;
        CountToActivate = allState.Count-1;
        image.sprite = allState[CountToActivate];
    }
    override public void OnGemsDestroyInNeighboringCells()
    {
        if (isReady)
        {
           CountToActivate--;
            isReady = false;
            if (CountToActivate >= 0)
            {
                Debug.Log("afasf");
                image.sprite = allState[CountToActivate];
            }
            if (CountToActivate <= 0)
            {
                if (!existAfterDestroy)
                {
                    cell.Clear();
                }
                inside.DOAnchorPosY(50, 2).SetEase(Ease.OutElastic).OnComplete(()=>Destroy(inside.gameObject));
                fon.sprite = allState[0];
                image.gameObject.SetActive(false); 
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
