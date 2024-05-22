using System.Collections.Generic;
using UnityEngine;

public class UIChiefGrid : MonoBehaviour
{
    [SerializeField] db_Chief db;
    [SerializeField] UITypeLine typeLine;
    [SerializeField] UIChiefCard card;
    [SerializeField] UIStatChief stat;
    private GemType currType;
    List<UIChiefCard> cards = new();
    public void Start() 
    {
        stat.OnStatClose += UpdateShow;
        for (int i = 0; i < 9; i++)
        {
            UIChiefCard newCard = Instantiate(card, transform);
            cards.Add(newCard);
            newCard.OnOpenChief += ShowStat;
            newCard.SetActive(false);
        }
        typeLine.OnTypeClick += ShowType;
        typeLine.Setup();
    }
    private void UpdateShow() 
    {
        ShowType(currType);
    }
    private void ShowType(GemType type)
    {
        currType = type;
        List<ChiefPlayerData> playerData = db.GetChiefsByType(type);
        for (int i = 0; i < 9; i++) 
        {
            if (i < playerData.Count)
            {
                cards[i].SetActive(true);
                cards[i].Setup(playerData[i]);
            }
            else 
            {
                cards[i].SetActive(false);
            }
        }
    }
    private void ShowStat(ChiefPlayerData data) 
    {
        stat.gameObject.SetActive(true);
        stat.Setup(data);
    }
}
