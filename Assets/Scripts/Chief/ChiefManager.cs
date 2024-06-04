using System;
using System.Collections.Generic;
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
    [SerializeField] db_CustomerSO customers;
    [SerializeField] Transform gemPoolTransform;
    [SerializeField] ChiefChanger chiefChanger;
    private ChiefPlace activePlace;

    private void Start() 
    {
        string str_data = PlayerPrefs.GetString("ChiefPlace", null);
        ChiefPlaceSave data = JsonUtility.FromJson<ChiefPlaceSave>(str_data);
        EventManager.instance.OnGameStarted += OnStartGameHandler;
        chiefChanger.Setup();
        chiefChanger.OnChiefChoose += SetChiefToPlace;
        foreach (var place in chiefPlaces) 
        {
            place.OnPlaceClick += ShowChiefMenu;
            if (data != null && data.data !=null) 
            {
                ChiefPlaceData placeData = data.data.Find(x => x.gemType == place.gemType);
                if (placeData != null)
                {
                    place.SetChief(placeData.place);
                }
            }
        }
    }
    private void SetChiefToPlace(ChiefPlayerData chief) 
    {
        activePlace.SetChief(chief);
    }
    private void ShowChiefMenu(GemType gemType, ChiefPlace place) 
    {
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
        ChiefPlaceSave data = new()
        {
            data = new()
        };
        foreach (var place in chiefPlaces)
        {
            place.OnPlaceClick -= ShowChiefMenu;
            if (place.chief == null) 
            {
                place.Setup();
                continue;
            }
            place.Setup(gemPoolTransform, GetBGByType(place.chief.bgType));
            data.data.Add(new ChiefPlaceData() { gemType = place.gemType, place = place.playerData });

        }
        string chiefs = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("ChiefPlace", chiefs);
        PlayerPrefs.Save();
    }
}
[Serializable]
public class ChiefPlaceSave 
{
    public List<ChiefPlaceData> data;    
}
[Serializable]
public class ChiefPlaceData 
{
    public GemType gemType;
    public ChiefPlayerData place;
}