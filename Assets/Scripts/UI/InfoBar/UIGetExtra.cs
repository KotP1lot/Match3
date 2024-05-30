using TMPro;
using UnityEngine;

public class UIGetExtra : MonoBehaviour
{
    [SerializeField] PlayerWalletSO wallet;
    [SerializeField] db_LvlSo lvlInfo;
    [SerializeField] EnergyManager energyManager;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] ExtraType type;
    [SerializeField] int amount = 2;
    public enum ExtraType 
    {
        money,
        energy
    }
    public void OnEnable()
    {
        switch (type)
        {
            case ExtraType.money:

                int lvls = lvlInfo.GetUnlocked().Count;
                amount = 200 + 30 * lvls;
                break;
            case ExtraType.energy:
                amount = 2;
                break;
        }
        textMeshPro.text = $"+{amount}!";
    }

    public void ShowAdd() 
    {
        switch (type)
        {
            case ExtraType.money:
                AdManager.Instance.rewarded.ShowAd(() =>
                {
                    wallet.Money.AddAmount(amount);
                });
                break;
            case ExtraType.energy:
                AdManager.Instance.rewarded.ShowAd(() =>
                {
                    energyManager.energySO.AddEnergy(amount);
                });
                break;
        }
    }
    
}
