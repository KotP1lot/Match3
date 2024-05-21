using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartLvl : MonoBehaviour
{
    [SerializeField] Transform openBtn;
    [SerializeField] RectTransform goalManager;
    [SerializeField] RectTransform customerBar;
    public void StartLvlBtn() 
    {
        GetComponent<Image>().enabled = false;
        openBtn.DOLocalMoveY(1970f, 2f).SetEase(Ease.InOutBack);
        goalManager.DOAnchorPosY(-20f, 2f).SetEase(Ease.InOutBack);
        customerBar.DOAnchorPosX(20f, 2f).SetEase(Ease.InOutBack);
    
        //EventManager.instance.OnGameStarted?.Invoke();
        //gameObject.SetActive(false);
    }
}
