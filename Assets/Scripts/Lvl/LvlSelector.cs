using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlSelector : MonoBehaviour
{
    [SerializeField] LvlBtn prefab;
    [SerializeField] LvlBtn[] lvls;
    [SerializeField] db_LvlSo db;
    [SerializeField] EnergyManager energyManager;
    [SerializeField] Image recoveryEnergy;
    [SerializeField] Image monthImg;
    [SerializeField] UIStatLvl stat;
    [SerializeField] TextMeshProUGUI monthTxt;
    [SerializeField] List<MonthInfo> monthInfo;

    [SerializeField] Button left;
    [SerializeField] Button right;
    private MonthType[] monthTypes = (MonthType[]) Enum.GetValues(typeof(MonthType));
    private MonthType curMonth;
    private int curMonthID;
    public static LvlPlayerData LvL;

    void Start()
    {
        curMonthID = 0;
        ChangeMonth(0);
        Subscribe();
    }

    private void Subscribe()
    {
        foreach (LvlBtn lvl in lvls)
        {
            lvl.OnLvlClick += OnLvlClick;
        }
    }
    //private void OnEnable()
    //{
    //    Subscribe();
    //}
    //private void OnDisable()
    //{
    //    foreach (LvlBtn lvl in lvls)
    //    {
    //        lvl.OnLvlClick -= OnLvlClick;
    //    }
    //}
    private void OnLvlClick(LvlPlayerData data)
    {
        stat.Setup(data);
        LvL = data;
    }
    private void ChangeMonthInfo() 
    {
        curMonth = monthTypes[curMonthID];
     
        List<LvlPlayerData> lvlPlayerDatas = db.GetLvlsByMonth(curMonth);
        MonthInfo curInfo = monthInfo.Find(x => x.month == curMonth);
        monthImg.sprite = curInfo.sprite;
        monthTxt.text = curInfo.monthName;
        for (int i = 0, j = 0; i < 35; i++)
        {
            if (i < curInfo.startDay || j >= curInfo.days)
            {
                lvls[i].Setup(null);
                lvls[i].OnLvlClick += OnLvlClick;
                continue;
            }
            lvls[i].Setup(lvlPlayerDatas[j]);
            j++;
        }
    }
    public void ChangeMonth(int i) 
    {
        curMonthID += i;
        if (curMonthID == 0)
        {
            left.interactable = false;
        }
        else if (curMonthID == monthTypes.Length - 1)
        {
            right.interactable = false;
        }
        else 
        {
            left.interactable = true;
            right.interactable = true;
        }
        ChangeMonthInfo();
    }
    [Serializable]
    struct MonthInfo 
    {
        public MonthType month;
        public int days;
        public int startDay;
        public Sprite sprite;
        public string monthName;
    }

}
