using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIMoneyEarned : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        EventManager.instance.OnMoneyEarned += OnMoneyEarned;
        rect = GetComponent<RectTransform>();
    }

    private void OnMoneyEarned(int money) 
    {
        text.text = money.ToString();
        rect.DOAnchorPosY(300, 1f).SetEase(Ease.InOutBack).OnComplete(
            ()=>
            {
                rect.DOAnchorPosY(300, 0.2f).OnComplete(
                    () =>
                    {
                        rect.DOAnchorPosY(0, 1f).SetEase(Ease.InOutBack);
                    });
            }
            );
    }
}
