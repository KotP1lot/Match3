using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartLvl : MonoBehaviour
{
    [SerializeField] Image openBtn;
    public void StartLvlBtn() 
    {
        openBtn.transform.DOLocalMoveY(1970f, 2f).SetEase(Ease.InOutBack);
        //EventManager.instance.OnGameStarted?.Invoke();
        //gameObject.SetActive(false);
    }
}
