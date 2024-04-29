using UnityEngine;

public class StartLvl : MonoBehaviour
{
    public void StartLvlBtn() 
    {
        EventManager.instance.OnGameStarted?.Invoke();
        gameObject.SetActive(false);
    }
}
