using System;
using UnityEngine;
[CreateAssetMenu()]
public class PlayerWalletSO : ISaveLoadSO
{
    public Resource Stars;
    public Resource Money;
    override public void Setup() 
    {
        Stars.OnResourceChanged += Save;
        Money.OnResourceChanged += Save;
    }
    override public void Save() 
    {
        string data = JsonUtility.ToJson(new PlayerWalletData() { Stars = Stars, Money = Money});
        PlayerPrefs.SetString("PlayerWallet", data);
    }

    override public void Load()
    {
        string strData = PlayerPrefs.GetString("PlayerWallet");
        PlayerWalletData data = JsonUtility.FromJson<PlayerWalletData>(strData);
        if (data == null)
        {
            Stars = new Resource();
            Money = new Resource();
            return;
        }
        Stars = data.Stars ?? new Resource();
        Money = data.Money ?? new Resource();
    }
    public override void SaveGameStat(GameStat stat)
    {
        Stars.AddAmount(stat.stars);
        Money.AddAmount(stat.money);
    }
    public override void Clear()
    {
        PlayerPrefs.DeleteKey("PlayerWallet");
    }

}
[Serializable]
public class PlayerWalletData 
{
    public Resource Stars;
    public Resource Money;
}
