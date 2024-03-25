using System;
using UnityEngine;

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
    public Action<ActivableObject> ObjectsActivatedEvent;
    public Action<Gem> OnGemDestroy;
    public Action TurnEndedEvent; 
}