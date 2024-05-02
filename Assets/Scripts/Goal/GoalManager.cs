using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] List<Goal> goalList = new List<Goal>();
    [SerializeField] UIGoalList UIGoalList;
    public void Start()
    {
        foreach (Goal goal in goalList)
        {
            goal.Setup();
            goal.OnGoalAchived += ChangeState;
        }
        UIGoalList.Setup(goalList);
    }
    private void ChangeState() 
    {
       EventManager.instance.OnAllGoalAchived?.Invoke();//Чисто зачитсити ігрове поле
    }
}
