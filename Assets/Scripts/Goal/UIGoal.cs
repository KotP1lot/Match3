using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGoal : MonoBehaviour
{
    public Goal goal;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] Image image;
    [SerializeField] db_GemSO gemSo;

    public void Setup(Goal newGoal)
    {
        goal = newGoal;
        goal.OnGoalAchived += (Goal goal) => Destroy();
        goal.OnGoalStateChanged += TextUpdate;
        TextUpdate(goal.count);
        SpriteSetup(goal.type);
    }
    private void SpriteSetup(GoalType type)
    {
        switch (type) 
        {
            case GoalType.gem:
                image.sprite = gemSo.GetIconByType(goal.gemType);
                break;
            case GoalType.clean:
                break;
        }
    }

    private void TextUpdate(int count)
    {
        textMeshPro.text = count.ToString();
    }

    private void Destroy() 
    {
        GetComponent<RectTransform>().DOAnchorPosY(1500, 1f).SetEase(Ease.InOutBack).OnComplete(() => { Destroy(gameObject); });
    }
}
