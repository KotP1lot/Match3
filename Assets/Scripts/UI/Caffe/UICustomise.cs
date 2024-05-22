using System.Collections.Generic;
using UnityEngine;

public class UICustomise : MonoBehaviour
{
    [SerializeField] List<UICustomBtn> images;
    [SerializeField] UICaffeStat stat;
    private int current;
    private void Start()
    {
        foreach (var btn in images) 
        {
            btn.OnBtnClick += OnBtnClick;
        }
    }
    public void Setup(InteriorLvlInfo lvlInfo, int current) 
    {
        this.current = current;
        for (int i =0; i<3;i++) 
        {
            images[i].Setup(lvlInfo.sprites[i], i == current);
        }
    }
    public void ChangeCurr() 
    {
        stat.ChangeCurr(current);
    }
    private void OnBtnClick(UICustomBtn btn) 
    {
        current = btn.curr;
        foreach(var btn2 in images) 
        {
            btn2.SetActive(btn2==btn);
        }
    }
}
