using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChiefChanger : MonoBehaviour
{
    private class RowConroler 
    {
        private List<ChiefRow> rows;
        public RowConroler() 
        {
            rows = new List<ChiefRow>();
        }
        public void AddRow(ChiefRow row) 
        {
            row.gameObject.SetActive(false);
            rows.Add(row);
        }
        public void SetActive(bool isActive) 
        {
            foreach (ChiefRow row in rows) 
            {
                row.gameObject.SetActive(isActive);
            }
        }
        public List<ChiefRow> GetRows() { return rows; }
    }
    [SerializeField] Image empty;
    [SerializeField] db_Chief chiefDB;
    [SerializeField] ChiefRow prefab;
    private Dictionary<GemType, RowConroler> rows = new Dictionary<GemType, RowConroler>();
    public event Action<ChiefPlayerData> OnChiefChoose;
    private ChiefPlayerData chosen;
    public void Setup()
    {
        RowInit(GemType.fish);
        RowInit(GemType.sweet);
        RowInit(GemType.salad);
        RowInit(GemType.meat);
        RowInit(GemType.drink);
        EventManager.instance.OnGameStarted += OnGameStartHandler;
    }

    private void OnGameStartHandler()
    {
        gameObject.SetActive(false);
    }

    private void RowInit(GemType type)
    {
        RowConroler conroler = new RowConroler();
        List<ChiefPlayerData> chiefs = GetChiefs(type);
        foreach (var chief in chiefs)
        {
            ChiefRow row = Instantiate(prefab, transform);
            row.Setup(chief);
            row.OnRowClick += ChooseChief;
            conroler.AddRow(row);
        }
        rows.Add(type, conroler);
    }
    private List<ChiefPlayerData> GetChiefs(GemType gemType)
    {
        List<ChiefPlayerData> chiefs = gemType switch
        {
            GemType.drink => chiefDB.GetUnlockedChiefsByType(GemType.drink),
            GemType.meat => chiefDB.GetUnlockedChiefsByType(GemType.meat),
            GemType.fish => chiefDB.GetUnlockedChiefsByType(GemType.fish),
            GemType.salad => chiefDB.GetUnlockedChiefsByType(GemType.salad),
            GemType.sweet => chiefDB.GetUnlockedChiefsByType(GemType.sweet),
            _ => null
        };
        return chiefs;
    }
    public void ShowChiefs(GemType gemType)
    {
        chosen = null;
        gameObject.SetActive(true);
        foreach (var key in rows.Keys)
        {
            RowConroler conroler = rows[key];
       
            conroler.SetActive(gemType == key);
        }
        empty.gameObject.SetActive(rows[gemType].GetRows().Count == 0);
    }
    private void ChooseChief(ChiefPlayerData chief) 
    {
        chosen = chief;
        OnChiefChoose?.Invoke(chosen);
    }
    public void ConfirmChoose()
    {
        gameObject.SetActive(false);
    }
}
