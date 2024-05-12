using System.Collections.Generic;
using UnityEngine;

public class UIGoalList : MonoBehaviour
{
    [SerializeField] UIGoal goalPrefab;

    public void Setup(List<Goal> goals) 
    {
        foreach (Goal goal in goals) 
        {
            UIGoal ui = Instantiate(goalPrefab, transform);
            ui.Setup(goal);
        }
    }
}
