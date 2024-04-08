using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGoal : MonoBehaviour
{
   public Goal goal;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] Image image;

    private void Start()
    {
        goal.OnGoalAchived += Destroy;
        goal.OnGoalStateChanged += TextUpdate;
        TextUpdate(goal.count);
    }

    private void TextUpdate(int count)
    {
        textMeshPro.text = count.ToString();
    }

    private void Destroy() 
    {
        transform.DOMoveX(10, 1f).OnComplete(() => { Destroy(gameObject); });
    }
}
