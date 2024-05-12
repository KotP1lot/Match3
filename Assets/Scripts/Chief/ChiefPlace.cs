using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChiefPlace : MonoBehaviour, IPointerDownHandler
{
    public GemType gemType;
    public ChiefSO chief;
    public ChiefLvlInfo lvl_Info;
    public event Action<GemType, ChiefPlace> OnPlaceClick;
    [SerializeField] Image chiefImg;
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
    public void SetChief(ChiefSO chief, int lvl = 0) 
    {
        this.chief = chief;
        lvl_Info = chief.GetLvlInfo(lvl);
        chiefImg.sprite = lvl_Info.sprite;
    }

    #region BonusGem
    private void OnGemDestroyHandler(Gem gem)
    {
       
        if (gem.GetGemType() == gemType)
        {
            if (chief == null)
            {
                EventManager.instance.OnChiefBonus?.Invoke(gem.GetGemType(), gem.GetScore());
                return;
            }
            EventManager.instance.OnChiefBonus?.Invoke(gem.GetGemType(), gem.GetScore() + lvl_Info.yumyBonus);
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
