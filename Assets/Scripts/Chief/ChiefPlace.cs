using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChiefPlace : MonoBehaviour, IPointerDownHandler
{
    public GemType gemType;
    public ChiefSO chief;
    public event Action<GemType, ChiefPlace> OnPlaceClick;
    [SerializeField] Image chiefImg;
    private BonusGem bg;
    private bool isActive;
    private int countToUltimate;
    private Transform poolPos;
    public void Setup(Transform poolPos, BonusGem prefab)
    {
        if (chief == null) return;
        EventManager.instance.OnGemDestroy += OnGemDestroyHandler;
        bg = SpawnBG(poolPos, prefab, gemType);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPlaceClick?.Invoke(gemType, this);
    }
    public void SetChief(ChiefSO chief) 
    {
        this.chief = chief;
        chiefImg.sprite = chief.sprite;
    }

    #region BonusGem
    private void OnGemDestroyHandler(Gem gem)
    {
        if (gem.GetGemType() == gemType)
        {
            EventManager.instance.OnChiefBonus?.Invoke(chief.yumyBonus);
            if (!isActive) 
            {
                countToUltimate++;
                if (countToUltimate >= chief.countToUltimate)
                {
                    ActivateBG();
                }

            }
          
        }
    }
    public BonusGem SpawnBG(Transform pos, BonusGem prefab, GemType type)
    {
        BonusGem bg = Instantiate(prefab, pos);
        bg.Setup(type);
        poolPos = pos;
        bg.gameObject.SetActive(false);
        return bg;
    }
    public void ActivateBG()
    {
        EventManager.instance.OnBonusCharged?.Invoke(bg);
        bg.gameObject.SetActive(true);
        bg.OnGemDeactivate += (gem) => DeactivateBG((BonusGem)gem);
        isActive = true;
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
