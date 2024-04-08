using System.Collections.Generic;
using UnityEngine;

public class ChiefManager : MonoBehaviour
{
    public Dictionary<GemType, Chief> chiefs = new Dictionary<GemType, Chief>();
    public ChiefPlace[] ChiefPlaces;
    private void Start()
    {
        EventManager.instance.OnGemDestroy += HandleGemDestroyed;   
    }
    public void ChangeChief(GemType gemType, Chief chief)
    {
        if (chiefs.ContainsKey(gemType))
        {
            chiefs[gemType] = chief;
        }
    }
    public void AddChief(GemType gemType, Chief chief)
    {
        if (!chiefs.ContainsKey(gemType))
        {
            chiefs[gemType] = chief;
        }
    }
    public void RemoveChef(GemType gemType)
    {
        if (chiefs.ContainsKey(gemType))
        {
            chiefs.Remove(gemType);
        }
    }
    public void HandleGemDestroyed(Gem gem)
    {
        int totalScore = gem.GetScore();
        GemType gemType = gem.GetGemType();
        foreach (ChiefPlace place in ChiefPlaces) 
        {
            Chief chief = place.Chief;
            if (chief != null && chief.GemType == gemType) 
            {
                totalScore += chief.HandleBonus();
                break;
            }
        }
        //if (chiefs.ContainsKey(gemType))
        //{
        //    Chief chief = chiefs[gemType];
        //    totalScore += chief.HandleBonus();
        //}
        EventManager.instance.OnScoreUpdate?.Invoke(totalScore);
    }
}
