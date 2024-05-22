using System.Collections.Generic;
using UnityEngine;

public class UIGoalCalendar : MonoBehaviour
{
    [SerializeField] UIGoal pref;
    List<UIGoal> UIGoals = new List<UIGoal>();

    public void Setup(GoalInfo[] goalInfo) 
    {
        foreach (UIGoal ui in UIGoals) 
        {
            Destroy(ui.gameObject);
        }
        UIGoals.Clear();
        foreach (GoalInfo info in goalInfo) 
        {
            Goal goal = new();
            goal.Setup(info.type, info.count, info.gemType, false);
            UIGoal uiGoal = Instantiate(pref, transform);
            uiGoal.Setup(goal);
            UIGoals.Add(uiGoal);
        }
    }
}
