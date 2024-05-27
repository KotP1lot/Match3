using TMPro;
using UnityEngine;

public class UITurns : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turns;
    private int turnsLeft;

    public void Setup(TurnManager turnManager) 
    {
        turnManager.OnTurnEnded += ChangeValue;
        turnsLeft = turnManager.turnInfo.max;
        turns.text = turnsLeft.ToString();
    }
    private void ChangeValue() 
    {
        if (turnsLeft == 0) return;
        turnsLeft--;
        turns.text = turnsLeft.ToString();
    }
    public void UpdateValue(TurnManager turnManager)
    {
        turnsLeft = turnManager.turnInfo.max - turnManager.curentTurn;
        turns.text = turnsLeft.ToString();
    }
}
