using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField]List<Goal> goalList = new List<Goal>();
    [SerializeField] UIGoalList UIGoalList;
    public void Start()
    {
        foreach (Goal goal in goalList)
        {
            goal.Setup();
        }
        UIGoalList.Setup(goalList);
    }
}
