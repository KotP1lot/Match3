using System;
using UnityEngine;
[DefaultExecutionOrder(0)]
public class EventManager: MonoBehaviour
{
    public static EventManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public Action OnGameStarted;
    public Action<ActivableObject> ObjectsActivated;
    public Action<Gem> OnGemDestroy;
    public Action<GemType, int> OnChiefBonus;
    public Action<BonusGem> OnBonusCharged;
    public Action OnTurnEnded;
    public Action OnCustomerSatisfied;
    public Action OnFloorCleaned;
    public Action OnAllGoalAchived;
    public Action<int> OnMoneyEarned;

    public Action<int> OnComboChanged;
}