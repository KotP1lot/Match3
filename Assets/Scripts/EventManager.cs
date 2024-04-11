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
    public Action<ActivableObject> ObjectsActivatedEvent;
    public Action<Gem> OnGemDestroy;
    public Action<int> OnChiefBonus;
    public Action<BonusGem> OnBonusCharged;
    public Action TurnEndedEvent;
    public Action OnCustomerSatisfied;
    public Action OnGemFallen;
}