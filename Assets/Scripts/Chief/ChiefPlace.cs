using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChiefPlace : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] BonusSO bonuses;
    public GemType gemType;
    public ChiefSO chief;
    public ChiefLvlInfo lvl_Info;
    public event Action<GemType, ChiefPlace> OnPlaceClick;
    [SerializeField] Image chiefImg;
    [SerializeField] Image gemImg;
    private BonusGem bg;
    private bool isReady;
    private bool isActive;
    private int countToUltimate;
    private Transform poolPos;
    public void Setup(Transform poolPos, BonusGem prefab)
    {
        if (chief == null) return;
        EventManager.instance.OnGemDestroy += OnGemDestroyHandler;
        EventManager.instance.OnTurnEnded += OnTurnEndedHandler;
        bg = SpawnBG(poolPos, prefab, gemType);
    }


    public void Setup()
    {
        EventManager.instance.OnGemDestroy += OnGemDestroyHandler;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPlaceClick?.Invoke(gemType, this);
    }
    public void SetChief(ChiefPlayerData data) 
    {
        chief = data.chief;
        lvl_Info = data.chief.GetLvlInfo(data.lvl);
        gemImg.gameObject.SetActive(false);
        chiefImg.sprite = chief.sprite;
    }

    #region BonusGem
    private void OnGemDestroyHandler(Gem gem)
    {

        if (gem.GetGemType() == gemType)
        {
            EventManager.instance.OnChiefBonus?.Invoke(gem.GetGemType(), GetScore(gem));
            if (chief == null) return;
            if (isReady)
            {
                countToUltimate++;
                if (countToUltimate >= lvl_Info.countToUltimate)
                {
                    ActivateBG();
                }
            }
        }
    }
    private int GetScore(Gem gem) 
    {
        int score = 0;
        score += gem.GetScore();
        Debug.Log($"Without all: {score}");
        if (chief != null) score += lvl_Info.yumyBonus;
        Debug.Log($"With chief: {score}");
        score += score * bonuses.yummyBonus / 100;
        Debug.Log($"With bonuses: {score} (+{bonuses.yummyBonus}%)");
        return score;
    }
    private void OnTurnEndedHandler()
    {
        if (!isActive) 
        {
            isReady = true;
        }
    }
    public BonusGem SpawnBG(Transform pos, BonusGem prefab, GemType type)
    {
        BonusGem bg = Instantiate(prefab, pos);
        bg.Setup(type);
        poolPos = pos;
        bg.gameObject.SetActive(false);
        isReady = true;
        return bg;
    }
    public void ActivateBG()
    {
        EventManager.instance.OnBonusCharged?.Invoke(bg);
        bg.ChangeCanvasLayout(2);
        bg.transform.position = transform.position;
        bg.gameObject.SetActive(true);
        bg.OnGemDeactivate += (gem) => DeactivateBG((BonusGem)gem);
        isActive = true;
        isReady = false;
    }
    public void DeactivateBG(BonusGem bg)
    {
        bg.gameObject.SetActive(false);
        bg.transform.SetParent(poolPos);
        countToUltimate = 0;
        isActive = false;
        bg.isActivated = false;
    }
    #endregion

}
