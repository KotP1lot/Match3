using UnityEngine;
using UnityEngine.UI;

public class UIFailure : MonoBehaviour
{
    [SerializeField] EnergyManager energyManager;

    [SerializeField] Image turnsAdd;
    [SerializeField] Image energyAdd;
    [SerializeField] Button restart;
    public bool isAddView = false;
    public void OnEnable() 
    {
        if (energyManager.GetEnergy() >= 2)
        {
            restart.interactable = true;
            energyAdd.gameObject.SetActive(false);
        }
        else
        {
            restart.interactable = false;
            energyAdd.gameObject.SetActive(!isAddView);
        }
        turnsAdd.gameObject.SetActive(!energyAdd.gameObject.active && !isAddView);
        isAddView = true;
    }
}
