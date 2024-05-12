using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] List<Goal> goalList = new();
    [SerializeField] UIGoalList UIGoalList;

    public void Setup(GoalInfo[] goalInfos = null)
    {
        if (goalInfos == null)
        {
            foreach (Goal goal in goalList)
            {
                goal.Setup();
                goal.OnGoalAchived += ChangeState;
            }
        }
        else 
        {
            goalList = new();
            foreach (GoalInfo info in goalInfos)
            {
                Goal goal = new();
                goal.Setup(info.type, info.count, info.gemType);
                goal.OnGoalAchived += ChangeState;
                goalList.Add(goal);
            }
        }
        UIGoalList.Setup(goalList);
    }
    private void ChangeState(Goal goal) 
    { 
        goalList.Remove(goal);
        if (goalList.Count <= 0) 
        {
            EventManager.instance.OnAllGoalAchived?.Invoke();
        }
    }
}
