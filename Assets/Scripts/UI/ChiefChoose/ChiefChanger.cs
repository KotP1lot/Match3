using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] ChiefDB chiefDB;
    [SerializeField] ChiefRow prefab;
    private Dictionary<GemType, RowConroler> rows = new Dictionary<GemType, RowConroler>();
    public event Action<ChiefSO> OnChiefChoose;
    public event Action OnConfirmed;
    private ChiefSO chosen;
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
        ChiefSO[] chiefs = GetChiefs(type);
        foreach (var chief in chiefs)
        {
            ChiefRow row = Instantiate(prefab, transform);
            row.Setup(chief);
            row.OnRowClick += ChooseChief;
            conroler.AddRow(row);
        }
        rows.Add(type, conroler);
    }
    private ChiefSO[] GetChiefs(GemType gemType) 
    {
        ChiefSO[] chiefs = gemType switch
        {
            GemType.drink => chiefDB.drink,
            GemType.meat => chiefDB.meat,
            GemType.fish => chiefDB.fish,
            GemType.salad => chiefDB.salad,
            GemType.sweet => chiefDB.sweet,
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
    }
    private void ChooseChief(ChiefSO chief) 
    {
        chosen = chief;
        OnChiefChoose?.Invoke(chosen);
    }
    public void ConfirmChoose()
    {
        OnConfirmed?.Invoke();
        gameObject.SetActive(false);
    }
}
