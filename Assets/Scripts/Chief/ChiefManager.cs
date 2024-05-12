using System;
using UnityEngine;
public class ChiefManager : MonoBehaviour
{
    [Serializable]
    private class BonusGemByType 
    {
        public BGType type;
        public BonusGem prefab;
    }
    public ChiefPlace[] chiefPlaces;
    [SerializeField] BonusGemByType[] bonusGemsPrefab;
    [SerializeField] Transform gemPoolTransform;
    [SerializeField] ChiefChanger chiefChanger;
    private ChiefPlace activePlace;

    private void Start() 
    {
        EventManager.instance.OnGameStarted += OnStartGameHandler;
        chiefChanger.Setup();
        chiefChanger.OnChiefChoose += SetChiefToPlace;
        chiefChanger.OnConfirmed += ClearActivePlace;
        foreach (var place in chiefPlaces) 
        {
            place.OnPlaceClick += ShowChiefMenu;
        }
    }
    private void ClearActivePlace() 
    {
        activePlace = null;
    }
    private void SetChiefToPlace(ChiefSO chief) 
    {
        activePlace.SetChief(chief);
    }
    private void ShowChiefMenu(GemType gemType, ChiefPlace place) 
    {
        if (activePlace != null) return;
        chiefChanger.ShowChiefs(gemType);
        activePlace = place;
    }
    private BonusGem GetBGByType(BGType type)
    {
        foreach (BonusGemByType bg in bonusGemsPrefab) 
        {
            if (bg.type == type) return bg.prefab;
        }
        return null;
    }
    private void OnStartGameHandler() 
    {
        chiefChanger.OnChiefChoose -= SetChiefToPlace;
        foreach (var place in chiefPlaces)
        {
            if (place.chief == null) 
            {
                place.Setup();
                continue;
            }
            place.Setup(gemPoolTransform, GetBGByType(place.chief.bgType));
            place.OnPlaceClick -= ShowChiefMenu;
        }

    }
}
